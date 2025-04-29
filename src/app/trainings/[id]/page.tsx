import { getTrainingById, getCurrentUser } from '@/lib/placeholder-data';
import { TrainingCard } from '@/components/training-card';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { ArrowLeft } from 'lucide-react';
import Link from 'next/link';
import { Button } from '@/components/ui/button';


interface TrainingDetailsPageProps {
  params: { id: string };
}

export async function generateMetadata({ params }: TrainingDetailsPageProps): Promise<Metadata> {
  const training = await getTrainingById(params.id);
  if (!training) {
    return {
      title: 'Training Not Found - ThrottleUp',
    };
  }
  return {
    title: `${training.title} - ThrottleUp Training`,
    description: `Details for the training session: ${training.title} on ${training.date.toLocaleDateString()} at ${training.location}. Suitable for ${training.skillLevels.join(', ')}.`,
  };
}


export default async function TrainingDetailsPage({ params }: TrainingDetailsPageProps) {
  const training = await getTrainingById(params.id);
  const currentUser = await getCurrentUser();

  if (!training) {
    notFound(); // Redirect to 404 if training doesn't exist
  }

   const isRegistered = !!currentUser && !!training.registeredRiders?.includes(currentUser.id); // Added checks for null/undefined


  return (
    <div className="space-y-6 max-w-4xl mx-auto">
       <Link href="/trainings" passHref legacyBehavior>
            <Button variant="outline" size="sm" className="mb-4">
                 <ArrowLeft className="mr-2 h-4 w-4" /> Back to Trainings
            </Button>
        </Link>
      {/* Re-use TrainingCard for consistent display, but hide redundant elements */}
      <TrainingCard
          training={training}
          currentUser={currentUser}
          // Only show register button if user is a rider AND not already registered
          showRegisterButton={currentUser?.role === 'rider'}
          // Only show unregister button if user is a rider AND is registered
          showUnregisterButton={currentUser?.role === 'rider' && isRegistered}
          showViewDetailsLink={false} // Hide the 'View Details' link on the details page
       />

       {/* Card for full description if needed */}
       <Card>
           <CardHeader>
               <CardTitle>Full Description</CardTitle>
           </CardHeader>
           <CardContent>
               <p className="whitespace-pre-wrap">{training.description}</p>
               {/* Potential future actions for trainer */}
               {currentUser?.role === 'trainer' && currentUser.id === training.trainerId && (
                 <div className="mt-4 border-t pt-4 flex gap-2">
                    {/* Placeholder buttons for future functionality */}
                    {/* <Button variant="outline" size="sm">Edit Training</Button>
                    <Button variant="destructive" size="sm">Delete Training</Button> */}
                 </div>
               )}
           </CardContent>
       </Card>

    </div>
  );
}
