import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { SignUpForm } from "./form";
import { Trans, useTranslation } from "react-i18next";
import { useAuth } from "@/contexts/auth";
import { type SignInSearch } from "@/types/search";
import { Link } from "@tanstack/react-router";
import { type SignUp } from "@/types/auth";

export default function SignUpPage({ continueWithUrl }: SignInSearch) {
    const { signUpWithEmail } = useAuth();
    const { t } = useTranslation();

    const handleForm = async (values: SignUp) => {
        if (!values.email || !values.password) {
            console.warn("Email and password are required for sign up!");
            return;
        }

        try {
            const user = await signUpWithEmail(values.email, values.password);
            console.log("User signed up:", user);
            window.location.href = continueWithUrl;
            // TODO: This is not working as expected, investigation needed.
            // router.history.push(continueWithUrl);
        } catch (error) {
            console.error("Error signing up:", error);
        }
    };

    return (
        <div className="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-6 md:p-10">
            <div className="flex w-full max-w-sm flex-col gap-6">
                <div className="flex flex-col gap-6">
                    <Card>
                        <CardHeader className="text-center justify-center">
                            <CardTitle className="text-xl">
                                {t("auth.signUp.page.title")}
                            </CardTitle>
                            <CardDescription>
                                {t("auth.signUp.page.description")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <SignUpForm onSignUpWithEmail={handleForm} />
                            <br /> {/* TODO: Use CSS for vertical separtion instead of HTML. */}
                            <div className="text-center text-sm">
                                {t("auth.signUp.page.hasAccountPrompt")}{" "}
                                <Link to='/signin' search={{ continueWithUrl }} className="underline underline-offset-4">
                                    {t("auth.signUp.page.signInLink")}
                                </Link>
                            </div>
                        </CardContent>
                    </Card>
                    <div className="text-balance text-center text-xs text-muted-foreground [&_a]:underline [&_a]:underline-offset-4 [&_a]:hover:text-primary  ">
                        <Trans i18nKey="signUp.form.termsAndPolicyPrompt">
                            By clicking continue, you agree to our <a href="https://bing.com" target="_blank">Terms of Service</a> and <a href="https://bing.com" target="_blank">Privacy Policy</a>.
                        </Trans>
                    </div>
                </div>
            </div>
        </div>
    )
}
