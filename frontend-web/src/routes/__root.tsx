import { Outlet, createRootRouteWithContext } from '@tanstack/react-router';
import { TanStackRouterDevtools } from '@tanstack/react-router-devtools';
import { type AuthContext } from '../contexts/auth';
import { Toaster } from '@/components/ui/sonner';
import type { QueryClient } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'

export interface RouterContext {
  authContext: AuthContext;
  queryClient: QueryClient;
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: () => (
    <>
      <Outlet />

      <Toaster richColors />
      
      <ReactQueryDevtools initialIsOpen={false} buttonPosition="bottom-right" />
      <TanStackRouterDevtools initialIsOpen={false} position="bottom-right" />
    </>
  ),
});
