// src/app/page.tsx
// This page should likely redirect to the default locale or handle root logic if any.
// For now, we'll keep it simple, assuming the middleware handles the redirect.
// You might want to add a redirect here as a fallback.

// import { redirect } from 'next/navigation';
// import { defaultLocale } from '@/locales/config';

export default function RootPage() {
  // Option 1: Redirect to default locale (requires middleware setup)
  // redirect(`/${defaultLocale}`);

  // Option 2: Render minimal content or nothing, relying on middleware
  return null; // Or a loading indicator, etc.
}
