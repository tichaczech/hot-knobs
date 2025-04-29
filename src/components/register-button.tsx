'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { registerForTraining } from '@/lib/placeholder-data'; // Assuming server action or API call function
import { Loader2, CheckCircle, Send } from 'lucide-react'; // Added Send icon
import { useRouter } from 'next/navigation';
import { useI18n } from '@/locales/client'; // Import client-side i18n hook
import type { RegistrationType } from '@/lib/types'; // Import RegistrationType


interface RegisterButtonProps {
  trainingId: string;
  userId: string;
  registrationType: RegistrationType; // Add registration type
}

export function RegisterButton({ trainingId, userId, registrationType }: RegisterButtonProps) {
  const t = useI18n(); // Get translation function
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  const isClosedRegistration = registrationType === 'closed';
  const buttonText = isClosedRegistration ? t('registerButton.requestRegistration') : t('registerButton.register');
  const ButtonIcon = isClosedRegistration ? Send : CheckCircle;

  const handleRegister = () => {
    startTransition(async () => {
      const result = await registerForTraining(userId, trainingId);
      if (result.success) {
          if (result.pending) {
             toast({
                title: t('registerButton.registrationPendingTitle'),
                description: t('registerButton.registrationPendingDesc', { message: result.message || t('success') }),
                 variant: "default",
            });
          } else {
             toast({
                title: t('registerButton.registrationSuccessTitle'),
                description: t('registerButton.registrationSuccessDesc', { message: result.message || t('success') }), // Provide fallback message
                 variant: "default", // Use default styling (often green or neutral)
                 className: "bg-primary text-primary-foreground"
            });
          }
        router.refresh(); // Refresh data on the page
      } else {
        toast({
          title: t('registerButton.registrationErrorTitle'),
          description: t('registerButton.registrationErrorDesc', { message: result.message || t('error') }), // Provide fallback message
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
