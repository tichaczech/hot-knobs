import { createFileRoute, redirect } from '@tanstack/react-router';
import { LogIn, UserPen } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { useTranslation } from 'react-i18next';

export const Route = createFileRoute('/')({
  beforeLoad: async ({ context }) => {
    const { currentUser } = context.authContext;
    if (currentUser) {
      throw redirect({ to: '/dashboard' });
    }
  },
  component: App,
});

function App() {
  const { t } = useTranslation();

  return (
    <div className="min-h-screen bg-background text-foreground p-4 flex flex-col items-center justify-center">
      <header className="text-center mb-12 flex flex-col items-center">
        {/* <img src="/img/logo.png" alt="Mad Sprocket Logo" width="256" height="256" className="mb-6" /> */}
        <h1 className="text-6xl font-bold mb-6">{t('home.welcomeTitle')}</h1>
        <p className="text-2xl mb-10 max-w-3xl mx-auto text-muted-foreground">
          {t('home.welcomeSubtitle')}
        </p>
        <div className="space-x-6">
          <Button className='transform hover:scale-105 transition duration-300' variant={"secondary"} size={"lg"} onClick={() => { window.location.href = "/signIn"; }}>
            <LogIn />
            <span className='text-xl'>
              {t('home.loginButton')}
            </span>
          </Button>
        </div>

      </header>

      <main className="w-full max-w-5xl mx-auto grid md:grid-cols-3 gap-8 mb-12 text-center">
        <div className="w-full max-w-md mx-auto transform hover:scale-105 transition duration-300">
          <div className="bg-white rounded-lg shadow-lg overflow-hidden transition-all duration-300 hover:shadow-xl dark:bg-gray-950">
            <img
              src="/img/operators.png"
              alt="Product Image"
              width={480}
              height={480}
              className="w-full object-cover"
              style={{ aspectRatio: "480/480", objectFit: "fill" }}
            />
            <div className="p-4 space-y-2">
              <h3 className="text-xl font-semibold">{t('home.operators.title')}</h3>
              <p className="text-gray-500 dark:text-gray-400">{t('home.operators.description')}</p>
              <br /><hr />
              <div className="flex items-center justify-center">
                <Button className='transform hover:scale-105 transition duration-300' variant={"default"} size={"lg"} onClick={() => { window.location.href = "/signUp"; }}>
                  <UserPen />
                  <span className='text-xl'>
                    {t('home.registerButton')}
                  </span>
                </Button>
              </div>
            </div>
          </div>
        </div>

        <div className="w-full max-w-md mx-auto transform hover:scale-105 transition duration-300">
          <div className="bg-white rounded-lg shadow-lg overflow-hidden transition-all duration-300 hover:shadow-xl dark:bg-gray-950">
            <img
              src="/img/coaches.png"
              alt="Product Image"
              width={480}
              height={480}
              className="w-full object-cover"
              style={{ aspectRatio: "480/480", objectFit: "fill" }}
            />
            <div className="p-4 space-y-2">
              <h3 className="text-xl font-semibold">{t('home.coaches.title')}</h3>
              <p className="text-gray-500 dark:text-gray-400">{t('home.coaches.description')}</p>
              <br /><hr />
              <div className="flex items-center justify-center">
                <Button className='transform hover:scale-105 transition duration-300' variant={"default"} size={"lg"} onClick={() => { window.location.href = "/signUp"; }}>
                  <UserPen />
                  <span className='text-xl'>
                    {t('home.registerButton')}
                  </span>
                </Button>
              </div>
            </div>
          </div>
        </div>

        <div className="w-full max-w-md mx-auto transform hover:scale-105 transition duration-300">
          <div className="bg-white rounded-lg shadow-lg overflow-hidden transition-all duration-300 hover:shadow-xl dark:bg-gray-950">
            <img
              src="/img/riders.png"
              alt="Product Image"
              width={480}
              height={480}
              className="w-full object-cover"
              style={{ aspectRatio: "480/480", objectFit: "fill" }}
            />
            <div className="p-4 space-y-2">
              <h3 className="text-xl font-semibold">{t('home.riders.title')}</h3>
              <p className="text-gray-500 dark:text-gray-400">{t('home.riders.description')}</p>
              <br /><hr />
              <div className="flex items-center justify-center">
                <Button className='transform hover:scale-105 transition duration-300' variant={"default"} size={"lg"} onClick={() => { window.location.href = "/signUp"; }}>
                  <UserPen />
                  <span className='text-xl'>
                    {t('home.registerButton')}
                  </span>
                </Button>
              </div>
            </div>
          </div>
        </div>
      </main>

      <footer className="text-center text-muted-foreground text-sm mt-auto pb-8">
        <p>{t('home.footerNotice', { year: new Date().getFullYear() })}</p>
      </footer>
    </div>
  );
}
