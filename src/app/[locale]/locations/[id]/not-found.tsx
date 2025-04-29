import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { AlertTriangle } from 'lucide-react'
import { getI18n } from '@/locales/server'; // Import server-side i18n

export default async function NotFound() {
    const t = await getI18n(); // Get translation function
  return (
    <div className="flex flex-col items-center justify-center space-y-4 text-center min-h-[60vh]">
        <AlertTriangle className="w-16 h-16 text-destructive" />
      <h2 className="text-2xl font-bold">{t('locationDetails.notFound.title')}</h2>
      <p className="text-muted-foreground">{t('locationDetails.notFound.description')}</p>
      <Link href="/trainings" passHref>
        <Button variant="outline">{t('locationDetails.notFound.return')}</Button>
      </Link>
      {/* Optionally link to a future /locations page */}
      {/*
      <Link href="/locations" passHref>
        <Button variant="link">View All Locations</Button>
      </Link>
      */}
    </div>
  )
}
