import { useAuth } from "@/contexts/auth";
import { createRouter, RouterProvider } from "@tanstack/react-router";

// Import the generated route tree
import { routeTree } from './routeTree.gen';
import { useQueryClient } from "@tanstack/react-query";

// Create a new router instance
const router = createRouter({
  routeTree,
  context: { authContext: undefined!, queryClient: undefined! },
  defaultPreload: 'intent',
  scrollRestoration: true,
  defaultStructuralSharing: true,
  defaultPreloadStaleTime: 0,
});

// Register the router instance for type safety
declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router;
  }
}

export default function App() {
  const authContext = useAuth();
  const queryClient = useQueryClient();

  return (
    <RouterProvider router={router} context={{ authContext, queryClient }} />
  );
}
