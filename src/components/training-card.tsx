'use client';

import Link from 'next/link';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Calendar, MapPin, Users, BarChart, AlertCircle, CheckCircle, Bike, Lock, Unlock, Clock, ListChecks, Hourglass, Ban, UserCheck, UserX } from "lucide-react"; // Added status icons
import { Badge } from "@/components/ui/badge";
import type { TrainingSession, User as AppUser, SkillLevel, MotorcycleType, RegistrationStatus } from "@/lib/types"; // Import SkillLevel and MotorcycleType
import { format } from 'date-fns';
import { RegisterButton } from './register-button';
import { UnregisterButton } from './unregister-button';
import { useI18n } from '@/locales/client';
import { getUserRegistration, getRegistrationCounts, isTrainingFull } from '@/lib/placeholder-data'; // Import helpers
import React, { useState, useEffect } from 'react'; // Import React, useState, useEffect

interface TrainingCardProps {
  training: TrainingSession;
  currentUser: AppUser | null;
  showRegisterButton?: boolean; // Should the register button potentially be shown?
  showCancelButton?: boolean; // Should the cancel button potentially be shown? (Replaces showUnregisterButton)
  showViewDetailsLink?: boolean; // General purpose link
  // showViewStatusLink is removed as status is shown directly
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

// Helper function to translate RegistrationStatus safely
const translateRegistrationStatus = (status: RegistrationStatus, t: ReturnType<typeof useI18n>): string => {
    try {
        return t(`registrationStatuses.${status}`);
    } catch (e) {
        console.warn(`Missing translation for registration status: ${status}`);
        return status; // Fallback
    }
};

export function TrainingCard({
  training,
  currentUser,
  showRegisterButton = false,
  showCancelButton = false,
  showViewDetailsLink = true,
}: TrainingCardProps) {
  const t = useI18n(); // Get translation function
  const [formattedDate, setFormattedDate] = useState<string | null>(null);

  // Format date on client after mount to avoid hydration mismatch
  useEffect(() => {
    // Ensure training.date is a valid Date object before formatting
    if (training.date) {
        try {
            // Attempt to format. Use 'en-US' locale explicitly if needed, or rely on environment
            const dateObj = new Date(training.date); // Ensure it's a Date object
            setFormattedDate(format(dateObj, 'PPP p'));
        } catch (error) {
            console.error("Error formatting date:", error);
            // Fallback or set an error state if needed
            setFormattedDate(training.date.toString()); // Simple fallback
        }
    }
  }, [training.date]);


  const userRegistration = getUserRegistration(training, currentUser?.id);
  const userStatus = userRegistration?.status;
  const userReason = userRegistration?.reason;

  const { confirmed, waiting } = getRegistrationCounts(training);
  const isFull = isTrainingFull(training);
  const spotsLeft = training.maxRiders !== undefined ? training.maxRiders - confirmed : Infinity;
  const spotText = spotsLeft !== 1 ? t('trainingCard.spots') : t('trainingCard.spot');

  const spotsDisplay = training.maxRiders !== undefined
        ? `${t('trainingCard.registeredCount', { count: confirmed })} / ${training.maxRiders}` + (waiting > 0 ? ` (${t('trainingCard.waitingCount', { count: waiting })})` : '')
        : t('trainingCard.registeredOpen', { count: confirmed });

   // Registration type display - Use uppercase variable name for the component type
   const RegistrationTypeIcon = training.registrationType === 'closed' ? Lock : Unlock;

   // Determine if the register button should be shown
   // Can register IF: showRegisterButton is true, user is a rider, AND
   // user does NOT have an 'active' registration (Confirmed, Created, or Waiting)
   const canRegister = showRegisterButton &&
                      currentUser?.role === 'rider' &&
                      !(userStatus === 'Confirmed' || userStatus === 'Created' || userStatus === 'Waiting');

   // Determine if the cancel button should be shown
   const canCancel = showCancelButton &&
                      currentUser?.role === 'rider' &&
                      userStatus &&
                      (userStatus === 'Confirmed' || userStatus === 'Created' || userStatus === 'Waiting');

  // --- Status Badge Logic ---
    let statusBadge = null;
    if (currentUser?.role === 'rider' && userStatus) {
        const translatedStatus = translateRegistrationStatus(userStatus, t);
        switch (userStatus) {
            case 'Confirmed':
                statusBadge = <Badge variant="default" className="bg-green-600 hover:bg-green-700"><CheckCircle className="mr-1 h-4 w-4" /> {translatedStatus}</Badge>;
                break;
            case 'Created':
                statusBadge = <Badge variant="secondary" className="text-blue-800 bg-blue-100 border-blue-300"><ListChecks className="mr-1 h-4 w-4" /> {translatedStatus}</Badge>;
                break;
            case 'Waiting':
                statusBadge = <Badge variant="secondary" className="text-yellow-800 bg-yellow-100 border-yellow-300"><Hourglass className="mr-1 h-4 w-4" /> {translatedStatus}</Badge>;
                break;
            case 'Rejected':
                statusBadge = <Badge variant="destructive"><UserX className="mr-1 h-4 w-4" /> {translatedStatus}</Badge>;
                break;
            case 'Cancelled':
                 statusBadge = <Badge variant="outline" className="text-muted-foreground"><Ban className="mr-1 h-4 w-4" /> {translatedStatus}</Badge>;
                break;
            default:
                 statusBadge = <Badge variant="secondary">{translatedStatus}</Badge>;
        }
    }
    // Show "Full" badge only if rider cannot register and it's actually full (open training only for card)
    else if (isFull && !canRegister && training.registrationType === 'open') {
        statusBadge = <Badge variant="destructive"><AlertCircle className="mr-1 h-4 w-4" /> {t('trainingCard.full')}</Badge>
    }
    // --- End Status Badge Logic ---


  return (
    <Card className="flex flex-col h-full shadow-md hover:shadow-lg transition-shadow duration-200">
      <CardHeader>
        <div className="flex justify-between items-start gap-2">
            <div className="flex-1">
                <CardTitle className="text-primary">{training.title}</CardTitle>
                <CardDescription>{t('trainingCard.taughtBy', { trainerName: training.trainerName })}</CardDescription>
            </div>
             <Badge variant={training.registrationType === 'closed' ? 'secondary' : 'outline'} className="ml-auto whitespace-nowrap shrink-0">
                {/* Render the component using the uppercase variable */}
                <RegistrationTypeIcon className="mr-1 h-3 w-3"/>
                {/* Display translated registration type text - Fetch translation */}
                {training.registrationType === 'closed' ? t('registrationTypes.closed') : t('registrationTypes.open')}
            </Badge>
        </div>
      </CardHeader>
      <CardContent className="flex-grow space-y-3">
        <div className="flex items-center text-sm text-muted-foreground">
          <Calendar className="mr-2 h-4 w-4" />
          {/* Display formatted date from state, or loading/fallback */}
          <span>{formattedDate || t('loading')}</span>
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
                  <Badge key={level} variant="secondary" className="whitespace-nowrap">{translateSkillLevel(level, t)}</Badge>
              ))}
          </div>
        </div>
         <div className="flex items-start text-sm text-muted-foreground">
          <Bike className="mr-2 h-4 w-4 shrink-0 mt-0.5" />
          <div className="flex flex-wrap gap-1">
             <span className="mr-1">{t('trainingCard.bikes')}</span>
              {training.motorcycleTypes?.map(type => (
                  <Badge key={type} variant="outline" className="whitespace-nowrap">{translateMotorcycleType(type, t)}</Badge>
              ))}
          </div>
        </div>
         <div className="flex items-center text-sm text-muted-foreground">
          <Users className="mr-2 h-4 w-4" />
          <span>{spotsDisplay}</span>
        </div>
        <p className="text-sm line-clamp-3">{training.description}</p>

         {/* Show reason for rejection/cancellation if applicable */}
         {userReason && (userStatus === 'Rejected' || userStatus === 'Cancelled') && (
            <p className="text-xs text-muted-foreground border-l-2 pl-2 italic">
                {t('reason')}: {userReason}
            </p>
         )}
      </CardContent>
      <CardFooter className="flex justify-between items-center mt-auto pt-4 border-t">
         {showViewDetailsLink && (
             <Link href={`/trainings/${training.id}`} passHref legacyBehavior>
              <Button variant="link" size="sm">{t('viewDetails')}</Button>
            </Link>
         )}
         {/* Placeholder for non-details-link space if needed */}
         {!showViewDetailsLink && <div></div>}

        <div className="flex gap-2 items-center">
            {/* Render status badge first */}
            {statusBadge}

            {/* Render buttons based on calculated permissions */}
             {canRegister && (
                <RegisterButton
                    trainingId={training.id}
                    userId={currentUser!.id} // Should be safe due to canRegister check
                    registrationType={training.registrationType}
                    isFull={isFull} // Pass full status
                />
            )}

             {canCancel && (
                <UnregisterButton
                    trainingId={training.id}
                    userId={currentUser!.id} // Should be safe due to canCancel check
                    currentStatus={userStatus!} // Should be safe due to canCancel check
                />
            )}
        </div>
      </CardFooter>
    </Card>
  );
}
