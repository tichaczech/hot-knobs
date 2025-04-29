import Link from 'next/link';
import { Bike } from 'lucide-react'; // Use Bike icon instead of Motorcycle
import { getCurrentUser } from '@/lib/placeholder-data';
import type { UserRole } from '@/lib/types';
import React from 'react';
import { getI18n } from '@/locales/server'; // Import server-side i18n for translateRole helper
import { HeaderNav } from './header-nav'; // Import the new client component
import { NavLink } from './nav-link'; // Import NavLink from its own file

// Helper to translate role safely using server-side i18n initially
// This helper is kept here for potential future use on the server, but not passed directly
const translateRole = async (role: UserRole) => {
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
  // Removed: const initialTranslatedRole = user ? await translateRole(user.role) : ''; - Not needed in HeaderNav
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
            // Removed: initialTranslatedRole={initialTranslatedRole}
           />
        </nav>
      </div>
    </header>
  );
}

// NavLink component moved to src/components/layout/nav-link.tsx
