import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { AlertTriangle } from 'lucide-react'

export default function NotFound() {
  return (
    <div className="flex flex-col items-center justify-center space-y-4 text-center min-h-[60vh]">
        <AlertTriangle className="w-16 h-16 text-destructive" />
      <h2 className="text-2xl font-bold">Location Not Found</h2>
      <p className="text-muted-foreground">Could not find the requested location.</p>
      <Link href="/trainings" passHref>
        <Button variant="outline">Return to All Trainings</Button>
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
