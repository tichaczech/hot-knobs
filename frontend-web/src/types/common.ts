import { t } from "i18next";
import { type LucideIcon } from "lucide-react";
import { z } from "zod";

export const emailSchema = z.string().email({ message: t("_validations.validEmail") });

export type Email = z.infer<typeof emailSchema>;

export const navigationItemSchema = z.object({
    title: z.string(),
    url: z.string(),
    icon: z.custom<LucideIcon>()
});

export type NavigationItem = z.infer<typeof navigationItemSchema>;

export const passwordSchema = z.string().min(8, { message: t("auth._validations.validPassword") });

export type Password = z.infer<typeof passwordSchema>;
