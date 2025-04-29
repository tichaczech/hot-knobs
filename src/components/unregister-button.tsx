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


interface UnregisterButtonProps {
  trainingId: string;
  userId: string;
}

export function UnregisterButton({ trainingId, userId }: UnregisterButtonProps) {
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  const handleUnregister = () => {
    startTransition(async () => {
      const result = await unregisterFromTraining(userId, trainingId);
      if (result.success) {
        toast({
          title: "Unregistration Successful!",
          description: result.message,
        });
         router.refresh(); // Refresh data on the page
      } else {
        toast({
          title: "Unregistration Failed",
          description: result.message,
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
            Unregister
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. You will be removed from the registration list for this training session.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <AlertDialogAction onClick={handleUnregister} disabled={isPending} className="bg-destructive hover:bg-destructive/90">
             {isPending ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : null}
            Confirm Unregistration
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>

  );
}
