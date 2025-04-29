
'use client';

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Checkbox } from "@/components/ui/checkbox";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { CalendarIcon, Loader2, Bike } from "lucide-react"; // Added Bike icon
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { useToast } from "@/hooks/use-toast";
import { useState, useTransition } from "react";
import { createTraining } from "@/lib/placeholder-data"; // Server action/API call
import { useRouter } from 'next/navigation';
import type { SkillLevel, MotorcycleType } from "@/lib/types";


const skillLevels: SkillLevel[] = ['Beginner', 'Intermediate', 'Advanced', 'Pro'];
const motorcycleTypes: MotorcycleType[] = ['125cc', '250cc', '450cc', 'Electric', 'Other']; // Defined motorcycle types

const formSchema = z.object({
  title: z.string().min(5, { message: "Title must be at least 5 characters." }),
  date: z.date({ required_error: "A date for the training is required." }),
  location: z.string().min(3, { message: "Location must be at least 3 characters." }),
  skillLevels: z.array(z.enum(skillLevels as [SkillLevel, ...SkillLevel[]])) // Ensure Zod gets the tuple type
                 .min(1, { message: "Select at least one skill level." }),
  motorcycleTypes: z.array(z.enum(motorcycleTypes as [MotorcycleType, ...MotorcycleType[]])) // Added motorcycleTypes schema
                      .min(1, { message: "Select at least one motorcycle type." }),
  description: z.string().min(10, { message: "Description must be at least 10 characters." }).max(500, {message: "Description cannot exceed 500 characters."}),
  maxRiders: z.coerce.number().int().positive().optional(),
});

type TrainingFormValues = z.infer<typeof formSchema>;

interface CreateTrainingFormProps {
    trainerId: string;
}

export function CreateTrainingForm({ trainerId }: CreateTrainingFormProps) {
  const [isPending, startTransition] = useTransition();
  const { toast } = useToast();
  const router = useRouter();

  const form = useForm<TrainingFormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: "",
      date: undefined,
      location: "",
      skillLevels: [],
      motorcycleTypes: [], // Default to empty array
      description: "",
      maxRiders: 10,
    },
  });

  function onSubmit(values: TrainingFormValues) {
    console.log("Form submitted with values:", values); // Debug log
    startTransition(async () => {
        // Ensure date is passed correctly
        const dataToSubmit = {
          ...values,
          date: values.date, // Keep as Date object
        };
        const result = await createTraining(trainerId, dataToSubmit);
        if (result.success) {
            toast({
                title: "Training Created!",
                description: result.message,
                variant: "default",
                 className: "bg-primary text-primary-foreground"
            });
            if(result.trainingId) {
                 router.push(`/trainings/${result.trainingId}`);
            } else {
                router.push('/trainings');
            }
            router.refresh();
        } else {
             toast({
                title: "Creation Failed",
                description: result.message || "An unexpected error occurred.",
                variant: "destructive",
            });
        }

    });
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
        <FormField
          control={form.control}
          name="title"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Training Title</FormLabel>
              <FormControl>
                <Input placeholder="e.g., Advanced Cornering Techniques" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="date"
          render={({ field }) => (
            <FormItem className="flex flex-col">
              <FormLabel>Date and Time</FormLabel>
               <Popover>
                <PopoverTrigger asChild>
                  <FormControl>
                    <Button
                      variant={"outline"}
                      className={cn(
                        "w-full justify-start text-left font-normal",
                        !field.value && "text-muted-foreground"
                      )}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {field.value ? format(field.value, "PPP p") : <span>Pick a date and time</span>}
                    </Button>
                  </FormControl>
                </PopoverTrigger>
                <PopoverContent className="w-auto p-0" align="start">
                  <Calendar
                    mode="single"
                    selected={field.value}
                    onSelect={(date) => {
                         if (date) {
                           // Preserve time if date already has one, otherwise default
                           const currentTime = field.value || new Date();
                           date.setHours(currentTime.getHours(), currentTime.getMinutes(), 0, 0);
                           field.onChange(date);
                         } else {
                            field.onChange(undefined);
                         }
                     }}
                    disabled={(date) => date < new Date(new Date().setHours(0,0,0,0))} // Disable past dates
                    initialFocus
                  />
                  <div className="p-3 border-t border-border">
                    <Input
                        type="time"
                        className="w-full p-2 border rounded"
                         defaultValue={field.value ? format(field.value, "HH:mm") : "10:00"}
                        onChange={(e) => {
                            const time = e.target.value;
                            const currentDate = field.value || new Date(); // Use existing date or today
                            if (time) {
                                const [hours, minutes] = time.split(':').map(Number);
                                const newDate = new Date(currentDate);
                                newDate.setHours(hours, minutes, 0, 0); // Set H, M, S, MS
                                field.onChange(newDate);
                            }
                        }}
                    />
                  </div>
                </PopoverContent>
              </Popover>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="location"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Location</FormLabel>
              <FormControl>
                <Input placeholder="e.g., MX Speed Park - Main Track" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

         <FormField
          control={form.control}
          name="skillLevels"
          render={() => (
            <FormItem>
              <div className="mb-4">
                <FormLabel className="text-base">Skill Levels</FormLabel>
                <FormDescription>
                  Select all applicable skill levels for this training.
                </FormDescription>
              </div>
              <div className="grid grid-cols-2 gap-4 sm:grid-cols-4">
                 {skillLevels.map((level) => (
                    <FormField
                      key={level}
                      control={form.control}
                      name="skillLevels"
                      render={({ field }) => {
                        return (
                          <FormItem
                            key={level}
                            className="flex flex-row items-start space-x-3 space-y-0"
                          >
                            <FormControl>
                              <Checkbox
                                checked={field.value?.includes(level)}
                                onCheckedChange={(checked) => {
                                  return checked
                                    ? field.onChange([...(field.value || []), level])
                                    : field.onChange(
                                        (field.value || []).filter(
                                          (value) => value !== level
                                        )
                                      )
                                }}
                              />
                            </FormControl>
                            <FormLabel className="font-normal">
                              {level}
                            </FormLabel>
                          </FormItem>
                        )
                      }}
                    />
                  ))}
              </div>
              <FormMessage />
            </FormItem>
          )}
        />

         {/* Motorcycle Types Checkboxes */}
          <FormField
          control={form.control}
          name="motorcycleTypes"
          render={() => (
            <FormItem>
              <div className="mb-4">
                <FormLabel className="text-base">Motorcycle Types</FormLabel>
                <FormDescription>
                  Select all applicable motorcycle types for this training.
                </FormDescription>
              </div>
              <div className="grid grid-cols-2 gap-4 sm:grid-cols-3"> {/* Adjusted grid columns */}
                 {motorcycleTypes.map((type) => (
                    <FormField
                      key={type}
                      control={form.control}
                      name="motorcycleTypes"
                      render={({ field }) => {
                        return (
                          <FormItem
                            key={type}
                            className="flex flex-row items-start space-x-3 space-y-0"
                          >
                            <FormControl>
                              <Checkbox
                                checked={field.value?.includes(type)}
                                onCheckedChange={(checked) => {
                                  return checked
                                    ? field.onChange([...(field.value || []), type])
                                    : field.onChange(
                                        (field.value || []).filter(
                                          (value) => value !== type
                                        )
                                      )
                                }}
                              />
                            </FormControl>
                             <FormLabel className="font-normal flex items-center gap-1">
                               <Bike className="h-4 w-4 text-muted-foreground" /> {type}
                            </FormLabel>
                          </FormItem>
                        )
                      }}
                    />
                  ))}
              </div>
              <FormMessage />
            </FormItem>
          )}
        />


         <FormField
          control={form.control}
          name="description"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Description</FormLabel>
              <FormControl>
                <Textarea
                  placeholder="Provide details about the training, what riders will learn, any prerequisites, etc."
                  className="resize-y min-h-[100px]"
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

         <FormField
          control={form.control}
          name="maxRiders"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Maximum Riders (Optional)</FormLabel>
              <FormControl>
                 <Input
                    type="number"
                    placeholder="e.g., 10"
                    {...field}
                    value={field.value ?? ""} // Handle potential undefined value for input
                    onChange={event => field.onChange(event.target.value === '' ? undefined : +event.target.value)} // Convert empty string to undefined
                 />
              </FormControl>
               <FormDescription>
                 Leave blank for unlimited participants.
               </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />


        <Button type="submit" className="w-full" disabled={isPending}>
           {isPending ? <Loader2 className="mr-2 h-4 w-4 animate-spin" /> : null}
          Create Training Session
        </Button>
      </form>
    </Form>
  );
}

