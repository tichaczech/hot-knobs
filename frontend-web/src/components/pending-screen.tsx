import { Spinner } from '@/components/ui/spinner';
import { useTranslation } from 'react-i18next';

interface PendingScreenProps {
    text?: string;
}

export function PendingScreen({ text }: PendingScreenProps) {
    const { t } = useTranslation();

    return (
        <div className="flex flex-col items-center justify-center h-screen">
            <Spinner speed={2000} type='bars' className='sm:h-8 sm:w-8 md:h-16 md:w-16 xl:h-32 xl:w-32' />
            <p className="sm:text-sm md:text-md xl:text-lg font-semibold">
                {text ?? t('_common.loading')}
            </p>
        </div>
    );
}
