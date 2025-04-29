import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { AlertTriangle } from 'lucide-react'

export default function NotFound() {
  return (
    <div className="flex flex-col items-center justify-center space-y-4 text-center min-h-[60vh]">
        <AlertTriangle className="w-16 h-16 text-destructive" />
      <h2 className="text-2xl font-bold">Training Not Found</h2>
      <p className="text-muted-foreground">Could not find the requested training session.</p>
      <Link href="/trainings" passHref>
        <Button variant="outline">Return to All Trainings</Button>
      </Link>
    </div>
  )
}
