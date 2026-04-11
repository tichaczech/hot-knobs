import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { ForgotPasswordForm } from "./form";
import { useTranslation } from "react-i18next";
import { useAuth } from "@/contexts/auth";
import { toast } from "sonner"; // Assuming you use sonner for toasts
import { Link } from "@tanstack/react-router";
import { type ForgotPassword } from "@/types/auth";

export default function ForgotPasswordPage() {
    const { resetPassword } = useAuth();
    const { t } = useTranslation();

    const handleForm = async (values: ForgotPassword) => {
        if (values.email) {
            try {
                await resetPassword(values.email);
                toast.success(t("auth.forgotPassword.form.successMessage"));
            } catch (error) {
                console.error("Error sending password reset email:", error);
                toast.error(t("auth.forgotPassword.form.errorMessage"));
            }
        }
    };

    return (
        <div className="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-6 md:p-10">
            <div className="flex w-full max-w-sm flex-col gap-6">
                <div className="flex flex-col gap-6">
                    <Card>
                        <CardHeader className="text-center justify-center">
                            <CardTitle className="text-xl">
                                {t("auth.forgotPassword.page.title")}
                            </CardTitle>
                            <CardDescription>
                                {t("auth.forgotPassword.page.description")}
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <ForgotPasswordForm onForgotPassword={handleForm} />
                            <br /> {/* TODO: Use CSS for vertical separtion instead of HTML. */}
                            <div className="text-center text-sm">
                                {t("auth.forgotPassword.page.remembered")}{" "}
                                <Link to='/signin' className="underline underline-offset-4">
                                    {t("auth.forgotPassword.page.signIn")}
                                </Link>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </div>
        </div>
    );
}
