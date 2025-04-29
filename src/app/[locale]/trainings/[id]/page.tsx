import { getTrainingById, getCurrentUser, placeholderUsers, getUserRegistration, getRegistrationCounts } from '@/lib/placeholder-data'; // Import helpers
import { TrainingCard } from '@/components/training-card';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { ArrowLeft, MapPin, CheckCircle, Clock, XCircle, Ban, Users, Edit, Trash2, UserCheck, Hourglass, ListChecks } from 'lucide-react'; // Added status icons
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { getI18n } from '@/locales/server';
import type { Locale } from '@/locales/config';
import type { SkillLevel, RegistrationType, RegistrationStatus } from '@/lib/types';
import { Badge } from '@/components/ui/badge';
import { Separator } from '@/components/ui/separator';
import { RegistrationManagement } from './_components/registration-management';

interface TrainingDetailsPageProps {
  params: { id: string; locale: Locale };
}

// Helper function to translate SkillLevel safely
const translateSkillLevel = async (level: SkillLevel, locale: Locale): Promise<string> => {
    const t = await getI18n(locale);
    try {
      return t(`skillLevels.${level}`);
    } catch (e) {
      console.warn(`Missing translation for skill level: ${level} in locale: ${locale}`);
      return level;
    }
  };

// Helper function to translate RegistrationType safely
const translateRegistrationType = async (type: RegistrationType, locale: Locale): Promise<string> => {
    const t = await getI18n(locale);
    try {
        return t(`registrationTypes.${type}`);
    } catch (e) {
        console.warn(`Missing translation for registration type: ${type} in locale: ${locale}`);
        return type;
    }
};

// Helper function to translate RegistrationStatus safely
const translateRegistrationStatus = async (status: RegistrationStatus, locale: Locale): Promise<string> => {
    const t = await getI18n(locale);
    try {
      return t(`registrationStatuses.${status}`);
    } catch (e) {
      console.warn(`Missing translation for status: ${status} in locale: ${locale}`);
      return status;
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

   const translatedSkillLevels = await Promise.all(
        training.skillLevels.map(level => translateSkillLevel(level, params.locale))
    );

   // Format date server-side for metadata, potentially using a less specific format or default locale
   const formattedDateForMeta = training.date.toLocaleDateString(params.locale, { year: 'numeric', month: 'long', day: 'numeric' });


  return {
    title: t('trainingDetails.meta.title', { trainingTitle: training.title }),
    description: t('trainingDetails.meta.description', {
        trainingTitle: training.title,
        date: formattedDateForMeta, // Use server-formatted date for metadata
        locationName: training.locationName,
        skillLevels: translatedSkillLevels.join(', ')
    }),
  };
}


export default async function TrainingDetailsPage({ params }: TrainingDetailsPageProps) {
  const t = await getI18n(params.locale);
  const training = await getTrainingById(params.id);
  const currentUser = await getCurrentUser();

  if (!training) {
    notFound();
  }

   // Get user's registration details for this training
   const userRegistration = getUserRegistration(training, currentUser?.id);
   const userStatus = userRegistration?.status;
   const userReason = userRegistration?.reason;

   // Determine capabilities based on user role and status
   // Can register IF: user is a rider AND (no existing registration OR status is not Cancelled and not Rejected)
   const canRegister = currentUser?.role === 'rider' &&
                      (!userStatus || (userStatus !== 'Cancelled' && userStatus !== 'Rejected'));
   const canCancel = currentUser?.role === 'rider' &&
                      userStatus &&
                      (userStatus === 'Confirmed' || userStatus === 'Created' || userStatus === 'Waiting');
   const isTrainerOwner = currentUser?.role === 'trainer' && currentUser.id === training.trainerId;

   const translatedRegType = await translateRegistrationType(training.registrationType, params.locale);
   const translatedUserStatus = userStatus ? await translateRegistrationStatus(userStatus, params.locale) : null;

   const { confirmed, waiting } = getRegistrationCounts(training);
   const capacityString = training.maxRiders
     ? t('trainingDetails.capacity', { confirmed, max: training.maxRiders })
     : t('trainingDetails.unlimitedCapacity');
   const waitingListString = waiting > 0 ? t('trainingDetails.waitingListCount', { count: waiting }) : '';


  return (
    <div className="space-y-6 max-w-4xl mx-auto">
       <Link href="/trainings" passHref legacyBehavior>
            <Button variant="outline" size="sm" className="mb-4">
                 <ArrowLeft className="mr-2 h-4 w-4" /> {t('trainingDetails.backToTrainings')}
            </Button>
        </Link>

      {/*
        Re-use TrainingCard for consistent display.
        Pass the determined capabilities.
      */}
      <TrainingCard
          training={training}
          currentUser={currentUser}
          showRegisterButton={canRegister} // Pass determined capability
          showCancelButton={canCancel}     // Pass determined capability
          showViewDetailsLink={false}      // Don't show details link *within* the card on the details page
       />

        {/* Card for Location Link */}
        <Card>
            <CardHeader>
                <CardTitle className="flex items-center text-base">
                    <MapPin className="mr-2 h-4 w-4" /> {t('trainingDetails.location')}
                </CardTitle>
            </CardHeader>
             <CardContent>
                <Link href={`/locations/${training.locationId}`} passHref legacyBehavior>
                    <Button variant="link" className="p-0 h-auto text-base">
                         {training.locationName}
                    </Button>
                </Link>
            </CardContent>
        </Card>

        {/* Card for User's Registration Status (if logged in as rider) */}
         {currentUser && currentUser.role === 'rider' && (
            <Card>
                <CardHeader>
                    <CardTitle className="flex items-center text-base">
                       <UserCheck className="mr-2 h-4 w-4"/> {t('trainingDetails.yourStatus')}
                    </CardTitle>
                     <CardDescription>
                        {t('trainingDetails.registrationTypeLabel')} {translatedRegType}
                    </CardDescription>
                </CardHeader>
                 <CardContent className="space-y-2">
                   {!userStatus && (
                       <p className="text-sm text-muted-foreground">{t('trainingDetails.notRegistered')}</p>
                    )}
                   {userStatus === 'Confirmed' && (
                     <Badge variant="default" className="bg-green-600 hover:bg-green-700 text-base">
                       <CheckCircle className="mr-2 h-4 w-4" /> {translatedUserStatus}
                     </Badge>
                   )}
                   {userStatus === 'Created' && ( // Pending Approval
                     <Badge variant="secondary" className="text-blue-800 bg-blue-100 border-blue-300 text-base">
                       <ListChecks className="mr-2 h-4 w-4" /> {translatedUserStatus}
                     </Badge>
                   )}
                   {userStatus === 'Waiting' && (
                     <Badge variant="secondary" className="text-yellow-800 bg-yellow-100 border-yellow-300 text-base">
                       <Hourglass className="mr-2 h-4 w-4" /> {translatedUserStatus}
                     </Badge>
                   )}
                   {userStatus === 'Rejected' && (
                     <div className="space-y-1">
                         <Badge variant="destructive" className="text-base">
                            <XCircle className="mr-2 h-4 w-4" /> {translatedUserStatus}
                        </Badge>
                        {userReason && <p className="text-sm text-muted-foreground">{t('reason')}: {userReason}</p>}
                     </div>
                   )}
                    {userStatus === 'Cancelled' && (
                     <div className="space-y-1">
                         <Badge variant="outline" className="text-muted-foreground text-base">
                            <Ban className="mr-2 h-4 w-4" /> {translatedUserStatus}
                        </Badge>
                        {userReason && <p className="text-sm text-muted-foreground">{t('reason')}: {userReason}</p>}
                     </div>
                   )}
                </CardContent>
            </Card>
         )}

       {/* Card for full description */}
       <Card>
           <CardHeader>
               <CardTitle>{t('trainingDetails.fullDescription')}</CardTitle>
           </CardHeader>
           <CardContent>
               <p className="whitespace-pre-wrap">{training.description}</p>
           </CardContent>
       </Card>

       {/* Trainer Management Section */}
        {isTrainerOwner && (
            <Card>
                 <CardHeader>
                    <CardTitle className="flex items-center">
                         <Users className="mr-2 h-5 w-5"/> {t('trainingDetails.manageRegistrations')}
                    </CardTitle>
                     <CardDescription className="flex flex-col sm:flex-row sm:gap-4">
                        <span>{t('trainingDetails.registrationTypeLabel')} {translatedRegType}</span>
                        <span>{capacityString}</span>
                         {waiting > 0 && <span>{waitingListString}</span>}
                    </CardDescription>
                 </CardHeader>
                <CardContent>
                   <RegistrationManagement
                        training={training}
                        trainerId={currentUser.id}
                        locale={params.locale}
                    />

                   {/* Placeholder buttons for editing/deleting the training itself */}
                    <Separator className="my-4" />
                     <div className="flex gap-2">
                        <Button variant="outline" size="sm" disabled> {/* Disabled for now */}
                            <Edit className="mr-2 h-4 w-4"/> {t('trainingDetails.editTraining')}
                        </Button>
                         <Button variant="destructive" size="sm" disabled> {/* Disabled for now */}
                            <Trash2 className="mr-2 h-4 w-4"/> {t('trainingDetails.deleteTraining')}
                         </Button>
                    </div>
                </CardContent>
            </Card>
        )}


    </div>
  );
}
