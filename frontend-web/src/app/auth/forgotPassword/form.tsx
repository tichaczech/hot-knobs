import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { forgotPasswordSchema, type ForgotPassword } from "@/types/auth";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

export interface ForgotPasswordFormProps {
    onForgotPassword: (values: ForgotPassword) => Promise<void>;
}

export function ForgotPasswordForm({ onForgotPassword}: ForgotPasswordFormProps) {
    const form = useForm<ForgotPassword>({
        resolver: zodResolver(forgotPasswordSchema),
        defaultValues: {
            email: "",
        },
    });
    const { t } = useTranslation();

    const onSubmit: SubmitHandler<ForgotPassword> = (values: ForgotPassword) => onForgotPassword(values);

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                <FormField control={form.control} name="email" render={({ field }) => (
                    <FormItem>
                        <FormLabel htmlFor="email">{t("auth._common.email")}</FormLabel>
                        <FormControl>
                            <Input placeholder={t("auth._common.emailPlaceholder")} {...field} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <Button type="submit" className="w-full" disabled={form.formState.isSubmitting}>
                    {form.formState.isSubmitting ? t("auth.forgotPassword.form.submitting") : t("auth.forgotPassword.form.submit")}
                </Button>
            </form>
        </Form>
    );
}
