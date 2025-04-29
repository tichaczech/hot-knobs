// src/locales/server.ts
import { createI18nServer } from 'next-international/server';
import { locales, defaultLocale } from './config';

export const { getI18n, getScopedI18n, getStaticParams, getCurrentLocale } = createI18nServer({
  en: () => import('./en'),
  cs: () => import('./cs'),
}, {
    // Uncomment to use custom segment name
    // segmentName: 'lang',
    // Uncomment to set fallback locale
    // fallbackLocale: defaultLocale,
});
