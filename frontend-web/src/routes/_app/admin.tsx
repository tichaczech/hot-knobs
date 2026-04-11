import { createFileRoute, redirect } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin')({
  beforeLoad: async ({ context }) => {
    // NOTE: Is this really needed?
    // const { currentUser } = context.authContext;

    // if (!currentUser) {
    //   // Not authenticated, redirect to login
    //   throw redirect({ to: '/signin' });
    // }

    const { isAdmin } = context.authContext;

    if (!isAdmin) {
      // Not authorized, redirect to home
      throw redirect({ to: '/dashboard' });
    }
  }
})
