"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, type SubmitHandler } from "react-hook-form";
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
import { useTranslation } from "react-i18next";
import { changePasswordSchema, type ChangePassword } from "@/types/auth";

interface ChangePasswordFormProps {
    onChangePassword: (values: ChangePassword) => Promise<void>;
}

export function ChangePasswordForm({ onChangePassword }: ChangePasswordFormProps) {
    const form = useForm<ChangePassword>({
        resolver: zodResolver(changePasswordSchema),
        defaultValues: {
            oldPassword: "",
            newPassword: "",
            confirmPassword: "",
        },
    });
    const { t } = useTranslation();

    const onSubmit: SubmitHandler<ChangePassword> = (values: ChangePassword) => onChangePassword(values);

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                <FormField control={form.control} name="oldPassword" render={({ field }) => (
                    <FormItem>
                        <FormLabel>{t("auth.changePassword.form.oldPasswordLabel")}</FormLabel>
                        <FormControl>
                            <Input type="password" placeholder={t("auth._common.passwordPlaceholder")} {...field} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <FormField control={form.control} name="newPassword" render={({ field }) => (
                    <FormItem>
                        <FormLabel>{t("auth.changePassword.form.newPasswordLabel")}</FormLabel>
                        <FormControl>
                            <Input type="password" placeholder={t("auth._common.passwordPlaceholder")} {...field} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <FormField control={form.control} name="confirmPassword" render={({ field }) => (
                    <FormItem>
                        <FormLabel>{t("auth.changePassword.form.confirmPasswordLabel")}</FormLabel>
                        <FormControl>
                            <Input type="password" placeholder={t("auth._common.passwordPlaceholder")} {...field} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <Button type="submit" className="w-full" disabled={form.formState.isSubmitting}>
                    {form.formState.isSubmitting ? t("auth.changePassword.form.submitting") : t("auth.changePassword.form.submit")}
                </Button>
            </form>
        </Form>
    );
}
