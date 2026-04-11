import { t } from "i18next";
import { z } from "zod";
import { emailSchema, passwordSchema } from "./common";

export const forgotPasswordSchema = z.object({
    email: emailSchema,
}).required();

export type ForgotPassword = z.infer<typeof forgotPasswordSchema>;

export const changePasswordSchema = z.object({
    oldPassword: z.string().min(1, {
        message: t("auth.changePassword._validations.oldPasswordRequired"),
    }),
    newPassword: passwordSchema,
    confirmPassword: passwordSchema,
}).required()
    // NOTE: Defined twice as described here: https://github.com/orgs/react-hook-form/discussions/9772
    .refine((data) => data.newPassword === data.confirmPassword, {
        message: t("auth._validations.passwordsMustMatch"),
        path: ["newPassword"],
    })
    .refine((data) => data.newPassword === data.confirmPassword, {
        message: t("auth._validations.passwordsMustMatch"),
        path: ["confirmPassword"],
    });

export type ChangePassword = z.infer<typeof changePasswordSchema>;

export const signInSchema = z.object({
    email: emailSchema,
    password: passwordSchema,
});

export type SignIn = z.infer<typeof signInSchema>;

export const signUpSchema = z.object({
    email: emailSchema,
    password: passwordSchema,
    confirmPassword: passwordSchema,
}).required()
    // NOTE: Defined twice as described here: https://github.com/orgs/react-hook-form/discussions/9772
    .refine((data) => data.password === data.confirmPassword, {
        message: t("auth._validations.passwordsMustMatch"),
        path: ["password"],
    })
    .refine((data) => data.password === data.confirmPassword, {
        message: t("auth._validations.passwordsMustMatch"),
        path: ["confirmPassword"],
    });

export type SignUp = z.infer<typeof signUpSchema>;
