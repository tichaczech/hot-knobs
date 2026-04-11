import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { OperatorEditForm } from "./form";
import { Trans, useTranslation } from "react-i18next";
import { useAuth } from "@/contexts/auth";
import type { EntityId, Operator } from "@/types/models";

export interface OperatorEditPageProps extends React.ComponentProps<"div"> {
    operatorId: EntityId | null;
}

export default function OperatorEditPage({ operatorId }: OperatorEditPageProps) {
    const { isAdmin } = useAuth();
    const { t } = useTranslation();

    const handleForm = async (values: Operator) => {
        try {
            console.log(values);
            window.location.href = "/admin/operators";
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
                                {t("operator.edit.page.title")}
                            </CardTitle>
                            <CardDescription>
                                {t("operator.edit.page.description")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <OperatorEditForm onSave={handleForm} />
                        </CardContent>
                    </Card>
                    {/* <div className="text-balance text-center text-xs text-muted-foreground [&_a]:underline [&_a]:underline-offset-4 [&_a]:hover:text-primary  ">
                        <Trans i18nKey="signUp.form.termsAndPolicyPrompt">
                            By clicking continue, you agree to our <a href="https://bing.com" target="_blank">Terms of Service</a> and <a href="https://bing.com" target="_blank">Privacy Policy</a>.
                        </Trans>
                    </div> */}
                </div>
            </div>
        </div>
    )
}
