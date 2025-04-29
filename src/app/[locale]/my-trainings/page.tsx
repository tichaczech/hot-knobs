import { getMyRegisteredTrainings, getCurrentUser } from '@/lib/placeholder-data';
import { TrainingCard } from '@/components/training-card';
import { Metadata } from 'next';
import { redirect } from 'next/navigation';
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { Info } from 'lucide-react';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type
import Link from 'next/link'; // Import Link for the alert message

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

  const registeredTrainings = await getMyRegisteredTrainings(currentUser.id);

   const upcomingRegistrations = registeredTrainings
      .filter(t => t.date >= new Date()) // Show today's and future trainings
      .sort((a, b) => a.date.getTime() - b.date.getTime()); // Sort by date ascending

   const pastRegistrations = registeredTrainings
      .filter(t => t.date < new Date()) // Show past trainings
      .sort((a, b) => b.date.getTime() - a.date.getTime()); // Sort by date descending


  return (
    <div className="space-y-8">
      <h1 className="text-3xl font-bold text-center">{t('myTrainings.title')}</h1>

       {upcomingRegistrations.length === 0 && pastRegistrations.length === 0 && (
             <Alert>
                <Info className="h-4 w-4" />
                <AlertTitle>{t('myTrainings.noRegistrations')}</AlertTitle>
                <AlertDescription>
                    {/* Use dangerouslySetInnerHTML for simple link embedding, or use a more robust solution if needed */}
                    <span dangerouslySetInnerHTML={{ __html: t('myTrainings.noRegistrationsDesc').replace('<link>', `<a href="/trainings" class="font-medium text-primary underline">`).replace('</link>', '</a>') }} />
                </AlertDescription>
            </Alert>
       )}

       {upcomingRegistrations.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4">{t('myTrainings.upcoming')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {upcomingRegistrations.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showUnregisterButton={true} // Show unregister button here
                        showViewDetailsLink={true}
                    />
                    ))}
                </div>
           </section>
       )}

        {pastRegistrations.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4 text-muted-foreground">{t('myTrainings.past')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 opacity-70">
                    {pastRegistrations.map((training) => (
                    <TrainingCard
                        key={training.id}
                        training={training}
                        currentUser={currentUser}
                        showUnregisterButton={false} // Don't allow unregistering from past events
                        showViewDetailsLink={true}
                    />
                    ))}
                </div>
           </section>
       )}


    </div>
  );
}
