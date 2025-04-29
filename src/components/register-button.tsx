'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { registerForTraining } from '@/lib/placeholder-data';
import { Loader2, CheckCircle, Send, Hourglass } from 'lucide-react'; // Added Hourglass for waiting list
import { useRouter } from 'next/navigation';
import { useI18n } from '@/locales/client';
import type { RegistrationType, RegistrationResult, RegistrationStatus } from '@/lib/types';


interface RegisterButtonProps {
  trainingId: string;
  userId: string;
  registrationType: RegistrationType;
  isFull: boolean; // Is the training currently full (confirmed == maxRiders)?
}

export function RegisterButton({ trainingId, userId, registrationType, isFull }: RegisterButtonProps) {
  const t = useI18n();
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  // Determine button text and icon based on registration type and fullness
  let buttonTextKey = 'registerButton.register'; // Default for open, not full
  let ButtonIcon = CheckCircle;
  let toastSuccessTitleKey = 'registerButton.registrationSuccessTitle';
  let toastSuccessDescKey = 'registerButton.registrationSuccessDesc';

  if (registrationType === 'closed') {
      buttonTextKey = 'registerButton.requestRegistration';
      ButtonIcon = Send;
      toastSuccessTitleKey = 'registerButton.registrationPendingTitle'; // Closed always starts as 'Created' (pending)
      toastSuccessDescKey = 'registerButton.registrationPendingDesc';
  } else if (isFull) { // Open and Full
      buttonTextKey = 'registerButton.joinWaitingList';
      ButtonIcon = Hourglass;
      toastSuccessTitleKey = 'registerButton.waitingListSuccessTitle';
      toastSuccessDescKey = 'registerButton.waitingListSuccessDesc';
  }

  const buttonText = t(buttonTextKey);
  const toastSuccessTitle = t(toastSuccessTitleKey);
  // Error messages remain the same regardless of initial action type
  const toastErrorTitle = t('registerButton.registrationErrorTitle');
  const toastErrorDescKey = 'registerButton.registrationErrorDesc';

  const handleRegister = () => {
    startTransition(async () => {
      const result: RegistrationResult = await registerForTraining(userId, trainingId);
      if (result.success) {
          // Use appropriate success message based on the *actual* resulting status
           let finalSuccessTitle = toastSuccessTitle; // Default to pre-calculated title
           let finalSuccessDescKey = toastSuccessDescKey; // Default to pre-calculated desc key

          if (result.status === 'Confirmed') {
               finalSuccessTitle = t('registerButton.registrationSuccessTitle');
               finalSuccessDescKey = 'registerButton.registrationSuccessDesc';
          } else if (result.status === 'Waiting') {
              finalSuccessTitle = t('registerButton.waitingListSuccessTitle');
              finalSuccessDescKey = 'registerButton.waitingListSuccessDesc';
          } else if (result.status === 'Created') {
              finalSuccessTitle = t('registerButton.registrationPendingTitle');
              finalSuccessDescKey = 'registerButton.registrationPendingDesc';
          }
          // Add more else if blocks here if other success scenarios arise

          toast({
             title: finalSuccessTitle,
             description: t(finalSuccessDescKey, { message: result.message || t('success') }),
             variant: "default",
             // Optional: Different styling based on status
             // className: result.status === 'Confirmed' ? "bg-primary text-primary-foreground" : undefined
          });
        router.refresh();
      } else {
        toast({
          title: toastErrorTitle,
          description: t(toastErrorDescKey, { message: result.message || t('error') }),
          variant: "destructive",
        });
      }
    });
  };

  return (
    <Button onClick={handleRegister} disabled={isPending} size="sm" className="bg-accent hover:bg-accent/90">
      {isPending ? (
        <Loader2 className="mr-2 h-4 w-4 animate-spin" />
      ) : (
        <ButtonIcon className="mr-2 h-4 w-4" />
      )}
      {buttonText}
    </Button>
  );
}
