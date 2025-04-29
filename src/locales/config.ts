// src/locales/config.ts
export const locales = ['en', 'cs'] as const;
export const defaultLocale = 'en';
export type Locale = typeof locales[number];
