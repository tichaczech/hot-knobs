import { getMyRegisteredTrainings, getMyPendingTrainings, getCurrentUser } from '@/lib/placeholder-data'; // Added getMyPendingTrainings
import { TrainingCard } from '@/components/training-card';
import { Metadata } from 'next';
import { redirect } from 'next/navigation';
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { Info, Clock } from 'lucide-react'; // Added Clock icon
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type
import Link from 'next/link'; // Import Link for the alert message
import { Separator } from '@/components/ui/separator'; // Import Separator

// Generate localized metadata
export async function generateMetadata({ params: { locale } }: { params: { locale: Locale } }): Promise<Metadata> {
  const t = await getI18n(locale);
  return {
    title: t('myTrainings.meta.title'),
    description: t('myTrainings.meta.description'),
  };
}


export default async function MyTrainingsPage() {
  const t = await getI18n(); // Get translation function
  const currentUser = await getCurrentUser();

  // This page is only for riders
  if (!currentUser || currentUser.role !== 'rider') {
     // Redirect non-riders or guests to the localized homepage
     redirect('/');
  }

  const [registeredTrainings, pendingTrainings] = await Promise.all([
      getMyRegisteredTrainings(currentUser.id),
      getMyPendingTrainings(currentUser.id), // Fetch pending trainings
  ]);

   const upcomingRegistrations = registeredTrainings
      .filter(t => t.date >= new Date()) // Show today's and future trainings
      .sort((a, b) => a.date.getTime() - b.date.getTime()); // Sort by date ascending

    const upcomingPending = pendingTrainings
      .filter(t => t.date >= new Date()) // Show today's and future trainings
      .sort((a, b) => a.date.getTime() - b.date.getTime()); // Sort by date ascending

   const pastRegistrations = registeredTrainings
      .filter(t => t.date < new Date()) // Show past trainings
      .sort((a, b) => b.date.getTime() - a.date.getTime()); // Sort by date descending


  return (
    <div className="space-y-8">
      <h1 className="text-3xl font-bold text-center">{t('myTrainings.title')}</h1>

       {upcomingRegistrations.length === 0 && upcomingPending.length === 0 && pastRegistrations.length === 0 && (
             <Alert>
                <Info className="h-4 w-4" />
                <AlertTitle>{t('myTrainings.noRegistrations')}</AlertTitle>
                <AlertDescription>
                    {/* Use dangerouslySetInnerHTML for simple link embedding, or use a more robust solution if needed */}
                    <span dangerouslySetInnerHTML={{ __html: t('myTrainings.noRegistrationsDesc').replace('<link>', `<a href="/trainings" class="font-medium text-primary underline">`).replace('</link>', '</a>') }} />
                </AlertDescription>
            </Alert>
       )}

        {/* Pending Section */}
        {upcomingPending.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4 flex items-center gap-2">
                   <Clock className="h-6 w-6 text-yellow-600"/> {t('myTrainings.pending')}
               </h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingPending.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Cannot re-register if pending
                        showUnregisterButton={true} // Show withdraw button
                        showViewDetailsLink={false} // Use status link instead
                        showViewStatusLink={true} // Show "View Status" link
                    />
                    ))}
                </div>
                <Separator className="my-8" />
           </section>
       )}


       {/* Upcoming Approved Section */}
       {upcomingRegistrations.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4">{t('myTrainings.upcoming')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingRegistrations.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Already registered
                        showUnregisterButton={true} // Show unregister button here
                        showViewDetailsLink={true} // Show normal details link
                        showViewStatusLink={false}
                    />
                    ))}
                </div>
           </section>
       )}

        {/* Past Section */}
        {pastRegistrations.length > 0 && (
           <section>
                { (upcomingPending.length > 0 || upcomingRegistrations.length > 0) && <Separator className="my-8"/> }
               <h2 className="text-2xl font-semibold mb-4 text-muted-foreground">{t('myTrainings.past')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 opacity-70">
                    {pastRegistrations.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showRegisterButton={false} // Don't allow re-registering for past events
                        showUnregisterButton={false} // Don't allow unregistering from past events
                        showViewDetailsLink={true} // Still allow viewing details
                        showViewStatusLink={false}
                    />
                    ))}
                </div>
           </section>
       )}


    </div>
  );
}
