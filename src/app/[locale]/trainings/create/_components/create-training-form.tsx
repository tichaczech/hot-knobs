
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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import { CalendarIcon, Loader2, Bike, MapPin } from "lucide-react";
import { cn } from "@/lib/utils";
import { format } from "date-fns"; // Consider locale-aware formatting later
import { useToast } from "@/hooks/use-toast";
import { useState, useEffect, useTransition } from "react";
import { createTraining, getLocations } from "@/lib/placeholder-data";
import { useRouter } from 'next/navigation';
import type { SkillLevel, MotorcycleType, Location } from "@/lib/types";
import { useI18n } from '@/locales/client'; // Import client-side i18n hook

const skillLevels: SkillLevel[] = ['Beginner', 'Intermediate', 'Advanced', 'Pro'];
const motorcycleTypes: MotorcycleType[] = ['125cc', '250cc', '450cc', 'Electric', 'Other'];

// Form schema remains largely the same, Zod handles validation logic
// Translations are applied in the component rendering
const createFormSchema = (t: ReturnType<typeof useI18n>) => z.object({
  title: z.string().min(5, { message: t('createTraining.form.titleError') }),
  date: z.date({ required_error: t('createTraining.form.dateError') }),
  locationId: z.string({ required_error: t('createTraining.form.locationError') }).min(1, { message: t('createTraining.form.locationError') }),
  skillLevels: z.array(z.enum(skillLevels as [SkillLevel, ...SkillLevel[]]))
                 .min(1, { message: t('createTraining.form.skillLevelsError') }),
  motorcycleTypes: z.array(z.enum(motorcycleTypes as [MotorcycleType, ...MotorcycleType[]]))
                      .min(1, { message: t('createTraining.form.motorcycleTypesError') }),
  description: z.string()
                  .min(10, { message: t('createTraining.form.descriptionErrorShort') })
                  .max(500, {message: t('createTraining.form.descriptionErrorLong')}),
  maxRiders: z.coerce.number().int().positive({message: t('createTraining.form.maxRidersError')}).optional(),
});

type TrainingFormValues = z.infer<ReturnType<typeof createFormSchema>>;

interface CreateTrainingFormProps {
    trainerId: string;
}

export function CreateTrainingForm({ trainerId }: CreateTrainingFormProps) {
  const t = useI18n(); // Get translation function
  const formSchema = createFormSchema(t); // Create schema with translations
  const [isPending, startTransition] = useTransition();
  const [locations, setLocations] = useState<Location[]>([]);
  const [isLoadingLocations, setIsLoadingLocations] = useState(true);
  const { toast } = useToast();
  const router = useRouter();

  // Helper function to translate SkillLevel safely
    const translateSkillLevel = (level: SkillLevel): string => {
    try {
        return t(`skillLevels.${level}`);
    } catch (e) {
        console.warn(`Missing translation for skill level: ${level}`);
        return level; // Fallback
    }
    };

    // Helper function to translate MotorcycleType safely
    const translateMotorcycleType = (type: MotorcycleType): string => {
        try {
        return t(`motorcycleTypes.${type}`);
        } catch (e) {
        console.warn(`Missing translation for motorcycle type: ${type}`);
        return type; // Fallback
        }
    };


  useEffect(() => {
    async function fetchLocations() {
      setIsLoadingLocations(true);
      try {
        const fetchedLocations = await getLocations();
        setLocations(fetchedLocations);
      } catch (error) {
        console.error("Failed to fetch locations:", error);
        toast({
          title: t('error'),
          description: t('createTraining.form.errorLoadLocations'),
          variant: "destructive",
        });
      } finally {
        setIsLoadingLocations(false);
      }
    }
    fetchLocations();
  }, [t, toast]);


  const form = useForm<TrainingFormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: "",
      date: undefined,
      locationId: "",
      skillLevels: [],
      motorcycleTypes: [],
      description: "",
      maxRiders: undefined, // Set undefined as default for optional number
    },
  });

  function onSubmit(values: TrainingFormValues) {
    startTransition(async () => {
        const dataToSubmit = {
          ...values,
          date: values.date,
          // Ensure maxRiders is number or undefined
          maxRiders: values.maxRiders !== undefined && !isNaN(values.maxRiders) ? Number(values.maxRiders) : undefined,
        };
        const result = await createTraining(trainerId, dataToSubmit);
        if (result.success) {
            toast({
                title: t('createTraining.form.successToastTitle'),
                description: t('createTraining.form.successToastDesc', { message: result.message || t('success') }),
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
                title: t('createTraining.form.errorToastTitle'),
                description: t('createTraining.form.errorToastDesc', { message: result.message || t('error') }),
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
              <FormLabel>{t('createTraining.form.titleLabel')}</FormLabel>
              <FormControl>
                <Input placeholder={t('createTraining.form.titlePlaceholder')} {...field} />
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
              <FormLabel>{t('createTraining.form.dateLabel')}</FormLabel>
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
                      {/* TODO: Locale-aware date formatting */}
                      {field.value ? format(field.value, "PPP p") : <span>{t('createTraining.form.datePlaceholder')}</span>}
                    </Button>
                  </FormControl>
                </PopoverTrigger>
                <PopoverContent className="w-auto p-0" align="start">
                  <Calendar
                    mode="single"
                    selected={field.value}
                    onSelect={(date) => {
                         if (date) {
                           const currentTime = field.value || new Date();
                           date.setHours(currentTime.getHours(), currentTime.getMinutes(), 0, 0);
                           field.onChange(date);
                         } else {
                            field.onChange(undefined);
                         }
                     }}
                    disabled={(date) => date < new Date(new Date().setHours(0,0,0,0))}
                    initialFocus
                  />
                  <div className="p-3 border-t border-border">
                    <Input
                        type="time"
                        className="w-full p-2 border rounded"
                         defaultValue={field.value ? format(field.value, "HH:mm") : "10:00"}
                        onChange={(e) => {
                            const time = e.target.value;
                            const currentDate = field.value || new Date();
                            if (time) {
                                const [hours, minutes] = time.split(':').map(Number);
                                const newDate = new Date(currentDate);
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
          name="locationId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>{t('createTraining.form.locationLabel')}</FormLabel>
              <Select onValueChange={field.onChange} defaultValue={field.value} disabled={isLoadingLocations}>
                <FormControl>
                  <SelectTrigger>
                    <MapPin className="mr-2 h-4 w-4 text-muted-foreground" />
                    <SelectValue placeholder={isLoadingLocations ? t('createTraining.form.locationPlaceholderLoading') : t('createTraining.form.locationPlaceholder')} />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  {!isLoadingLocations && locations.length === 0 && (
                     <SelectItem value="no-locations" disabled>{t('createTraining.form.locationNotAvailable')}</SelectItem>
                  )}
                  {locations.map((location) => (
                    <SelectItem key={location.id} value={location.id}>
                      {location.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
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
                <FormLabel className="text-base">{t('createTraining.form.skillLevelsLabel')}</FormLabel>
                <FormDescription>
                  {t('createTraining.form.skillLevelsDesc')}
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
                              {translateSkillLevel(level)}
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
          name="motorcycleTypes"
          render={() => (
            <FormItem>
              <div className="mb-4">
                <FormLabel className="text-base">{t('createTraining.form.motorcycleTypesLabel')}</FormLabel>
                <FormDescription>
                  {t('createTraining.form.motorcycleTypesDesc')}
                </FormDescription>
              </div>
              <div className="grid grid-cols-2 gap-4 sm:grid-cols-3">
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
                               <Bike className="h-4 w-4 text-muted-foreground" /> {translateMotorcycleType(type)}
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
              <FormLabel>{t('createTraining.form.descriptionLabel')}</FormLabel>
              <FormControl>
                <Textarea
                  placeholder={t('createTraining.form.descriptionPlaceholder')}
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
              <FormLabel>{t('createTraining.form.maxRidersLabel')}</FormLabel>
              <FormControl>
                 <Input
                    type="number"
                    placeholder={t('createTraining.form.maxRidersPlaceholder')}
                    {...field}
                    // Ensure value is controlled correctly for optional number
                    value={field.value ?? ""}
                    onChange={event => field.onChange(event.target.value === '' ? undefined : +event.target.value)}
                 />
              </FormControl>
               <FormDescription>
                 {t('createTraining.form.maxRidersDesc')}
               </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />


        <Button type="submit" className="w-full" disabled={isPending || isLoadingLocations}>
           {isPending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
           {isLoadingLocations ? t('createTraining.form.submitButtonLoading') : (isPending ? t('createTraining.form.submitButtonCreating') : t('createTraining.form.submitButton'))}
        </Button>
      </form>
    </Form>
  );
}
