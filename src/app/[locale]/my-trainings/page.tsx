import { getMyRegisteredTrainings, getMyPendingTrainings, getMyWaitingListTrainings, getCurrentUser } from '@/lib/placeholder-data'; // Added getMyWaitingListTrainings
import { TrainingCard } from '@/components/training-card';
import { Metadata } from 'next';
import { redirect } from 'next/navigation';
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { Info, Clock, Hourglass, ListChecks } from 'lucide-react'; // Added Hourglass, ListChecks
import { getI18n } from '@/locales/server';
import type { Locale } from '@/locales/config';
import Link from 'next/link';
import { Separator } from '@/components/ui/separator';

export async function generateMetadata({ params: { locale } }: { params: { locale: Locale } }): Promise<Metadata> {
  const t = await getI18n(locale);
  return {
    title: t('myTrainings.meta.title'),
    description: t('myTrainings.meta.description'),
  };
}


export default async function MyTrainingsPage() {
  const t = await getI18n();
  const currentUser = await getCurrentUser();

  if (!currentUser || currentUser.role !== 'rider') {
     redirect('/');
  }

  const [confirmedTrainings, pendingTrainings, waitingTrainings] = await Promise.all([
      getMyRegisteredTrainings(currentUser.id), // Fetches 'Confirmed' status
      getMyPendingTrainings(currentUser.id),   // Fetches 'Created' status (for closed trainings)
      getMyWaitingListTrainings(currentUser.id), // Fetches 'Waiting' status
  ]);

   const upcomingConfirmed = confirmedTrainings
      .filter(t => t.date >= new Date())
      .sort((a, b) => a.date.getTime() - b.date.getTime());

    const upcomingPending = pendingTrainings // 'Created' status
      .filter(t => t.date >= new Date())
      .sort((a, b) => a.date.getTime() - b.date.getTime());

    const upcomingWaiting = waitingTrainings // 'Waiting' status
      .filter(t => t.date >= new Date())
      .sort((a, b) => a.registrations.find(r => r.userId === currentUser.id)!.registeredAt.getTime() - // Sort by waiting list join time
                       b.registrations.find(r => r.userId === currentUser.id)!.registeredAt.getTime());

   const pastConfirmed = confirmedTrainings // Only show past *confirmed* trainings
      .filter(t => t.date < new Date())
      .sort((a, b) => b.date.getTime() - a.date.getTime());

   const hasUpcomingActivity = upcomingConfirmed.length > 0 || upcomingPending.length > 0 || upcomingWaiting.length > 0;

  return (
    <div className="space-y-8">
      <h1 className="text-3xl font-bold text-center">{t('myTrainings.title')}</h1>

       {!hasUpcomingActivity && pastConfirmed.length === 0 && (
             <Alert>
                <Info className="h-4 w-4" />
                <AlertTitle>{t('myTrainings.noRegistrations')}</AlertTitle>
                <AlertDescription>
                    <span dangerouslySetInnerHTML={{ __html: t('myTrainings.noRegistrationsDesc').replace('<link>', `<a href="/trainings" class="font-medium text-primary underline">`).replace('</link>', '</a>') }} />
                </AlertDescription>
            </Alert>
       )}

        {/* Pending Approval Section ('Created' status) */}
        {upcomingPending.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4 flex items-center gap-2">
                   <ListChecks className="h-6 w-6 text-blue-600"/> {t('myTrainings.pendingApproval')} ({upcomingPending.length})
               </h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingPending.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Cannot register while pending
                        showCancelButton={true} // Allow withdrawing request
                        showViewDetailsLink={true} // Allow viewing details
                    />
                    ))}
                </div>
           </section>
       )}

        {/* Waiting List Section */}
        {upcomingWaiting.length > 0 && (
           <section>
                { upcomingPending.length > 0 && <Separator className="my-8"/> }
               <h2 className="text-2xl font-semibold mb-4 flex items-center gap-2">
                   <Hourglass className="h-6 w-6 text-yellow-600"/> {t('myTrainings.waitingList')} ({upcomingWaiting.length})
               </h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingWaiting.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Cannot register while waiting
                        showCancelButton={true} // Allow leaving waiting list
                        showViewDetailsLink={true} // Allow viewing details
                    />
                    ))}
                </div>
           </section>
       )}


       {/* Upcoming Confirmed Section */}
       {upcomingConfirmed.length > 0 && (
           <section>
                 { (upcomingPending.length > 0 || upcomingWaiting.length > 0) && <Separator className="my-8"/> }
               <h2 className="text-2xl font-semibold mb-4">{t('myTrainings.upcomingConfirmed')} ({upcomingConfirmed.length})</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingConfirmed.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Already registered
                        showCancelButton={true} // Allow cancelling confirmed registration
                        showViewDetailsLink={true} // Show normal details link
                    />
                    ))}
                </div>
           </section>
       )}

        {/* Past Section */}
        {pastConfirmed.length > 0 && (
           <section>
                { hasUpcomingActivity && <Separator className="my-8"/> }
               <h2 className="text-2xl font-semibold mb-4 text-muted-foreground">{t('myTrainings.past')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 opacity-70">
                    {pastConfirmed.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Don't allow re-registering for past events
                        showCancelButton={false} // Don't allow cancelling past events
                        showViewDetailsLink={true} // Still allow viewing details
                    />
                    ))}
                </div>
           </section>
       )}


    </div>
  );
}
