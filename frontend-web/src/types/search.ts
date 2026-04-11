import { z } from "zod";

export const signInSearchSchema = z.object({
  continueWithUrl: z.string().url().catch('/dashboard'),
});

export type SignInSearch = z.infer<typeof signInSearchSchema>;

