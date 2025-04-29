'use client';

import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { UserCog, CalendarCheck, LogIn, SquarePen } from 'lucide-react';
import type { User, UserRole } from '@/lib/types';
import React from 'react';
import { NavLink } from './nav-link'; // Import NavLink from the new file
// Removed: import { useI18n } from '@/locales/client'; - No longer needed as translations are passed via props

interface NavTranslations {
    availableTrainings: string;
    myRegistrations: string;
    createTraining: string;
    loginSignUp: string;
    profile: string; // Template string like '{name} ({role})'
}

interface RoleTranslations {
    rider: string;
    trainer: string;
    guest: string;
}

interface HeaderNavProps {
    user: User | null;
    navTranslations: NavTranslations;
    roleTranslations: RoleTranslations;
    // Removed: initialTranslatedRole: string; - Not used directly
}

// Client component - uses passed props for translations
export function HeaderNav({ user, navTranslations, roleTranslations }: HeaderNavProps) {
    // Removed: const t = useI18n();

    // Function to translate role using passed translations
    const getTranslatedRole = (role: UserRole): string => {
        return roleTranslations[role] || role; // Use pre-fetched translations
    };

    // Function to format profile string
    const formatProfileString = (name: string, role: UserRole): string => {
        const translatedRole = getTranslatedRole(role);
        // Replace placeholders in the template string
        return navTranslations.profile
               .replace('{name}', name)
               .replace('{role}', translatedRole);
    }

    return (
        <>
          <NavLink href="/trainings" icon={<CalendarCheck />}>{navTranslations.availableTrainings}</NavLink>
          {user?.role === 'rider' && (
            <NavLink href="/my-trainings" icon={<CalendarCheck />}>{navTranslations.myRegistrations}</NavLink>
          )}
          {user?.role === 'trainer' && (
             <NavLink href="/trainings/create" icon={<SquarePen />}>{navTranslations.createTraining}</NavLink>
          )}
          {/* Placeholder for Auth - Replace with actual login/profile */}
          {user ? (
            <Button variant="ghost" size="sm">
               {/* Use the formatted string */}
               <UserCog className="mr-2 h-4 w-4" /> {formatProfileString(user.name, user.role)}
            </Button>
          ) : (
             <Button variant="outline" size="sm">
                 <LogIn className="mr-2 h-4 w-4" /> {navTranslations.loginSignUp}
            </Button>
          )}
        </>
    );
}
