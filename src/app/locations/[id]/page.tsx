import { getLocationById } from '@/lib/placeholder-data';
import { notFound } from 'next/navigation';
import { Metadata } from 'next';
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from '@/components/ui/card';
import { MapPin, ArrowLeft } from 'lucide-react';
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

  // TODO: Fetch trainings happening at this location in the future?

  return (
    <div className="space-y-6 max-w-2xl mx-auto">
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
          {/* Placeholder for additional location details */}
          <p className="text-muted-foreground">
            More details about the location could go here, such as track conditions, amenities, website link, etc.
          </p>

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
