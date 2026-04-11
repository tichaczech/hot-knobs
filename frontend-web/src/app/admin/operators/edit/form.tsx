import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { operatorSchema, type Operator } from "@/types/models";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, type SubmitHandler } from "react-hook-form";
import { useTranslation } from "react-i18next";

export interface OperatorEditFormProps {
    onSave: (values: Operator) => Promise<void>;
}

export function OperatorEditForm(formProps: OperatorEditFormProps) {
    const form = useForm<Operator>({
        resolver: zodResolver(operatorSchema),
        defaultValues: {
            description: "",
            id: undefined,
            name: "",
        },
    });
    const { t } = useTranslation();

    const onSubmit: SubmitHandler<Operator> = (data) => formProps.onSave(data);

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                <input type="hidden" {...form.register("id")} />
                <FormField control={form.control} name="name" render={({ field }) => (
                    <FormItem>
                        <FormLabel htmlFor="name">{t("operator._common.name")}</FormLabel>
                        <FormControl>
                            <Input {...field} placeholder={t("operator._common.namePlaceholder")} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <FormField control={form.control} name="description" render={({ field }) => (
                    <FormItem>
                        <FormLabel htmlFor="description">{t("_common.description")}</FormLabel>
                        <FormControl>
                            <Input {...field} placeholder={t("_common.descriptionPlaceholder")} />
                        </FormControl>
                        <FormMessage />
                    </FormItem>
                )} />
                <Button type="submit" className="w-full" disabled={form.formState.isSubmitting}>
                    {form.formState.isSubmitting ? t("_buttons.saving") : t("_buttons.save")}
                </Button>
            </form>
        </Form>
    );
}
