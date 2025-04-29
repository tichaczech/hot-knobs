'use client';

import React, { useState, useTransition } from 'react';
import { TrainingSession, User, Registration, RegistrationStatus } from '@/lib/types';
import { Button } from '@/components/ui/button';
import { Textarea } from '@/components/ui/textarea';
import {
    approveRegistration,
    rejectRegistration,
    cancelConfirmedRegistration,
    placeholderUsers, // Import users for lookup (replace with actual fetch if needed)
    getRegistrationCounts,
    getUserRegistration,
} from '@/lib/placeholder-data';
import { useToast } from '@/hooks/use-toast';
import { useRouter } from 'next/navigation';
import { Loader2, UserCheck, UserX, Ban, Send, Hourglass, ListChecks, CheckCircle } from 'lucide-react'; // Added status icons
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Separator } from '@/components/ui/separator';
import { Badge } from '@/components/ui/badge'; // Import Badge
import { useI18n } from '@/locales/client';
import type { Locale } from '@/locales/config';

interface RegistrationManagementProps {
    training: TrainingSession;
    trainerId: string;
    locale: Locale;
}

// Helper component for rendering a single registration item
const RegistrationItem: React.FC<{
    registration: Registration;
    trainingId: string;
    trainerId: string;
    maxRiders?: number;
    confirmedCount: number;
    isPendingAction: boolean;
    onApprove: (riderId: string) => void;
    onReject: (riderId: string, reason?: string) => void;
    onCancel: (riderId: string, reason?: string) => void;
    getUsername: (userId: string) => string;
    translateStatus: (status: RegistrationStatus) => string;
    t: ReturnType<typeof useI18n>;
}> = ({
    registration,
    trainingId,
    trainerId,
    maxRiders,
    confirmedCount,
    isPendingAction,
    onApprove,
    onReject,
    onCancel,
    getUsername,
    translateStatus,
    t,
}) => {
    const [reason, setReason] = useState('');
    const isFull = maxRiders !== undefined && confirmedCount >= maxRiders;

    const handleApproveClick = () => onApprove(registration.userId);
    const handleRejectClick = () => onReject(registration.userId, reason);
    const handleCancelClick = () => onCancel(registration.userId, reason);

    const StatusBadge: React.FC<{ status: RegistrationStatus }> = ({ status }) => {
         const translatedStatus = translateStatus(status);
         switch (status) {
            case 'Confirmed': return <Badge variant="default" className="bg-green-600 hover:bg-green-700"><CheckCircle className="mr-1 h-3 w-3"/>{translatedStatus}</Badge>;
            case 'Created': return <Badge variant="secondary" className="text-blue-800 bg-blue-100 border-blue-300"><ListChecks className="mr-1 h-3 w-3"/>{translatedStatus}</Badge>;
            case 'Waiting': return <Badge variant="secondary" className="text-yellow-800 bg-yellow-100 border-yellow-300"><Hourglass className="mr-1 h-3 w-3"/>{translatedStatus}</Badge>;
            case 'Rejected': return <Badge variant="destructive"><UserX className="mr-1 h-3 w-3"/>{translatedStatus}</Badge>;
            case 'Cancelled': return <Badge variant="outline" className="text-muted-foreground"><Ban className="mr-1 h-3 w-3"/>{translatedStatus}</Badge>;
            default: return <Badge variant="secondary">{translatedStatus}</Badge>;
        }
    };

    return (
        <li className="p-3 border rounded-md bg-card space-y-2">
            <div className="flex justify-between items-center gap-2 flex-wrap">
                <div className="flex items-center gap-2">
                     <span className="font-medium">{getUsername(registration.userId)}</span>
                     <StatusBadge status={registration.status} />
                </div>

                {/* Action Buttons */}
                <div className="flex gap-2 flex-wrap">
                    {/* Approve Button (for Created or Waiting status) */}
                    {(registration.status === 'Created' || registration.status === 'Waiting') && (
                        <Button
                            size="sm"
                            variant="outline"
                            className="bg-green-500 hover:bg-green-600 text-white border-green-600"
                            onClick={handleApproveClick}
                            disabled={isPendingAction || (isFull && registration.status === 'Waiting')} // Disable approving waiting if full
                            title={isFull && registration.status === 'Waiting' ? t('trainerActions.approveWaitingFullTooltip') : t('trainerActions.approveTooltip')}
                        >
                            {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <UserCheck className="h-4 w-4" />}
                            <span className="ml-1">{t('trainingDetails.approve')}</span>
                        </Button>
                    )}

                    {/* Reject Button (for Created or Waiting status) */}
                     {(registration.status === 'Created' || registration.status === 'Waiting') && (
                        <Button
                            size="sm"
                            variant="outline"
                            className="bg-red-500 hover:bg-red-600 text-white border-red-600"
                            onClick={handleRejectClick}
                            disabled={isPendingAction}
                             title={t('trainerActions.rejectTooltip')}
                         >
                             {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <UserX className="h-4 w-4" />}
                             <span className="ml-1">{t('trainingDetails.reject')}</span>
                        </Button>
                    )}

                     {/* Cancel Button (for Confirmed status ONLY by trainer) */}
                     {registration.status === 'Confirmed' && (
                        <Button
                            size="sm"
                            variant="outline"
                            className="text-red-600 border-red-500 hover:bg-red-50"
                            onClick={handleCancelClick}
                            disabled={isPendingAction}
                             title={t('trainerActions.cancelTooltip')}
                        >
                            {isPendingAction ? <Loader2 className="h-4 w-4 animate-spin" /> : <Ban className="h-4 w-4" />}
                             <span className="ml-1">{t('trainingDetails.cancelRegistration')}</span>
                        </Button>
                     )}
                </div>
            </div>

             {/* Reason Input Area (for Reject or Cancel actions) */}
            {(registration.status === 'Created' || registration.status === 'Waiting' || registration.status === 'Confirmed') && (
                 <div>
                     <Textarea
                         placeholder={
                            registration.status === 'Confirmed'
                                ? t('trainingDetails.cancellationReasonPlaceholder')
                                : t('trainingDetails.rejectionReasonPlaceholder')
                            }
                        value={reason}
                        onChange={(e) => setReason(e.target.value)}
                        className="text-sm h-16"
                        disabled={isPendingAction}
                    />
                 </div>
             )}

             {/* Display existing reason for Rejected/Cancelled */}
             {(registration.status === 'Rejected' || registration.status === 'Cancelled') && registration.reason && (
                 <p className="text-xs text-muted-foreground italic pl-2 border-l-2">
                     {t('reason')}: {registration.reason}
                 </p>
             )}
        </li>
    );
};


export function RegistrationManagement({ training, trainerId, locale }: RegistrationManagementProps) {
    const t = useI18n();
    const [isPendingAction, startTransition] = useTransition();
    const { toast } = useToast();
    const router = useRouter();

    // Helper to get username
    const getUsername = (userId: string): string => {
        return placeholderUsers.find(u => u.id === userId)?.name || userId;
    };

    // Helper to translate status
    const translateStatus = (status: RegistrationStatus): string => {
         try {
            return t(`registrationStatuses.${status}`);
        } catch (e) {
             console.warn(`Missing translation for status: ${status}`);
            return status;
        }
    };

    const handleApprove = (riderId: string) => {
        startTransition(async () => {
            const result = await approveRegistration(trainerId, training.id, riderId);
            if (result.success) {
                 toast({ title: t('trainerActions.approveSuccessTitle'), description: t('trainerActions.approveSuccessDesc', { message: result.message }) });
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.approveErrorTitle'), description: t('trainerActions.approveErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };

    const handleReject = (riderId: string, reason?: string) => {
        startTransition(async () => {
            const result = await rejectRegistration(trainerId, training.id, riderId, reason);
             if (result.success) {
                 toast({ title: t('trainerActions.rejectSuccessTitle'), description: t('trainerActions.rejectSuccessDesc') });
                 // No need to clear reason here as it's local to the item component
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.rejectErrorTitle'), description: t('trainerActions.rejectErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };

     // Use cancelConfirmedRegistration action for trainer cancelling a confirmed rider
     const handleCancel = (riderId: string, reason?: string) => {
        startTransition(async () => {
            const result = await cancelConfirmedRegistration(trainerId, training.id, riderId, reason);
             if (result.success) {
                 toast({ title: t('trainerActions.cancelSuccessTitle'), description: t('trainerActions.cancelSuccessDesc') });
                 // No need to clear reason here
                router.refresh();
            } else {
                 toast({ title: t('trainerActions.cancelErrorTitle'), description: t('trainerActions.cancelErrorDesc', { message: result.message }), variant: 'destructive' });
            }
        });
    };

    const sortedRegistrations = [...training.registrations].sort((a, b) => {
        // Sort primarily by status order (e.g., Created -> Waiting -> Confirmed -> Cancelled -> Rejected)
        const statusOrder: Record<RegistrationStatus, number> = {
            Created: 1,
            Waiting: 2,
            Confirmed: 3,
            Cancelled: 4,
            Rejected: 5,
        };
        const statusDiff = statusOrder[a.status] - statusOrder[b.status];
        if (statusDiff !== 0) return statusDiff;
        // Then sort by registration time (earliest first)
        return a.registeredAt.getTime() - b.registeredAt.getTime();
    });

     const { confirmed } = getRegistrationCounts(training);

    return (
        <div className="space-y-4">
            <h3 className="text-lg font-semibold">
                {t('trainingDetails.allRegistrations')} ({training.registrations.length})
                 {training.maxRiders && ` - ${t('trainingDetails.capacity', { confirmed: confirmed, max: training.maxRiders })}`}
            </h3>

            {sortedRegistrations.length > 0 ? (
                 <ul className="space-y-3">
                     {sortedRegistrations.map(reg => (
                        <RegistrationItem
                            key={reg.userId}
                            registration={reg}
                            trainingId={training.id}
                            trainerId={trainerId}
                            maxRiders={training.maxRiders}
                            confirmedCount={confirmed}
                            isPendingAction={isPendingAction}
                            onApprove={handleApprove}
                            onReject={handleReject}
                            onCancel={handleCancel} // Trainer cancelling confirmed user
                            getUsername={getUsername}
                            translateStatus={translateStatus}
                            t={t}
                        />
                    ))}
                 </ul>
            ) : (
                <p className="text-sm text-muted-foreground">{t('myTrainings.noRegistrationsYet')}</p>
            )}

        </div>
    );
}
