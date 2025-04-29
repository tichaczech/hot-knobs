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
    description: `Details for the training session: ${training.title} on ${training.date.toLocaleDateString()} at ${training.location}.`,
  };
}


export default async function TrainingDetailsPage({ params }: TrainingDetailsPageProps) {
  const training = await getTrainingById(params.id);
  const currentUser = await getCurrentUser();

  if (!training) {
    notFound(); // Redirect to 404 if training doesn't exist
  }

   const isRegistered = currentUser && training.registeredRiders?.includes(currentUser.id);


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
          showRegisterButton={currentUser?.role === 'rider'}
          showUnregisterButton={isRegistered} // Show unregister if already registered on this page
          showViewDetailsLink={false} // Hide the 'View Details' link on the details page
       />

       {/* Potentially add more details specific to this page if needed */}
       {/* <Card>
           <CardHeader>
               <CardTitle>Additional Information</CardTitle>
           </CardHeader>
           <CardContent>
               <p>More details about the terrain, specific requirements, or schedule could go here.</p>
               {currentUser?.role === 'trainer' && currentUser.id === training.trainerId && (
                 <div className="mt-4">
                    <Button variant="outline" size="sm">Edit Training</Button>
                    <Button variant="destructive" size="sm" className="ml-2">Delete Training</Button>
                 </div>
               )}
           </CardContent>
       </Card> */}

    </div>
  );
}
