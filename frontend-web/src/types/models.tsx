import { z } from "zod";

export const entityIdSchema = z.string().uuid();

export type EntityId = z.infer<typeof entityIdSchema>;

export const entitySchema = z.object({
    id: entityIdSchema,
    description: z.string().optional(),
});

export interface Entity extends z.infer<typeof entitySchema> { }

export const operatorSchema = entitySchema.extend({
    name: z.string(),
});

export interface Operator extends Entity, z.infer<typeof operatorSchema> { }

export type OperatorListItem = Pick<Operator, "id" | "description" | "name">;
