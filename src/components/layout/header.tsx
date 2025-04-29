import Link from 'next/link';
import { Bike } from 'lucide-react'; // Use Bike icon instead of Motorcycle
import { getCurrentUser } from '@/lib/placeholder-data';
import type { UserRole } from '@/lib/types';
import React from 'react';
import { getI18n } from '@/locales/server'; // Import server-side i18n for translateRole helper
import { HeaderNav } from './header-nav'; // Import the new client component

// Helper to translate role safely using server-side i18n initially
const translateRole = async (role: UserRole) => {
    // This function now runs on the server initially
    const t = await getI18n();
    try {
      return t(`userRoles.${role}`);
    } catch {
      return role; // Fallback
    }
};

export async function Header() {
  const user = await getCurrentUser(); // Fetch user role (Server component)
  const t = await getI18n(); // Get server-side translations for Header

  // Prepare props for the client component
  const initialTranslatedRole = user ? await translateRole(user.role) : '';
  const navTranslations = {
        availableTrainings: t('nav.availableTrainings'),
        myRegistrations: t('nav.myRegistrations'),
        createTraining: t('nav.createTraining'),
        loginSignUp: t('nav.loginSignUp'),
        profile: t('nav.profile'), // Keep the template string
  };
  const roleTranslations = {
      rider: t('userRoles.rider'),
      trainer: t('userRoles.trainer'),
      guest: t('userRoles.guest'),
  }


  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center space-x-4 sm:justify-between sm:space-x-0">
        <Link href="/" className="flex items-center space-x-2">
          <Bike className="h-6 w-6 text-primary" />
          {/* App name can be fetched from translations if needed */}
          <span className="font-bold text-lg">{t('appName')}</span>
        </Link>
        <nav className="flex flex-1 items-center justify-end space-x-4">
          {/* Render the client component, passing necessary data */}
          <HeaderNav
            user={user}
            navTranslations={navTranslations}
            roleTranslations={roleTranslations}
            initialTranslatedRole={initialTranslatedRole}
           />
        </nav>
      </div>
    </header>
  );
}


// Helper component for navigation links (remains server or client, doesn't matter much here)
// Keeping it here as it doesn't use client hooks
export function NavLink({ href, children, icon }: { href: string; children: React.ReactNode, icon?: React.ReactNode }) {
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
