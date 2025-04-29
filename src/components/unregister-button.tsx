'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { unregisterFromTraining } from '@/lib/placeholder-data'; // Assuming server action or API call function
import { Loader2, XCircle } from 'lucide-react';
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
}

export function UnregisterButton({ trainingId, userId }: UnregisterButtonProps) {
  const t = useI18n(); // Get translation function
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  const handleUnregister = () => {
    startTransition(async () => {
      const result = await unregisterFromTraining(userId, trainingId);
      if (result.success) {
        toast({
          title: t('unregisterButton.unregistrationSuccessTitle'),
          description: t('unregisterButton.unregistrationSuccessDesc', { message: result.message || t('success') }),
        });
         router.refresh(); // Refresh data on the page
      } else {
        toast({
          title: t('unregisterButton.unregistrationErrorTitle'),
          description: t('unregisterButton.unregistrationErrorDesc', { message: result.message || t('error') }),
          variant: "destructive",
        });
      }
    });
  };

  return (
     <AlertDialog>
      <AlertDialogTrigger asChild>
         <Button variant="outline" size="sm" disabled={isPending}>
            {isPending ? (
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            ) : (
                <XCircle className="mr-2 h-4 w-4" />
            )}
            {t('unregisterButton.unregister')}
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>{t('unregisterButton.confirmTitle')}</AlertDialogTitle>
          <AlertDialogDescription>
            {t('unregisterButton.confirmDesc')}
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>{t('cancel')}</AlertDialogCancel>
          <AlertDialogAction onClick={handleUnregister} disabled={isPending} className="bg-destructive hover:bg-destructive/90">
             {isPending ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : null}
            {t('unregisterButton.confirmAction')}
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>

  );
}
