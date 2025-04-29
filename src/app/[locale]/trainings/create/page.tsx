import { getCurrentUser } from '@/lib/placeholder-data';
import { redirect } from 'next/navigation';
import { Metadata } from 'next';
import { CreateTrainingForm } from './_components/create-training-form';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type

// Generate localized metadata
export async function generateMetadata({ params: { locale } }: { params: { locale: Locale } }): Promise<Metadata> {
  const t = await getI18n(locale);
  return {
    title: t('createTraining.meta.title'),
    description: t('createTraining.meta.description'),
  };
}


export default async function CreateTrainingPage() {
  const t = await getI18n(); // Get translation function
  const currentUser = await getCurrentUser();

  // This page is only for trainers
  if (!currentUser || currentUser.role !== 'trainer') {
     // Redirect non-trainers or guests to the localized homepage
     redirect('/');
  }


  return (
    <div className="max-w-2xl mx-auto">
         <Card className="shadow-lg">
            <CardHeader>
                <CardTitle className="text-2xl">{t('createTraining.title')}</CardTitle>
                <CardDescription>{t('createTraining.description')}</CardDescription>
            </CardHeader>
            <CardContent>
                <CreateTrainingForm trainerId={currentUser.id} />
            </CardContent>
        </Card>
    </div>
  );
}
