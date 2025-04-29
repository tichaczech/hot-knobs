import { getLocationById } from '@/lib/placeholder-data';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { MapPin, ArrowLeft, Map } from 'lucide-react'; // Added Map icon
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { Locale } from '@/locales/config'; // Import Locale type

interface LocationDetailsPageProps {
  params: { id: string; locale: Locale }; // Add locale
}

// Generate localized metadata
export async function generateMetadata({ params }: LocationDetailsPageProps): Promise<Metadata> {
  const t = await getI18n(params.locale);
  const location = await getLocationById(params.id);
  if (!location) {
    return {
      title: t('locationDetails.meta.notFoundTitle'),
    };
  }
  const addressPart = location.address ? t('locationDetails.meta.addressPart', { address: location.address }) : '';
  return {
    title: t('locationDetails.meta.title', { locationName: location.name }),
    description: t('locationDetails.meta.description', { locationName: location.name, address: addressPart }),
  };
}

export default async function LocationDetailsPage({ params }: LocationDetailsPageProps) {
  const t = await getI18n(params.locale); // Get translation function
  const location = await getLocationById(params.id);

  if (!location) {
    notFound(); // Redirect to 404 if location doesn't exist
  }

  const hasCoordinates = location.location !== undefined
  const googleMapsUrl = hasCoordinates
    ? `https://www.google.com/maps/search/?api=1&query=${location.location!.latitude},${location.location!.longitude}`
    : null;
   const openStreetMapUrl = hasCoordinates
    ? `https://www.openstreetmap.org/?mlat=${location.latitude}&mlon=${location.location!.longitude}#map=15/${location.latitude}/${location.location!.longitude}`
     : null;

  // TODO: Fetch trainings happening at this location in the future?

  return (
    <div className="space-y-6 max-w-3xl mx-auto"> {/* Increased max-width */}
      <Link href="/trainings" passHref legacyBehavior>
        {/* Adjust link as needed, maybe back to a locations list page if created */}
        <Button variant="outline" size="sm" className="mb-4">
          <ArrowLeft className="mr-2 h-4 w-4" /> {t('locationDetails.backToTrainings')}
        </Button>
      </Link>

      <Card className="shadow-md">
        <CardHeader>
          <CardTitle className="flex items-center text-primary">
            <MapPin className="mr-2 h-6 w-6" /> {location.name}
          </CardTitle>
          {location.address && (
            <CardDescription>{location.address}</CardDescription>
          )}
        </CardHeader>
        <CardContent>
            {/* Location Details */}
            <div className="space-y-2 mb-6">
                 <p className="text-muted-foreground">
                    {t('locationDetails.detailsPlaceholder')}
                </p>
                {hasCoordinates && (
                    <div className="text-sm text-muted-foreground flex items-center gap-2">
                        <span>{t('locationDetails.coordinates', { lat: location.location?.latitude.toFixed(4), lon: location.location?.longitude.toFixed(4) })}</span>
                    </div>
                )}
            </div>


          {/* Map Preview Section */}
          {hasCoordinates && (
             <div className="border-t pt-4">
                <h3 className="text-lg font-semibold mb-2 flex items-center"><Map className="mr-2 h-5 w-5"/>{t('locationDetails.mapPreview')}</h3>
                 {/* Placeholder for an embedded map or image */}
                <div className="bg-muted rounded-md p-4 text-center mb-4 h-48 flex items-center justify-center">
                  <p className="text-muted-foreground">
                    {t('locationDetails.mapPlaceholder')}
                     <br/>
                     <span className="text-xs">{t('locationDetails.mapHint')}</span>
                  </p>
                </div>
                 <div className="flex gap-4">
                     {googleMapsUrl && (
                        <Link href={googleMapsUrl} target="_blank" rel="noopener noreferrer" passHref legacyBehavior>
                             <Button variant="outline">
                                 {t('locationDetails.viewOnGoogleMaps')}
                             </Button>
                         </Link>
                    )}
                     {openStreetMapUrl && (
                         <Link href={openStreetMapUrl} target="_blank" rel="noopener noreferrer" passHref legacyBehavior>
                            <Button variant="outline">
                                {t('locationDetails.viewOnOpenStreetMap')}
                             </Button>
                        </Link>
                    )}
                 </div>
             </div>
           )}

          {/* Placeholder for list of upcoming trainings at this location */}
           <div className="mt-6 border-t pt-4">
                <h3 className="text-lg font-semibold mb-2">{t('locationDetails.upcomingTrainings')}</h3>
                <p className="text-sm text-muted-foreground">
                    {t('locationDetails.upcomingTrainingsPlaceholder')}
                </p>
                {/* Example:
                 <ul>
                    <li><Link href="/trainings/ts-1">Enduro Basics Clinic</Link> - [Date]</li>
                 </ul>
                 */}
           </div>

           {/* Placeholder for Admin actions */}
            {/*
            {currentUser?.role === 'admin' && (
                <div className="mt-6 border-t pt-4 flex gap-2">
                    <Button variant="outline" size="sm">{t('locationDetails.editLocation')}</Button>
                    <Button variant="destructive" size="sm">{t('locationDetails.deleteLocation')}</Button>
                </div>
            )}
            */}
        </CardContent>
      </Card>
    </div>
  );
}
