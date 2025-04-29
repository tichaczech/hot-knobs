import { getLocationById } from '@/lib/placeholder-data';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { MapPin, ArrowLeft, Map } from 'lucide-react'; // Added Map icon
import Link from 'next/link';
import { Button } from '@/components/ui/button';

interface LocationDetailsPageProps {
  params: { id: string };
}

export async function generateMetadata({ params }: LocationDetailsPageProps): Promise<Metadata> {
  const location = await getLocationById(params.id);
  if (!location) {
    return {
      title: 'Location Not Found - Mad Sprocket',
    };
  }
  return {
    title: `${location.name} - Mad Sprocket Training Location`,
    description: `Details for the training location: ${location.name}${location.address ? ` at ${location.address}` : ''}.`,
  };
}

export default async function LocationDetailsPage({ params }: LocationDetailsPageProps) {
  const location = await getLocationById(params.id);

  if (!location) {
    notFound(); // Redirect to 404 if location doesn't exist
  }

  const hasCoordinates = location.latitude !== undefined && location.longitude !== undefined;
  const googleMapsUrl = hasCoordinates
    ? `https://www.google.com/maps/search/?api=1&query=${location.latitude},${location.longitude}`
    : null;
   const openStreetMapUrl = hasCoordinates
    ? `https://www.openstreetmap.org/?mlat=${location.latitude}&mlon=${location.longitude}#map=15/${location.latitude}/${location.longitude}`
     : null;

  // TODO: Fetch trainings happening at this location in the future?

  return (
    <div className="space-y-6 max-w-3xl mx-auto"> {/* Increased max-width */}
      <Link href="/trainings" passHref legacyBehavior>
        {/* Adjust link as needed, maybe back to a locations list page if created */}
        <Button variant="outline" size="sm" className="mb-4">
          <ArrowLeft className="mr-2 h-4 w-4" /> Back to All Trainings
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
                    More details about the location could go here, such as track conditions, amenities, website link, etc.
                </p>
                {hasCoordinates && (
                    <div className="text-sm text-muted-foreground flex items-center gap-2">
                        <span>Coordinates: {location.latitude?.toFixed(4)}, {location.longitude?.toFixed(4)}</span>
                    </div>
                )}
            </div>


          {/* Map Preview Section */}
          {hasCoordinates && (
             <div className="border-t pt-4">
                <h3 className="text-lg font-semibold mb-2 flex items-center"><Map className="mr-2 h-5 w-5"/>Map Preview & Links</h3>
                 {/* Placeholder for an embedded map or image */}
                <div className="bg-muted rounded-md p-4 text-center mb-4 h-48 flex items-center justify-center">
                  <p className="text-muted-foreground">
                    (Map preview placeholder - Integration with a map library like Leaflet or an iframe is needed here)
                     <br/>
                     <span className="text-xs"> For now, use the links below.</span>
                  </p>
                </div>
                 <div className="flex gap-4">
                     {googleMapsUrl && (
                        <Link href={googleMapsUrl} target="_blank" rel="noopener noreferrer" passHref legacyBehavior>
                             <Button variant="outline">
                                 View on Google Maps
                             </Button>
                         </Link>
                    )}
                     {openStreetMapUrl && (
                         <Link href={openStreetMapUrl} target="_blank" rel="noopener noreferrer" passHref legacyBehavior>
                            <Button variant="outline">
                                View on OpenStreetMap
                             </Button>
                        </Link>
                    )}
                 </div>
             </div>
           )}

          {/* Placeholder for list of upcoming trainings at this location */}
           <div className="mt-6 border-t pt-4">
                <h3 className="text-lg font-semibold mb-2">Upcoming Trainings Here</h3>
                <p className="text-sm text-muted-foreground">
                    (Functionality to list trainings for this location is not yet implemented.)
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
                    <Button variant="outline" size="sm">Edit Location</Button>
                    <Button variant="destructive" size="sm">Delete Location</Button>
                </div>
            )}
            */}
        </CardContent>
      </Card>
    </div>
  );
}
