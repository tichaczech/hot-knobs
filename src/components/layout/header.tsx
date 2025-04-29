import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { Bike, UserCog, CalendarCheck, LogIn, SquarePen } from 'lucide-react';
import { getCurrentUser } from '@/lib/placeholder-data';
import type { User, UserRole } from '@/lib/types';
import React from 'react';
import { useI18n } from '@/locales/client'; // Import client-side i18n hook

export async function Header() {
  const user = await getCurrentUser(); // Fetch user role

  // Helper to translate role safely
  const translateRole = (role: UserRole, t: ReturnType<typeof useI18n>) => {
    try {
      return t(`userRoles.${role}`);
    } catch {
      return role; // Fallback
    }
  };

  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center space-x-4 sm:justify-between sm:space-x-0">
        <Link href="/" className="flex items-center space-x-2">
          <Bike className="h-6 w-6 text-primary" />
          <span className="font-bold text-lg">Mad Sprocket</span> {/* App name is hardcoded here, can be dynamic */}
        </Link>
        <nav className="flex flex-1 items-center justify-end space-x-4">
          {/* Use the hook inside a client component wrapper */}
          <HeaderNav user={user} translateRole={translateRole}/>
        </nav>
      </div>
    </header>
  );
}

// Client component to use the i18n hook
function HeaderNav({ user, translateRole }: { user: User | null, translateRole: (role: UserRole, t: ReturnType<typeof useI18n>) => string }) {
    const t = useI18n();

    return (
        <>
          <NavLink href="/trainings" icon={<CalendarCheck />}>{t('nav.availableTrainings')}</NavLink>
          {user?.role === 'rider' && (
            <NavLink href="/my-trainings" icon={<CalendarCheck />}>{t('nav.myRegistrations')}</NavLink>
          )}
          {user?.role === 'trainer' && (
             <NavLink href="/trainings/create" icon={<SquarePen />}>{t('nav.createTraining')}</NavLink>
          )}
          {/* Placeholder for Auth - Replace with actual login/profile */}
          {user ? (
            <Button variant="ghost" size="sm">
               <UserCog className="mr-2 h-4 w-4" /> {t('nav.profile', {name: user.name, role: translateRole(user.role, t)})}
            </Button>
          ) : (
             <Button variant="outline" size="sm">
                 <LogIn className="mr-2 h-4 w-4" /> {t('nav.loginSignUp')}
            </Button>
          )}
        </>
    );
}


// Helper component for navigation links (remains server or client, doesn't matter much here)
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
