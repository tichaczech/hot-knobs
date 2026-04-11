import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { ChangePasswordForm } from "./form";
import { useTranslation } from "react-i18next";
import { useAuth } from "@/contexts/auth";
import { Link, useNavigate } from "@tanstack/react-router";
import { type ChangePassword } from "@/types/auth";

export default function ChangePasswordPage() {
    const navigate = useNavigate();
    const { updatePassword, currentUser } = useAuth();
    const { t } = useTranslation();

    const handleForm = async (values: ChangePassword) => {
        if (!values.oldPassword || !values.newPassword) {
            console.warn("Both new and old passwords are required!");
            return;
        }

        try {
            await updatePassword(values.oldPassword, values.newPassword);
            console.log("Password updated successfully for user: ", currentUser!.email);
            navigate({ to: "/dashboard" });
        } catch (error) {
            console.error("Error updating password:", error);
            // Show error to the user
        }
    };

    return (
        <div className="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-6 md:p-10">
            <div className="flex w-full max-w-sm flex-col gap-6">
                <Card>
                    <CardHeader className="text-center justify-center">
                        <CardTitle className="text-xl">
                            {t("auth.changePassword.page.title")}
                        </CardTitle>
                        <CardDescription>
                            {t("auth.changePassword.page.description")}
                        </CardDescription>
                    </CardHeader>
                    <CardContent>
                        <ChangePasswordForm onChangePassword={handleForm} />
                        <br /> {/* TODO: Use CSS for vertical separtion instead of HTML. */}
                        <div className="text-center text-sm">
                            {t("auth.changePassword.page.changedMind")}{" "}
                            <Link to='/dashboard' className="underline underline-offset-4"> {/* TODO: Set link to origin. */}
                                {t("auth.changePassword.page.goBack")}
                            </Link>
                        </div>
                    </CardContent>
                </Card>
            </div>
        </div>
    )
}
