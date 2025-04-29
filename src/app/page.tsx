import { getCurrentUser, getTrainings } from '@/lib/placeholder-data';
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { TrainingCard } from '@/components/training-card';
import { ArrowRight } from 'lucide-react';

export default async function Home() {
  const user = await getCurrentUser();
  const upcomingTrainings = (await getTrainings())
    .filter(t => t.date > new Date()) // Filter for future trainings
    .sort((a, b) => a.date.getTime() - b.date.getTime()) // Sort by date ascending
    .slice(0, 3); // Take the first 3

  return (
    <div className="space-y-8">
      <section className="text-center bg-card p-8 rounded-lg shadow">
        <h1 className="text-4xl font-bold text-primary mb-2">Welcome to Mad Sprocket!</h1>
        <p className="text-lg text-muted-foreground mb-4">
          Your hub for motocross and enduro training sessions.
        </p>
        {user ? (
          <p className="text-md">
            Logged in as <span className="font-semibold">{user.name} ({user.role})</span>.
          </p>
        ) : (
          <p className="text-md text-muted-foreground">
            Browse available trainings or log in to register.
          </p>
        )}
         <div className="mt-6 flex justify-center gap-4">
             <Link href="/trainings" passHref>
                 <Button size="lg">
                     View All Trainings <ArrowRight className="ml-2 h-5 w-5"/>
                 </Button>
             </Link>
              {user?.role === 'rider' && (
                 <Link href="/my-trainings" passHref>
                     <Button size="lg" variant="outline">
                        My Registrations
                    </Button>
                 </Link>
             )}
             {user?.role === 'trainer' && (
                 <Link href="/trainings/create" passHref>
                    <Button size="lg" variant="outline">
                        Create New Training
                    </Button>
                 </Link>
             )}
             {!user && (
                 <Button size="lg" variant="secondary">Login / Sign Up</Button>
             )}
         </div>
      </section>

       {upcomingTrainings.length > 0 && (
            <section>
                <h2 className="text-2xl font-semibold mb-4 text-center">Upcoming Training Sessions</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {upcomingTrainings.map((training) => (
                    <TrainingCard
                    key={training.id}
                    training={training}
                    currentUser={user}
                    showRegisterButton={user?.role === 'rider'}
                    showViewDetailsLink={true}
                    />
                ))}
                </div>
                 <div className="text-center mt-6">
                    <Link href="/trainings" passHref>
                        <Button variant="link">
                            See all available trainings <ArrowRight className="ml-1 h-4 w-4"/>
                        </Button>
                    </Link>
                 </div>
            </section>
        )}

    </div>
  );
}
