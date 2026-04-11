import SignInPage from '@/app/auth/signIn/page';
import { signInSearchSchema } from '@/types/search';
import { createFileRoute } from '@tanstack/react-router';

export const Route = createFileRoute('/_auth/signIn')({
    component: RouteComponent,
    validateSearch: signInSearchSchema
});

function RouteComponent() {
    const { continueWithUrl } = Route.useSearch();

    return (
        <SignInPage continueWithUrl={continueWithUrl} />
    );
}
