// src/locales/client.ts
'use client';
import { createI18nClient } from 'next-international/client';
import { locales, defaultLocale } from './config';

export const { useI18n, useScopedI18n, I18nProviderClient, useChangeLocale, useCurrentLocale } = createI18nClient({
  en: () => import('./en'),
  cs: () => import('./cs'),
}, {
    // Uncomment to set fallback locale
    // fallbackLocale: defaultLocale,
});
