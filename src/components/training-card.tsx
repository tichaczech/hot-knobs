
'use client'; // Add 'use client' directive

import Link from 'next/link';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Calendar, MapPin, Users, BarChart, AlertCircle, CheckCircle, Bike } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import type { TrainingSession, User as AppUser, SkillLevel, MotorcycleType } from "@/lib/types"; // Import SkillLevel and MotorcycleType
import { format } from 'date-fns';
import { RegisterButton } from './register-button';
import { UnregisterButton } from './unregister-button';
import { useI18n } from '@/locales/client'; // Import client-side i18n hook

interface TrainingCardProps {
  training: TrainingSession;
  currentUser: AppUser | null;
  showRegisterButton?: boolean; // Rider view
  showUnregisterButton?: boolean; // My Trainings view
  showViewDetailsLink?: boolean; // General purpose link
}

// Helper function to translate SkillLevel safely
const translateSkillLevel = (level: SkillLevel, t: ReturnType<typeof useI18n>): string => {
  try {
    return t(`skillLevels.${level}`);
  } catch (e) {
    console.warn(`Missing translation for skill level: ${level}`);
    return level; // Fallback to the key
  }
};

// Helper function to translate MotorcycleType safely
const translateMotorcycleType = (type: MotorcycleType, t: ReturnType<typeof useI18n>): string => {
    try {
      return t(`motorcycleTypes.${type}`);
    } catch (e) {
      console.warn(`Missing translation for motorcycle type: ${type}`);
      return type; // Fallback to the key
    }
};

export function TrainingCard({
  training,
  currentUser,
  showRegisterButton = false,
  showUnregisterButton = false,
  showViewDetailsLink = true,
}: TrainingCardProps) {
  const t = useI18n(); // Get translation function

  const isRegistered = currentUser && training.registeredRiders?.includes(currentUser.id);
  const isFull = training.maxRiders !== undefined && training.registeredRiders && training.registeredRiders.length >= training.maxRiders;
  const spotsLeft = training.maxRiders !== undefined ? training.maxRiders - (training.registeredRiders?.length ?? 0) : Infinity;
  const spotText = spotsLeft !== 1 ? t('trainingCard.spots') : t('trainingCard.spot');

  // Handle potential undefined maxRiders for display logic
   const spotsDisplay = training.maxRiders !== undefined
        ? t('trainingCard.registered', { count: training.registeredRiders?.length ?? 0, max: training.maxRiders, spotsLeft: spotsLeft > 0 ? spotsLeft : 0, spotText: spotsLeft > 0 ? spotText : '' }).replace(' 0 ','').trim() // Remove extra space if 0 spots left
        : t('trainingCard.registeredOpen', { count: training.registeredRiders?.length ?? 0 });

  return (
    <Card className="flex flex-col h-full shadow-md hover:shadow-lg transition-shadow duration-200">
      <CardHeader>
        <CardTitle className="text-primary">{training.title}</CardTitle>
        <CardDescription>{t('trainingCard.taughtBy', { trainerName: training.trainerName })}</CardDescription>
      </CardHeader>
      <CardContent className="flex-grow space-y-3">
        <div className="flex items-center text-sm text-muted-foreground">
          <Calendar className="mr-2 h-4 w-4" />
          {/* TODO: Consider locale-aware date formatting */}
          <span>{format(training.date, 'PPP p')}</span> {/* e.g., Jun 21, 2024 10:00 AM */}
        </div>
        <div className="flex items-center text-sm text-muted-foreground">
          <MapPin className="mr-2 h-4 w-4" />
          <span>{training.locationName}</span>
        </div>
         <div className="flex items-start text-sm text-muted-foreground">
          <BarChart className="mr-2 h-4 w-4 shrink-0 mt-0.5" />
          <div className="flex flex-wrap gap-1">
             <span className="mr-1">{t('trainingCard.levels')}</span>
              {training.skillLevels?.map(level => (
                  // Translate skill level
                  <Badge key={level} variant="secondary" className="whitespace-nowrap">{translateSkillLevel(level, t)}</Badge>
              ))}
          </div>
        </div>
        {/* Display Motorcycle Types */}
         <div className="flex items-start text-sm text-muted-foreground">
          <Bike className="mr-2 h-4 w-4 shrink-0 mt-0.5" />
          <div className="flex flex-wrap gap-1">
             <span className="mr-1">{t('trainingCard.bikes')}</span>
              {training.motorcycleTypes?.map(type => (
                  // Translate motorcycle type
                  <Badge key={type} variant="outline" className="whitespace-nowrap">{translateMotorcycleType(type, t)}</Badge>
              ))}
          </div>
        </div>
         <div className="flex items-center text-sm text-muted-foreground">
          <Users className="mr-2 h-4 w-4" />
          <span>{isFull ? t('trainingCard.full') : spotsDisplay}</span>
        </div>
        <p className="text-sm line-clamp-3">{training.description}</p>
      </CardContent>
      <CardFooter className="flex justify-between items-center mt-auto pt-4 border-t">
         {showViewDetailsLink && (
             <Link href={`/trainings/${training.id}`} passHref legacyBehavior>
              <Button variant="link" size="sm">{t('viewDetails')}</Button>
            </Link>
         )}
        <div className="flex gap-2">
            {currentUser?.role === 'rider' && showRegisterButton && (
              <>
                {isRegistered ? (
                   <Badge variant="default" className="bg-green-600 hover:bg-green-700">
                     <CheckCircle className="mr-1 h-4 w-4" /> {t('trainingCard.registeredBadge')}
                   </Badge>
                ) : isFull ? (
                    <Badge variant="destructive">
                      <AlertCircle className="mr-1 h-4 w-4" /> {t('trainingCard.full')}
                    </Badge>
                ) : (
                    <RegisterButton trainingId={training.id} userId={currentUser.id} />
                )}
              </>
            )}
            {currentUser?.role === 'rider' && showUnregisterButton && isRegistered && (
                 <UnregisterButton trainingId={training.id} userId={currentUser.id} />
            )}
             {/* Maybe add edit/delete for trainers in the future */}
        </div>
      </CardFooter>
    </Card>
  );
}
