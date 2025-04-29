import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { AlertTriangle } from 'lucide-react'
import { getI18n } from '@/locales/server';

export default async function NotFound() {
  const t = await getI18n(); // Get translations
  return (
    <div className="flex flex-col items-center justify-center space-y-4 text-center min-h-[60vh]">
        <AlertTriangle className="w-16 h-16 text-destructive" />
      <h2 className="text-2xl font-bold">{t('trainingDetails.notFound.title')}</h2>
      <p className="text-muted-foreground">{t('trainingDetails.notFound.description')}</p>
      <Link href="/trainings" passHref>
        <Button variant="outline">{t('trainingDetails.notFound.return')}</Button>
      </Link>
    </div>
  )
}
