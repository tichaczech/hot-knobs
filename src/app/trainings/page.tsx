import { getTrainings, getCurrentUser } from '@/lib/placeholder-data';
import { TrainingCard } from '@/components/training-card';
import { Metadata } from 'next';

export const metadata: Metadata = {
  title: 'Available Trainings - Mad Sprocket',
  description: 'Browse all available motocross and enduro training sessions.',
};


export default async function TrainingsPage() {
  const trainings = await getTrainings();
  const currentUser = await getCurrentUser();

  const upcomingTrainings = trainings
      .filter(t => t.date >= new Date()) // Show today's and future trainings
      .sort((a, b) => a.date.getTime() - b.date.getTime()); // Sort by date ascending


  return (
    <div className="space-y-6">
      <h1 className="text-3xl font-bold text-center">Available Training Sessions</h1>

       {upcomingTrainings.length > 0 ? (
           <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {upcomingTrainings.map((training) => (
                <TrainingCard
                    key={training.id}
                    training={training}
                    currentUser={currentUser}
                    showRegisterButton={currentUser?.role === 'rider'} // Only show register if user is a rider
                     showViewDetailsLink={true}
                />
                ))}
            </div>
       ) : (
           <p className="text-center text-muted-foreground mt-8">No upcoming training sessions found. Check back soon!</p>
       )}

    </div>
  );
}
