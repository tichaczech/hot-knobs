'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { cancelMyRegistration } from '@/lib/placeholder-data'; // Use the new rider cancellation action
import { Loader2, XCircle, LogOut, Ban } from 'lucide-react'; // Use Ban icon for general cancellation
import { useRouter } from 'next/navigation';
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { useI18n } from '@/locales/client';
import type { RegistrationStatus, RegistrationResult } from '@/lib/types'; // Import types

// Renamed component to CancelButton
interface CancelButtonProps {
  trainingId: string;
  userId: string;
  currentStatus: RegistrationStatus; // Receive the user's current status
}

export function UnregisterButton({ trainingId, userId, currentStatus }: CancelButtonProps) {
  const t = useI18n();
  const [isSubmitting, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  // Determine button text, confirmation messages based on the current status
  let buttonTextKey = 'cancelButton.cancelRegistration'; // Default
  let confirmDescKey = 'cancelButton.confirmDesc';
  let confirmActionKey = 'cancelButton.confirmAction';
  let successTitleKey = 'cancelButton.cancelSuccessTitle';
  let successDescKey = 'cancelButton.cancelSuccessDesc';
  let errorTitleKey = 'cancelButton.cancelErrorTitle';
  let errorDescKey = 'cancelButton.cancelErrorDesc';

  if (currentStatus === 'Created') {
      buttonTextKey = 'cancelButton.withdrawRequest';
      confirmDescKey = 'cancelButton.confirmWithdrawDesc';
      confirmActionKey = 'cancelButton.confirmWithdrawAction';
      // Use same success/error messages as general cancellation
  } else if (currentStatus === 'Waiting') {
       buttonTextKey = 'cancelButton.leaveWaitingList';
       confirmDescKey = 'cancelButton.confirmLeaveWaitingListDesc';
       confirmActionKey = 'cancelButton.confirmLeaveWaitingListAction';
       // Use same success/error messages
  }
  // 'Confirmed' status uses the defaults

  const buttonText = t(buttonTextKey);
  const confirmTitle = t('cancelButton.confirmTitle'); // Title can be generic
  const confirmDesc = t(confirmDescKey);
  const confirmActionText = t(confirmActionKey);
  const successTitle = t(successTitleKey);
  const errorTitle = t(errorTitleKey);


  const handleAction = () => {
    startTransition(async () => {
      // Use the new rider-specific cancellation action
      const result: RegistrationResult = await cancelMyRegistration(userId, trainingId);
      if (result.success) {
        toast({
          title: successTitle,
          description: t(successDescKey, { message: result.message || t('success') }),
        });
         router.refresh();
      } else {
        toast({
          title: errorTitle,
          description: t(errorDescKey, { message: result.message || t('error') }),
          variant: "destructive",
        });
      }
    });
  };

  return (
     <AlertDialog>
      <AlertDialogTrigger asChild>
         {/* Use outline variant for cancellation */}
         <Button variant="outline" size="sm" disabled={isSubmitting}>
            {isSubmitting ? (
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            ) : (
                <Ban className="mr-2 h-4 w-4" /> // General cancel icon
            )}
            {buttonText}
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>{confirmTitle}</AlertDialogTitle>
          <AlertDialogDescription>
            {confirmDesc}
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>{t('cancel')}</AlertDialogCancel>
          <AlertDialogAction onClick={handleAction} disabled={isSubmitting} className="bg-destructive hover:bg-destructive/90">
             {isSubmitting ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : null}
            {confirmActionText}
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>

  );
}
