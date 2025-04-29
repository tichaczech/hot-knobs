import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { Bike, UserCog, CalendarCheck, LogIn, SquarePen } from 'lucide-react'; // Changed Motorcycle to Bike
import { getCurrentUser } from '@/lib/placeholder-data';
import type { User } from '@/lib/types';
import React from 'react'; // Import React

export async function Header() {
  const user = await getCurrentUser(); // Fetch user role

  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center space-x-4 sm:justify-between sm:space-x-0">
        <Link href="/" className="flex items-center space-x-2">
          <Bike className="h-6 w-6 text-primary" /> {/* Changed Motorcycle to Bike */}
          <span className="font-bold text-lg">Mad Sprocket</span>
        </Link>
        <nav className="flex flex-1 items-center justify-end space-x-4">
           <NavLink href="/trainings" icon={<CalendarCheck />}>Available Trainings</NavLink>
          {user?.role === 'rider' && (
            <NavLink href="/my-trainings" icon={<CalendarCheck />}>My Registrations</NavLink>
          )}
          {user?.role === 'trainer' && (
             <NavLink href="/trainings/create" icon={<SquarePen />}>Create Training</NavLink>
          )}
          {/* Placeholder for Auth - Replace with actual login/profile */}
          {user ? (
            <Button variant="ghost" size="sm">
               <UserCog className="mr-2 h-4 w-4" /> {user.name} ({user.role})
            </Button>
          ) : (
             <Button variant="outline" size="sm">
                 <LogIn className="mr-2 h-4 w-4" /> Login / Sign Up
            </Button>
          )}
        </nav>
      </div>
    </header>
  );
}

// Helper component for navigation links
function NavLink({ href, children, icon }: { href: string; children: React.ReactNode, icon?: React.ReactNode }) {
  return (
    <Link
      href={href}
      className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary flex items-center gap-1"
    >
       {icon && React.cloneElement(icon as React.ReactElement, { className: 'h-4 w-4' })}
      {children}
    </Link>
  );
}
