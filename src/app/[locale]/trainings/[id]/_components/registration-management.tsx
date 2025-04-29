
'use client';

import React, { useState, useTransition } from 'react';
import { TrainingSession, User } from '@/lib/types';
import { Button } from '@/components/ui/button';
import { Textarea } from '@/components/ui/textarea';
import {
  approveRegistration,
  rejectRegistration,
  cancelOpenRegistration,
  placeholderUsers // Import users for lookup (replace with actual fetch if needed)
} from '@/lib/placeholder-data';
import { useToast } from '@/hooks/use-toast';
import { useRouter } from 'next/navigation';
import { Loader2, UserCheck, UserX, Ban, Send } from 'lucide-react';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Separator } from '@/components/ui/separator';
import { useI18n } from '@/locales/client'; // Import client i18n hook
import type { Locale } from '@/locales/config'; // Import Locale type


interface RegistrationManagementProps {
    training: TrainingSession;
    trainerId: string;
    locale: Locale; // Receive locale for translations
}

export function RegistrationManagement({ training, trainerId, locale }: RegistrationManagementProps) {
    const t = useI18n(); // Use the hook with the passed locale implicitly
    const [isPendingAction, startTransition] = useTransition();
    const [rejectionReason, setRejectionReason] = useState<{ [userId: string]: string }>({});
     const [cancellationReason, setCancellationReason] = useState<{ [userId: string]: string }>({});
    const { toast } = useToast();
    const router = useRouter();

     // Helper to get username (replace with actual data fetching if necessary)
    const getUsername = (userId: string): string => {
        return placeholderUsers.find(u => u.id === userId)?.name || userId;
    };

    const handleApprove = (riderId: string) => {
        startTransition(async () => {
            const result = await approveRegistration(trainerId, training.id, riderId);
            if (result.success) {
                 toast({ title: t('trainerActions.approveSuccessTitle'), description: t('trainerActions.approveSuccessDesc') });
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.approveErrorTitle'), description: t('trainerActions.approveErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };

    const handleReject = (riderId: string) => {
        startTransition(async () => {
             const reason = rejectionReason[riderId] || undefined;
            const result = await rejectRegistration(trainerId, training.id, riderId, reason);
             if (result.success) {
                 toast({ title: t('trainerActions.rejectSuccessTitle'), description: t('trainerActions.rejectSuccessDesc') });
                 setRejectionReason(prev => ({ ...prev, [riderId]: '' })); // Clear reason after submission
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.rejectErrorTitle'), description: t('trainerActions.rejectErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };

     const handleCancel = (riderId: string) => {
        startTransition(async () => {
             const reason = cancellationReason[riderId] || undefined;
            const result = await cancelOpenRegistration(trainerId, training.id, riderId, reason);
             if (result.success) {
                 toast({ title: t('trainerActions.cancelSuccessTitle'), description: t('trainerActions.cancelSuccessDesc') });
                 setCancellationReason(prev => ({ ...prev, [riderId]: '' })); // Clear reason after submission
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.cancelErrorTitle'), description: t('trainerActions.cancelErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };


    return (
        <div className="space-y-6">
            {/* Pending Approvals (Only for Closed Trainings) */}
            {training.registrationType === 'closed' && (
                <div>
                    <h3 className="text-lg font-semibold mb-2">{t('trainingDetails.pending')} ({training.pendingRegistrations?.length ?? 0})</h3>
                    {training.pendingRegistrations && training.pendingRegistrations.length > 0 ? (
                        <ul className="space-y-4">
                            {training.pendingRegistrations.map(riderId => (
                                <li key={riderId} className="p-3 border rounded-md bg-muted/50 space-y-2">
                                    <div className="flex justify-between items-center">
                                        <span>{getUsername(riderId)}</span>
                                        <div className="flex gap-2">
                                            <Button
                                                size="sm"
                                                variant="outline"
                                                className="bg-green-500 hover:bg-green-600 text-white border-green-600"
                                                onClick={() => handleApprove(riderId)}
                                                disabled={isPendingAction}
                                            >
                                                {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <UserCheck className="h-4 w-4" />}
                                                <span className="ml-1">{t('trainingDetails.approve')}</span>
                                            </Button>
                                            <Button
                                                size="sm"
                                                variant="outline"
                                                className="bg-red-500 hover:bg-red-600 text-white border-red-600"
                                                onClick={() => handleReject(riderId)}
                                                disabled={isPendingAction}
                                             >
                                                 {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <UserX className="h-4 w-4" />}
                                                 <span className="ml-1">{t('trainingDetails.reject')}</span>
                                            </Button>
                                        </div>
                                    </div>
                                    {/* Rejection Reason Input */}
                                    <div>
                                         <Textarea
                                            placeholder={t('trainingDetails.rejectionReasonPlaceholder')}
                                            value={rejectionReason[riderId] || ''}
                                            onChange={(e) => setRejectionReason(prev => ({ ...prev, [riderId]: e.target.value }))}
                                            className="text-sm h-16"
                                            disabled={isPendingAction}
                                        />
                                     </div>
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p className="text-sm text-muted-foreground">{t('myTrainings.noPendingRegistrations')}</p>
                    )}
                </div>
            )}

            {/* Registered Riders */}
            <div>
                 <h3 className="text-lg font-semibold mb-2">{t('trainingDetails.registered')} ({training.registeredRiders?.length ?? 0} / {training.maxRiders ?? '∞'})</h3>
                 {training.registeredRiders && training.registeredRiders.length > 0 ? (
                    <ul className="space-y-4">
                        {training.registeredRiders.map(riderId => (
                             <li key={riderId} className="p-3 border rounded-md space-y-2">
                                 <div className="flex justify-between items-center">
                                    <span>{getUsername(riderId)}</span>
                                    {/* Cancel Button (Only for Open Trainings) */}
                                    {training.registrationType === 'open' && (
                                        <Button
                                            size="sm"
                                            variant="outline"
                                            className="text-red-600 border-red-500 hover:bg-red-50"
                                            onClick={() => handleCancel(riderId)}
                                            disabled={isPendingAction}
                                        >
                                            {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <Ban className="h-4 w-4" />}
                                             <span className="ml-1">{t('trainingDetails.cancelRegistration')}</span>
                                        </Button>
                                     )}
                                </div>
                                 {/* Cancellation Reason Input (Only for Open Trainings) */}
                                {training.registrationType === 'open' && (
                                      <div>
                                         <Textarea
                                            placeholder={t('trainingDetails.cancellationReasonPlaceholder')}
                                            value={cancellationReason[riderId] || ''}
                                            onChange={(e) => setCancellationReason(prev => ({ ...prev, [riderId]: e.target.value }))}
                                            className="text-sm h-16"
                                            disabled={isPendingAction}
                                        />
                                     </div>
                                )}
                            </li>
                        ))}
                    </ul>
                 ) : (
                    <p className="text-sm text-muted-foreground">No riders registered yet.</p>
                 )}
            </div>

            {/* Optionally show rejected/cancelled lists */}
            {/*
             <Separator className="my-4" />
             <div>
                <h4 className="text-md font-semibold mb-1">Rejected Riders</h4>
                 {training.rejectedRegistrations && training.rejectedRegistrations.length > 0 ? (
                    <ul>{training.rejectedRegistrations.map(r => <li key={r.userId}>{getUsername(r.userId)} ({r.reason || 'No reason'})</li>)}</ul>
                 ) : (<p>None</p>)}
            </div>
             <div>
                 <h4 className="text-md font-semibold mb-1">Cancelled Riders (Open Training)</h4>
                 {training.cancelledRegistrations && training.cancelledRegistrations.length > 0 ? (
                    <ul>{training.cancelledRegistrations.map(c => <li key={c.userId}>{getUsername(c.userId)} ({c.reason || 'No reason'})</li>)}</ul>
                 ) : (<p>None</p>)}
             </div>
            */}

        </div>
    );
}
