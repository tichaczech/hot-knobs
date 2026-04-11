import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { signInSchema, type SignIn } from "@/types/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { Link } from "@tanstack/react-router";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

export interface SignUpFormProps {
    onSignInWithEmail: (values: SignIn) => Promise<void>;
}

export function SignInForm({ onSignInWithEmail }: SignUpFormProps) {
    const form = useForm<SignIn>({
        resolver: zodResolver(signInSchema),
        defaultValues: {
            email: "",
            password: "",
        },
    });
    const { t } = useTranslation();

    const onSubmit: SubmitHandler<SignIn> = (values: SignIn) => onSignInWithEmail(values);

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                <FormField control={form.control} name="email" render={({ field }) => (
                    <FormItem>
                        <FormLabel htmlFor="email">{t("auth._common.email")}</FormLabel>
                        <FormControl><Input {...field} placeholder={t("auth._common.emailPlaceholder")} /></FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <FormField control={form.control} name="password" render={({ field }) => (
                    <FormItem>
                        <div className="flex items-center">
                            <FormLabel htmlFor="password">{t("auth._common.password")}</FormLabel>
                            <Link to="/forgotPassword" className="ml-auto text-sm underline underline-offset-4">{t("auth.signIn.form.forgotPasswordLink")}</Link>
                        </div>
                        <FormControl>
                            <Input {...field} type="password" placeholder={t("auth._common.passwordPlaceholder")} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <Button type="submit" className="w-full" disabled={form.formState.isSubmitting}>
                    {form.formState.isSubmitting ? t("auth.signIn.form.submitting") : t("auth.signIn.form.submit")}
                </Button>
            </form>
        </Form>
    );
}
