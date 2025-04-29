'use client';

import { useState, useTransition } from 'react';
import { Button } from '@/components/ui/button';
import { useToast } from '@/hooks/use-toast';
import { registerForTraining } from '@/lib/placeholder-data'; // Assuming server action or API call function
import { Loader2, CheckCircle } from 'lucide-react';
import { useRouter } from 'next/navigation';


interface RegisterButtonProps {
  trainingId: string;
  userId: string;
}

export function RegisterButton({ trainingId, userId }: RegisterButtonProps) {
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
   const router = useRouter();

  const handleRegister = () => {
    startTransition(async () => {
      const result = await registerForTraining(userId, trainingId);
      if (result.success) {
        toast({
          title: "Registration Successful!",
          description: result.message,
           variant: "default", // Use default styling (often green or neutral)
           className: "bg-primary text-primary-foreground"
        });
        router.refresh(); // Refresh data on the page
      } else {
        toast({
          title: "Registration Failed",
          description: result.message,
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
        <CheckCircle className="mr-2 h-4 w-4" />
      )}
      Register
    </Button>
  );
}
