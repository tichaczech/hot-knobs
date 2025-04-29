import Link from 'next/link';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Calendar, MapPin, User, Users, BarChart, AlertCircle, CheckCircle } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import type { TrainingSession, User as AppUser } from "@/lib/types";
import { format } from 'date-fns';
import { RegisterButton } from './register-button';
import { UnregisterButton } from './unregister-button';


interface TrainingCardProps {
  training: TrainingSession;
  currentUser: AppUser | null;
  showRegisterButton?: boolean; // Rider view
  showUnregisterButton?: boolean; // My Trainings view
  showViewDetailsLink?: boolean; // General purpose link
}

export function TrainingCard({
  training,
  currentUser,
  showRegisterButton = false,
  showUnregisterButton = false,
  showViewDetailsLink = true,
}: TrainingCardProps) {
  const isRegistered = currentUser && training.registeredRiders?.includes(currentUser.id);
  const isFull = training.maxRiders && training.registeredRiders && training.registeredRiders.length >= training.maxRiders;
  const spotsLeft = training.maxRiders ? training.maxRiders - (training.registeredRiders?.length ?? 0) : Infinity;

  return (
    <Card className="flex flex-col h-full shadow-md hover:shadow-lg transition-shadow duration-200">
      <CardHeader>
        <CardTitle className="text-primary">{training.title}</CardTitle>
        <CardDescription>Taught by {training.trainerName}</CardDescription>
      </CardHeader>
      <CardContent className="flex-grow space-y-3">
        <div className="flex items-center text-sm text-muted-foreground">
          <Calendar className="mr-2 h-4 w-4" />
          <span>{format(training.date, 'PPP p')}</span> {/* e.g., Jun 21, 2024 10:00 AM */}
        </div>
        <div className="flex items-center text-sm text-muted-foreground">
          <MapPin className="mr-2 h-4 w-4" />
          <span>{training.location}</span>
        </div>
         <div className="flex items-center text-sm text-muted-foreground">
          <BarChart className="mr-2 h-4 w-4" />
          <span>Skill Level: <Badge variant="secondary">{training.skillLevel}</Badge></span>
        </div>
         <div className="flex items-center text-sm text-muted-foreground">
          <Users className="mr-2 h-4 w-4" />
          <span>
             {training.maxRiders
                ? `${training.registeredRiders?.length ?? 0} / ${training.maxRiders} registered (${spotsLeft > 0 ? `${spotsLeft} spot${spotsLeft !== 1 ? 's' : ''} left` : 'Full'})`
                : `${training.registeredRiders?.length ?? 0} registered`}
          </span>
        </div>
        <p className="text-sm">{training.description}</p>
      </CardContent>
      <CardFooter className="flex justify-between items-center mt-auto pt-4 border-t">
         {showViewDetailsLink && (
             <Link href={`/trainings/${training.id}`} passHref legacyBehavior>
              <Button variant="link" size="sm">View Details</Button>
            </Link>
         )}
        <div className="flex gap-2">
            {currentUser?.role === 'rider' && showRegisterButton && (
              <>
                {isRegistered ? (
                   <Badge variant="default" className="bg-green-600 hover:bg-green-700">
                     <CheckCircle className="mr-1 h-4 w-4" /> Registered
                   </Badge>
                ) : isFull ? (
                    <Badge variant="destructive">
                      <AlertCircle className="mr-1 h-4 w-4" /> Full
                    </Badge>
                ) : (
                    <RegisterButton trainingId={training.id} userId={currentUser.id} />
                )}
              </>
            )}
            {currentUser?.role === 'rider' && showUnregisterButton && isRegistered && (
                 <UnregisterButton trainingId={training.id} userId={currentUser.id} />
            )}
             {/* Maybe add edit/delete for trainers in the future */}
        </div>
      </CardFooter>
    </Card>
  );
}
