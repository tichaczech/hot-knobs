'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { unregisterFromTraining } from '@/lib/placeholder-data'; // Assuming server action or API call function
import { Loader2, XCircle, LogOut } from 'lucide-react'; // Added LogOut for withdraw
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
import { useI18n } from '@/locales/client'; // Import client-side i18n hook

interface UnregisterButtonProps {
  trainingId: string;
  userId: string;
  isPending: boolean; // Is the registration pending approval?
}

export function UnregisterButton({ trainingId, userId, isPending }: UnregisterButtonProps) {
  const t = useI18n(); // Get translation function
  const [isSubmitting, startTransition] = useTransition(); // Renamed for clarity
  const { toast } = useToast();
  const router = useRouter();

  const buttonText = isPending ? t('unregisterButton.withdrawRequest') : t('unregisterButton.unregister');
  const ButtonIcon = isPending ? LogOut : XCircle;
  const confirmTitle = isPending ? t('unregisterButton.confirmTitle') : t('unregisterButton.confirmTitle'); // Same title ok?
  const confirmDesc = isPending ? t('unregisterButton.confirmWithdrawDesc') : t('unregisterButton.confirmDesc');
  const confirmActionText = isPending ? t('unregisterButton.confirmWithdrawAction') : t('unregisterButton.confirmAction');
  const successTitle = isPending ? t('unregisterButton.withdrawSuccessTitle') : t('unregisterButton.unregistrationSuccessTitle');
  const errorTitle = isPending ? t('unregisterButton.withdrawErrorTitle') : t('unregisterButton.unregistrationErrorTitle');
  const successDescKey = isPending ? 'unregisterButton.withdrawSuccessDesc' : 'unregisterButton.unregistrationSuccessDesc';
  const errorDescKey = isPending ? 'unregisterButton.withdrawErrorDesc' : 'unregisterButton.unregistrationErrorDesc';


  const handleAction = () => {
    startTransition(async () => {
      const result = await unregisterFromTraining(userId, trainingId); // Same function handles both
      if (result.success) {
        toast({
          title: successTitle,
          description: t(successDescKey, { message: result.message || t('success') }),
        });
         router.refresh(); // Refresh data on the page
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
         <Button variant="outline" size="sm" disabled={isSubmitting}>
            {isSubmitting ? (
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            ) : (
                <ButtonIcon className="mr-2 h-4 w-4" />
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
