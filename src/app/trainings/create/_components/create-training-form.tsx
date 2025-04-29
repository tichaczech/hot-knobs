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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { CalendarIcon, Loader2 } from "lucide-react";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { useToast } from "@/hooks/use-toast";
import { useState, useTransition } from "react";
import { createTraining } from "@/lib/placeholder-data"; // Server action/API call
import { useRouter } from 'next/navigation';


const formSchema = z.object({
  title: z.string().min(5, { message: "Title must be at least 5 characters." }),
  date: z.date({ required_error: "A date for the training is required." }),
  location: z.string().min(3, { message: "Location must be at least 3 characters." }),
  skillLevel: z.enum(['Beginner', 'Intermediate', 'Advanced', 'Pro']),
  description: z.string().min(10, { message: "Description must be at least 10 characters." }).max(500, {message: "Description cannot exceed 500 characters."}),
  maxRiders: z.coerce.number().int().positive().optional(), // Make maxRiders optional but must be positive int if provided
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
      skillLevel: 'Intermediate', // Default skill level
      description: "",
      maxRiders: 10, // Default max riders
    },
  });

  function onSubmit(values: TrainingFormValues) {
    startTransition(async () => {
        const result = await createTraining(trainerId, values);
        if (result.success) {
            toast({
                title: "Training Created!",
                description: result.message,
                variant: "default",
                 className: "bg-primary text-primary-foreground"
            });
            // Redirect to the newly created training details page or the main trainings page
            if(result.trainingId) {
                 router.push(`/trainings/${result.trainingId}`);
            } else {
                router.push('/trainings');
            }
            router.refresh(); // Ensure layout potentially showing trainer's sessions updates
        } else {
             toast({
                title: "Creation Failed",
                description: result.message,
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
                      {field.value ? format(field.value, "PPP HH:mm") : <span>Pick a date and time</span>}
                    </Button>
                  </FormControl>
                </PopoverTrigger>
                <PopoverContent className="w-auto p-0" align="start">
                  <Calendar
                    mode="single"
                    selected={field.value}
                    onSelect={field.onChange}
                    disabled={(date) => date < new Date(new Date().setHours(0,0,0,0))} // Disable past dates
                    initialFocus
                  />
                   {/* Basic Time Input - Consider a dedicated time picker component for better UX */}
                  <div className="p-3 border-t border-border">
                    <input
                        type="time"
                        className="w-full p-2 border rounded"
                         defaultValue={field.value ? format(field.value, "HH:mm") : "10:00"}
                        onChange={(e) => {
                            const time = e.target.value;
                            if (field.value && time) {
                                const [hours, minutes] = time.split(':').map(Number);
                                const newDate = new Date(field.value);
                                newDate.setHours(hours, minutes);
                                field.onChange(newDate);
                            } else if (time){
                                // If no date is selected yet, just set a default date (today) and apply time
                                const [hours, minutes] = time.split(':').map(Number);
                                const newDate = new Date();
                                newDate.setHours(hours, minutes, 0, 0);
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
          name="skillLevel"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Skill Level</FormLabel>
              <Select onValueChange={field.onChange} defaultValue={field.value}>
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Select the target skill level" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  <SelectItem value="Beginner">Beginner</SelectItem>
                  <SelectItem value="Intermediate">Intermediate</SelectItem>
                  <SelectItem value="Advanced">Advanced</SelectItem>
                  <SelectItem value="Pro">Pro</SelectItem>
                </SelectContent>
              </Select>
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
                 <Input type="number" placeholder="e.g., 10" {...field} onChange={event => field.onChange(+event.target.value)} />
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
