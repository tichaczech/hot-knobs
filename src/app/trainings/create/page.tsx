import { getCurrentUser } from '@/lib/placeholder-data';
import { redirect } from 'next/navigation';
import { Metadata } from 'next';
import { CreateTrainingForm } from './_components/create-training-form';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';


export const metadata: Metadata = {
  title: 'Create Training - Mad Sprocket',
  description: 'Create a new motocross or enduro training session.',
};


export default async function CreateTrainingPage() {
  const currentUser = await getCurrentUser();

  // This page is only for trainers
  if (!currentUser || currentUser.role !== 'trainer') {
     redirect('/');
  }


  return (
    <div className="max-w-2xl mx-auto">
         <Card className="shadow-lg">
            <CardHeader>
                <CardTitle className="text-2xl">Create New Training Session</CardTitle>
                <CardDescription>Fill in the details for your upcoming training session.</CardDescription>
            </CardHeader>
            <CardContent>
                <CreateTrainingForm trainerId={currentUser.id} />
            </CardContent>
        </Card>
    </div>
  );
}
