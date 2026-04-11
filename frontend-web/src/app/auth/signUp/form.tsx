import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { type SignUp, signUpSchema } from "@/types/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

export interface SignUpFormProps {
    onSignUpWithEmail: (values: SignUp) => Promise<void>;
}

export function SignUpForm({ onSignUpWithEmail }: SignUpFormProps) {
    const form = useForm<SignUp>({
        resolver: zodResolver(signUpSchema),
        defaultValues: {
            email: "",
            password: "",
            confirmPassword: "",
        },
    });
    const { t } = useTranslation();

    const onSubmit: SubmitHandler<SignUp> = (values: SignUp) => onSignUpWithEmail(values);

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
                        <FormLabel htmlFor="password">{t("auth._common.password")}</FormLabel>
                        <FormControl><Input {...field} type="password" placeholder={t("auth._common.passwordPlaceholder")} /></FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <FormField control={form.control} name="confirmPassword" render={({ field }) => (
                    <FormItem>
                        <FormLabel htmlFor="confirmPassword">{t("auth.signUp.form.confirmPasswordLabel")}</FormLabel>
                        <FormControl><Input {...field} type="password" placeholder={t("auth._common.passwordPlaceholder")} /></FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <Button type="submit" className="w-full" disabled={form.formState.isSubmitting} >
                    {form.formState.isSubmitting ? t("auth.signUp.form.submitting") : t("auth.signUp.form.submit")}
                </Button>
            </form>
        </Form>
    );
}
