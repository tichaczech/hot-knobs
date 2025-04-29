import { getMyRegisteredTrainings, getCurrentUser } from '@/lib/placeholder-data';
import { TrainingCard } from '@/components/training-card';
import { Metadata } from 'next';
import { redirect } from 'next/navigation';
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { Info } from 'lucide-react';


export const metadata: Metadata = {
  title: 'My Registrations - ThrottleUp',
  description: 'View the training sessions you are registered for.',
};


export default async function MyTrainingsPage() {
  const currentUser = await getCurrentUser();

  // This page is only for riders
  if (!currentUser || currentUser.role !== 'rider') {
     // Redirect non-riders or guests to the homepage or login page
     // For now, redirecting to homepage
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
      <h1 className="text-3xl font-bold text-center">My Registered Trainings</h1>

       {upcomingRegistrations.length === 0 && pastRegistrations.length === 0 && (
             <Alert>
                <Info className="h-4 w-4" />
                <AlertTitle>No Registrations Yet!</AlertTitle>
                <AlertDescription>
                    You haven't registered for any training sessions. Head over to the <a href="/trainings" className="font-medium text-primary underline">Available Trainings</a> page to find one.
                </AlertDescription>
            </Alert>
       )}

       {upcomingRegistrations.length > 0 && (
           <section>
               <h2 className="text-2xl font-semibold mb-4">Upcoming Sessions</h2>
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
               <h2 className="text-2xl font-semibold mb-4 text-muted-foreground">Past Sessions</h2>
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
