import { getTrainingById, getCurrentUser, placeholderUsers } from '@/lib/placeholder-data'; // Import users for lookup
import { TrainingCard } from '@/components/training-card';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { ArrowLeft, MapPin, CheckCircle, Clock, XCircle, UserX, Users, Edit, Trash2, UserCheck, Ban } from 'lucide-react'; // Added status icons, management icons
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type
import type { SkillLevel, RegistrationType } from '@/lib/types'; // Import SkillLevel type & RegistrationType
import { Badge } from '@/components/ui/badge';
import { Separator } from '@/components/ui/separator'; // Import Separator
import { RegistrationManagement } from './_components/registration-management'; // Import new component

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

// Helper function to translate RegistrationType safely
const translateRegistrationType = async (type: RegistrationType, locale: Locale): Promise<string> => {
    const t = await getI18n(locale);
    try {
        return t(`registrationTypes.${type}`);
    } catch (e) {
        console.warn(`Missing translation for registration type: ${type} in locale: ${locale}`);
        return type; // Fallback to the key
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

   // Determine user's status relative to this training
   const isRegistered = !!currentUser && !!training.registeredRiders?.includes(currentUser.id);
   const isPending = !!currentUser && training.registrationType === 'closed' && !!training.pendingRegistrations?.includes(currentUser.id);
   const isRejected = !!currentUser && training.registrationType === 'closed' && !!training.rejectedRegistrations?.some(r => r.userId === currentUser.id);
   const rejectionReason = isRejected ? training.rejectedRegistrations?.find(r => r.userId === currentUser.id)?.reason : undefined;
   const isCancelled = !!currentUser && training.registrationType === 'open' && !!training.cancelledRegistrations?.some(c => c.userId === currentUser.id);
   const cancellationReason = isCancelled ? training.cancelledRegistrations?.find(c => c.userId === currentUser.id)?.reason : undefined;

   const canRegister = currentUser?.role === 'rider' && !isRegistered && !isPending && !isRejected && !isCancelled;
   const canUnregister = currentUser?.role === 'rider' && (isRegistered || isPending); // Can unregister if approved or pending
   const isTrainerOwner = currentUser?.role === 'trainer' && currentUser.id === training.trainerId;

   const translatedRegType = await translateRegistrationType(training.registrationType, params.locale);

    // Fetch user details for pending/registered lists (example placeholder lookup)
    const getUsername = (userId: string) => placeholderUsers.find(u => u.id === userId)?.name || 'Unknown User';

    const pendingUsernames = (training.pendingRegistrations ?? []).map(getUsername);
    const registeredUsernames = (training.registeredRiders ?? []).map(getUsername);

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
          showRegisterButton={canRegister} // Show if rider can register
          showUnregisterButton={canUnregister} // Show if rider can unregister/withdraw
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

        {/* Card for User's Registration Status */}
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
                   {isRegistered && (
                     <Badge variant="default" className="bg-green-600 hover:bg-green-700 text-base">
                       <CheckCircle className="mr-2 h-4 w-4" /> {t('trainingDetails.approved')}
                     </Badge>
                   )}
                   {isPending && (
                     <Badge variant="secondary" className="text-yellow-800 bg-yellow-100 border-yellow-300 text-base">
                       <Clock className="mr-2 h-4 w-4" /> {t('trainingDetails.pendingApproval')}
                     </Badge>
                   )}
                   {isRejected && (
                     <div className="space-y-1">
                         <Badge variant="destructive" className="text-base">
                            <XCircle className="mr-2 h-4 w-4" /> {t('trainingDetails.rejected')}
                        </Badge>
                        {rejectionReason && <p className="text-sm text-muted-foreground">{t('reason')}: {rejectionReason}</p>}
                     </div>
                   )}
                    {isCancelled && (
                     <div className="space-y-1">
                         <Badge variant="destructive" className="text-base">
                            <Ban className="mr-2 h-4 w-4" /> {t('trainingDetails.cancelled')}
                        </Badge>
                        {cancellationReason && <p className="text-sm text-muted-foreground">{t('reason')}: {cancellationReason}</p>}
                     </div>
                   )}
                    {!isRegistered && !isPending && !isRejected && !isCancelled && (
                         <p className="text-sm text-muted-foreground">Not registered.</p>
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
                     <CardDescription>
                        {t('trainingDetails.registrationTypeLabel')} {translatedRegType}
                    </CardDescription>
                 </CardHeader>
                <CardContent>
                   <RegistrationManagement
                        training={training}
                        trainerId={currentUser.id}
                        locale={params.locale} // Pass locale for translations inside management component
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
