import { getCurrentUser, getTrainings } from '@/lib/placeholder-data';
import Link from 'next/link';
import { Button } from '@/components/ui/button';
import { TrainingCard } from '@/components/training-card';
import { ArrowRight } from 'lucide-react';
import { getI18n } from '@/locales/server'; // Import server-side i18n
import type { UserRole } from '@/lib/types';

export default async function Home() {
  const t = await getI18n(); // Get translation function
  const user = await getCurrentUser();
  const upcomingTrainings = (await getTrainings())
    .filter(t => t.date > new Date()) // Filter for future trainings
    .sort((a, b) => a.date.getTime() - b.date.getTime()) // Sort by date ascending
    .slice(0, 3); // Take the first 3

  // Helper to translate role safely
  const translateRole = (role: UserRole) => {
    try {
        return t(`userRoles.${role}`);
    } catch {
        return role; // Fallback to the role key if translation is missing
    }
  };

  return (
    <div className="space-y-8">
      <section className="text-center bg-card p-8 rounded-lg shadow">
        <h1 className="text-4xl font-bold text-primary mb-2">{t('home.welcome')}</h1>
        <p className="text-lg text-muted-foreground mb-4">
          {t('home.subheading')}
        </p>
        {user ? (
          <p className="text-md">
            {t('home.loggedInAs', { name: user.name, role: translateRole(user.role) })}
          </p>
        ) : (
          <p className="text-md text-muted-foreground">
            {t('home.browseOrLogin')}
          </p>
        )}
         <div className="mt-6 flex justify-center gap-4">
             <Link href="/trainings" passHref>
                 <Button size="lg">
                     {t('home.viewAllTrainings')} <ArrowRight className="ml-2 h-5 w-5"/>
                 </Button>
             </Link>
              {user?.role === 'rider' && (
                 <Link href="/my-trainings" passHref>
                     <Button size="lg" variant="outline">
                        {t('nav.myRegistrations')} {/* Use existing nav translation */}
                    </Button>
                 </Link>
             )}
             {user?.role === 'trainer' && (
                 <Link href="/trainings/create" passHref>
                    <Button size="lg" variant="outline">
                        {t('nav.createTraining')} {/* Use existing nav translation */}
                    </Button>
                 </Link>
             )}
             {!user && (
                 // TODO: Implement actual login/signup functionality
                 <Button size="lg" variant="secondary">{t('nav.loginSignUp')}</Button>
             )}
         </div>
      </section>

       {upcomingTrainings.length > 0 && (
            <section>
                <h2 className="text-2xl font-semibold mb-4 text-center">{t('home.upcomingTrainings')}</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {upcomingTrainings.map((training) => (
                    <TrainingCard
                    key={training.id}
                    training={training}
                    currentUser={user}
                    showRegisterButton={user?.role === 'rider'}
                    showViewDetailsLink={true}
                    />
                ))}
                </div>
                 <div className="text-center mt-6">
                    <Link href="/trainings" passHref>
                        <Button variant="link">
                            {t('home.seeAllTrainings')} <ArrowRight className="ml-1 h-4 w-4"/>
                        </Button>
                    </Link>
                 </div>
            </section>
        )}

    </div>
  );
}
