import SignUpPage from '@/app/auth/signUp/page'
import { signInSearchSchema } from '@/types/search';
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_auth/signUp')({
  component: RouteComponent,
  validateSearch: signInSearchSchema
});

function RouteComponent() {
    const { continueWithUrl } = Route.useSearch();

    return (
        <SignUpPage continueWithUrl={continueWithUrl} />
    );
}
