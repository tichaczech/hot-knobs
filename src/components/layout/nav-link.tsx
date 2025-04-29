// src/components/layout/nav-link.tsx
import Link from 'next/link';
import React from 'react';
import { cn } from '@/lib/utils'; // Import cn if needed for future styling

// Helper component for navigation links.
// This component is simple and can be used in both Server and Client Components.
export function NavLink({ href, children, icon }: { href: string; children: React.ReactNode, icon?: React.ReactNode }) {
  return (
    <Link
      href={href}
      className="text-sm font-medium text-muted-foreground transition-colors hover:text-primary flex items-center gap-1"
    >
      {/* Clone icon to add className if it's a React Element */}
      {icon && React.isValidElement(icon) && React.cloneElement(icon, { className: 'h-4 w-4' })}
      {children}
    </Link>
  );
}
