// src/app/layout.tsx
// This layout applies to the root level, before locale segments.
// It should contain minimal structure, mostly passing children down.

import type { Metadata } from 'next';
import './globals.css';


export const metadata: Metadata = {
  title: 'Mad Sprocket', // Generic title, locale layout can override
  description: 'Motocross and Enduro Training Management', // Generic description
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return children; // Pass children directly to the locale layout
}
