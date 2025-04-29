import { getTrainingById, getCurrentUser } from '@/lib/placeholder-data';
import { TrainingCard } from '@/components/training-card';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { ArrowLeft, MapPin } from 'lucide-react';
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type
import type { SkillLevel } from '@/lib/types'; // Import SkillLevel type

interface TrainingDetailsPageProps {
  params: { id: string; locale: Locale }; // Add locale to params
}

// Helper function to translate SkillLevel safely
const translateSkillLevel = async (level: SkillLevel, locale: Locale): Promise<string> => {
    const t = await getI18n(locale);
    try {
      return t(`skillLevels.${level}`);
    } catch (e) {
      console.warn(`Missing translation for skill level: ${level} in locale: ${locale}`);
      return level; // Fallback to the key
    }
  };

export async function generateMetadata({ params }: TrainingDetailsPageProps): Promise<Metadata> {
  const t = await getI18n(params.locale);
  const training = await getTrainingById(params.id);
  if (!training) {
    return {
      title: t('trainingDetails.meta.notFoundTitle'),
    };
  }

  // Translate skill levels for description
   const translatedSkillLevels = await Promise.all(
        training.skillLevels.map(level => translateSkillLevel(level, params.locale))
    );

  return {
    title: t('trainingDetails.meta.title', { trainingTitle: training.title }),
    description: t('trainingDetails.meta.description', {
        trainingTitle: training.title,
        // TODO: Consider locale-aware date formatting
        date: training.date.toLocaleDateString(params.locale), // Basic locale date string
        locationName: training.locationName,
        skillLevels: translatedSkillLevels.join(', ')
    }),
  };
}


export default async function TrainingDetailsPage({ params }: TrainingDetailsPageProps) {
  const t = await getI18n(params.locale); // Get translation function for the current locale
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
                 <ArrowLeft className="mr-2 h-4 w-4" /> {t('trainingDetails.backToTrainings')}
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

        {/* Card for Location Link */}
        <Card>
            <CardHeader>
                <CardTitle className="flex items-center text-base"> {/* Reduced size */}
                    <MapPin className="mr-2 h-4 w-4" /> {t('trainingDetails.location')}
                </CardTitle>
            </CardHeader>
             <CardContent>
                <Link href={`/locations/${training.locationId}`} passHref legacyBehavior>
                    <Button variant="link" className="p-0 h-auto text-base"> {/* Adjust link styling */}
                         {training.locationName}
                    </Button>
                </Link>
            </CardContent>
        </Card>

       {/* Card for full description if needed */}
       <Card>
           <CardHeader>
               <CardTitle>{t('trainingDetails.fullDescription')}</CardTitle>
           </CardHeader>
           <CardContent>
               <p className="whitespace-pre-wrap">{training.description}</p>
               {/* Potential future actions for trainer */}
               {currentUser?.role === 'trainer' && currentUser.id === training.trainerId && (
                 <div className="mt-4 border-t pt-4 flex gap-2">
                    {/* Placeholder buttons for future functionality */}
                    {/* <Button variant="outline" size="sm">{t('trainingDetails.editTraining')}</Button>
                    <Button variant="destructive" size="sm">{t('trainingDetails.deleteTraining')}</Button> */}
                 </div>
               )}
           </CardContent>
       </Card>

    </div>
  );
}
