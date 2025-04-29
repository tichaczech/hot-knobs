// src/middleware.ts
import { createI18nMiddleware } from 'next-international/middleware';
import { locales, defaultLocale } from './locales/config';
import type { NextRequest } from 'next/server';

const I18nMiddleware = createI18nMiddleware({
  locales: locales,
  defaultLocale: defaultLocale,
  // Optional: Define paths that should not be localized
  // urlMappingStrategy: 'rewrite', // Recommended for cleaner URLs
});

export function middleware(request: NextRequest) {
  return I18nMiddleware(request);
}

export const config = {
  matcher: ['/((?!api|_next/static|_next/image|favicon.ico|.*\\..*).*)'] // Adjust if necessary
};
