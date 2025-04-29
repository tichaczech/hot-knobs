// This file re-exports the RootLayout but within the [locale] segment.
// This is necessary for the middleware and routing to work correctly with locales.
// src/app/layout.tsx content is moved here.

import type { Metadata } from 'next';
import { Geist, Geist_Mono } from 'next/font/google';
import '../globals.css'; // Adjust path due to locale folder
import { Header } from '@/components/layout/header';
import { Toaster } from "@/components/ui/toaster"
import { cn } from '@/lib/utils';
import { getCurrentLocale, getStaticParams } from '@/locales/server';
import { I18nProviderClient } from '@/locales/client';
import type { Locale } from '@/locales/config'; // Import Locale type

const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

// Generate static params for SSG (optional but recommended)
export function generateStaticParams() {
    return getStaticParams();
}


// Metadata can remain static or be dynamically generated if needed
// Consider using getI18n for dynamic metadata based on locale
export async function generateMetadata({ params: { locale } }: { params: { locale: Locale } }): Promise<Metadata> {
    // Example of dynamic metadata title (optional)
    // const t = await getI18n(locale);
    return {
        title: 'Mad Sprocket Training', // Replace with t('appName') or similar if needed
        description: 'Manage and register for motocross and enduro trainings with Mad Sprocket.', // Replace with t('description') if needed
    };
}


export default async function LocaleLayout({
  children,
  params: { locale } // Receive locale from params
}: Readonly<{
  children: React.ReactNode;
  params: { locale: Locale }; // Use Locale type
}>) {
  // const currentLocale = getCurrentLocale(); // Already have locale from params

  return (
    // Use the locale from params for the lang attribute
    <html lang={locale}>
      <body
        className={cn(
          'min-h-screen bg-secondary font-sans antialiased',
          geistSans.variable,
          geistMono.variable
        )}
      >
       {/* Wrap the application with the I18n client provider */}
       <I18nProviderClient locale={locale}>
          <div className="relative flex min-h-screen flex-col">
            <Header />
            <main className="flex-1 container mx-auto px-4 py-8">
              {children}
            </main>
          </div>
           <Toaster />
        </I18nProviderClient>
      </body>
    </html>
  );
}
