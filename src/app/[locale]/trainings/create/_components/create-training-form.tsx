'use client';

import React from 'react';
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
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import { CalendarIcon, Loader2, Bike, MapPin, AlertCircle, Lock, Unlock } from "lucide-react";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { useToast } from "@/hooks/use-toast";
import { useState, useEffect, useTransition } from "react";
import { createTraining, getLocations, getSkillLevels, getMotorcycleTypes } from "@/lib/placeholder-data";
import { useRouter } from 'next/navigation';
import type { SkillLevel, MotorcycleType, Site, RegistrationType, CreateTrainingData as CreateTrainingPayload } from "@/lib/types"; // Use specific payload type
import { useI18n } from '@/locales/client';
import { Skeleton } from "@/components/ui/skeleton";
import { Alert, AlertDescription } from "@/components/ui/alert";

const registrationTypes: RegistrationType[] = ['open', 'closed'];

const createFormSchema = (
    t: ReturnType<typeof useI18n>,
    availableSkills: SkillLevel[],
    availableTypes: MotorcycleType[]
) => z.object({
  title: z.string().min(5, { message: t('createTraining.form.titleError') }),
  date: z.date({ required_error: t('createTraining.form.dateError') }),
  locationId: z.string({ required_error: t('createTraining.form.locationError') }).min(1, { message: t('createTraining.form.locationError') }),
  skillLevels: z.array(z.enum(availableSkills as [SkillLevel, ...SkillLevel[]], { errorMap: () => ({ message: t('createTraining.form.skillLevelsError') }) }))
                 .min(1, { message: t('createTraining.form.skillLevelsError') }),
  motorcycleTypes: z.array(z.enum(availableTypes as [MotorcycleType, ...MotorcycleType[]], { errorMap: () => ({ message: t('createTraining.form.motorcycleTypesError') }) }))
                      .min(1, { message: t('createTraining.form.motorcycleTypesError') }),
  description: z.string()
                  .min(10, { message: t('createTraining.form.descriptionErrorShort') })
                  .max(500, {message: t('createTraining.form.descriptionErrorLong')}),
  maxRiders: z.coerce.number().int().positive({message: t('createTraining.form.maxRidersError')}).optional(),
  registrationType: z.enum(registrationTypes as [RegistrationType, ...RegistrationType[]], { required_error: t('createTraining.form.registrationTypeError') })
});

// This type represents the data coming *from* the form
type FormValues = z.infer<ReturnType<typeof createFormSchema>>;

interface CreateTrainingFormProps {
    trainerId: string;
}

export function CreateTrainingForm({ trainerId }: CreateTrainingFormProps) {
  const t = useI18n();
  const [isPending, startTransition] = useTransition();
  const [locations, setLocations] = useState<Site[]>([]);
  const [skillLevels, setSkillLevels] = useState<SkillLevel[]>([]);
  const [motorcycleTypes, setMotorcycleTypes] = useState<MotorcycleType[]>([]);
  const [isLoadingData, setIsLoadingData] = useState(true);
  const [loadingError, setLoadingError] = useState<string | null>(null);
  const { toast } = useToast();
  const router = useRouter();

  // Fetch initial data
  useEffect(() => {
    async function fetchData() {
      setIsLoadingData(true);
      setLoadingError(null);
      try {
        const [fetchedLocations, fetchedSkills, fetchedTypes] = await Promise.all([
          getLocations(),
          getSkillLevels(),
          getMotorcycleTypes(),
        ]);
        setLocations(fetchedLocations);
        setSkillLevels(fetchedSkills);
        setMotorcycleTypes(fetchedTypes);
      } catch (error) {
        console.error("Failed to fetch form data:", error);
        setLoadingError(t('createTraining.form.errorLoadData'));
        toast({
          title: t('error'),
          description: t('createTraining.form.errorLoadData'),
          variant: "destructive",
        });
      } finally {
        setIsLoadingData(false);
      }
    }
    fetchData();
  }, [t, toast]);

   const formSchema = React.useMemo(() => {
    return createFormSchema(t, skillLevels.length > 0 ? skillLevels : ['Beginner'], motorcycleTypes.length > 0 ? motorcycleTypes : ['125cc']);
   }, [t, skillLevels, motorcycleTypes]);


  const translateSkillLevel = (level: SkillLevel): string => {
    try { return t(`skillLevels.${level}`); } catch { return level; }
    };
    const translateMotorcycleType = (type: MotorcycleType): string => {
        try { return t(`motorcycleTypes.${type}`); } catch { return type; }
    };
     const translateRegistrationType = (type: RegistrationType): string => {
        try { return t(`registrationTypes.${type}`); } catch { return type; }
    };


  const form = useForm<FormValues>({ // Use FormValues type here
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: "",
      date: undefined,
      locationId: "",
      skillLevels: [],
      motorcycleTypes: [],
      description: "",
      maxRiders: undefined,
      registrationType: 'open',
    },
    mode: "onChange",
  });


  function onSubmit(values: FormValues) { // values are of FormValues type
    startTransition(async () => {
        // Ensure skill levels and types are valid based on fetched data
        const validSkillLevels = values.skillLevels.filter(sl => skillLevels.includes(sl));
        const validMotorcycleTypes = values.motorcycleTypes.filter(mt => motorcycleTypes.includes(mt));

        if (validSkillLevels.length !== values.skillLevels.length) {
             toast({ title: t('error'), description: t('createTraining.form.invalidSkillLevel'), variant: "destructive" });
             return;
        }
         if (validMotorcycleTypes.length !== values.motorcycleTypes.length) {
            toast({ title: t('error'), description: t('createTraining.form.invalidMotorcycleType'), variant: "destructive" });
             return;
        }

        // Prepare the payload for the createTraining function, matching CreateTrainingPayload
        const dataToSubmit: CreateTrainingPayload = {
          ...values,
          // Ensure maxRiders is number or undefined
          maxRiders: values.maxRiders !== undefined && !isNaN(values.maxRiders) ? Number(values.maxRiders) : undefined,
        };

        // Call the backend function with the correctly typed payload
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

   // Render loading state
   if (isLoadingData) {
    return (
      <div className="space-y-6">
        <Skeleton className="h-10 w-1/3" />
        <Skeleton className="h-10 w-full" />
        <Skeleton className="h-10 w-full" />
        <Skeleton className="h-10 w-full" />
         <div className="space-y-2">
             <Skeleton className="h-6 w-1/4" />
             <Skeleton className="h-4 w-1/2" />
             <div className="grid grid-cols-2 gap-4 sm:grid-cols-4">
                 <Skeleton className="h-10 w-full" />
                 <Skeleton className="h-10 w-full" />
                 <Skeleton className="h-10 w-full" />
                 <Skeleton className="h-10 w-full" />
             </div>
         </div>
         <div className="space-y-2">
             <Skeleton className="h-6 w-1/4" />
             <Skeleton className="h-4 w-1/2" />
             <div className="grid grid-cols-2 gap-4 sm:grid-cols-3">
                 <Skeleton className="h-10 w-full" />
                 <Skeleton className="h-10 w-full" />
                 <Skeleton className="h-10 w-full" />
             </div>
         </div>
         <Skeleton className="h-24 w-full" />
         <Skeleton className="h-10 w-full" />
         {/* Skeleton for registration type */}
         <div className="space-y-2">
            <Skeleton className="h-6 w-1/4" />
            <Skeleton className="h-4 w-1/2" />
             <div className="flex gap-4">
                 <Skeleton className="h-10 w-1/3" />
                 <Skeleton className="h-10 w-1/3" />
             </div>
         </div>
         <Skeleton className="h-10 w-full" /> {/* Submit button skeleton */}
      </div>
    );
  }

   // Render error state
  if (loadingError) {
      return (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertDescription>{loadingError}</AlertDescription>
        </Alert>
      );
  }

  // Render form once data is loaded
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
              <Select onValueChange={field.onChange} defaultValue={field.value} disabled={isLoadingData}>
                <FormControl>
                  <SelectTrigger>
                    <MapPin className="mr-2 h-4 w-4 text-muted-foreground" />
                    <SelectValue placeholder={isLoadingData ? t('createTraining.form.locationPlaceholderLoading') : (locations.length === 0 ? t('createTraining.form.locationNotAvailable') : t('createTraining.form.locationPlaceholder'))} />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  {locations.length === 0 && (
                     <SelectItem value="no-locations" disabled>{t('createTraining.form.locationNotAvailable')}</SelectItem>
                  )}
                  {locations.map((location) => (
                    <SelectItem key={location.id} value={location.id}>
                      {location.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
               {locations.length === 0 && !isLoadingData && (
                  <FormDescription className="text-destructive">
                     {t('createTraining.form.noLocationsAvailableAdmin')}
                 </FormDescription>
               )}
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
                {skillLevels.length === 0 && !isLoadingData && (
                    <FormDescription className="text-destructive mt-2">
                         {t('createTraining.form.noSkillLevelsAvailableAdmin')}
                    </FormDescription>
                )}
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
                {motorcycleTypes.length === 0 && !isLoadingData && (
                    <FormDescription className="text-destructive mt-2">
                         {t('createTraining.form.noMotorcycleTypesAvailableAdmin')}
                    </FormDescription>
                )}
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

        <FormField
            control={form.control}
            name="registrationType"
            render={({ field }) => (
                <FormItem className="space-y-3">
                <FormLabel>{t('createTraining.form.registrationTypeLabel')}</FormLabel>
                 <FormDescription>
                     {t('createTraining.form.registrationTypeDesc')}
                 </FormDescription>
                <FormControl>
                    <RadioGroup
                    onValueChange={field.onChange}
                    defaultValue={field.value}
                    className="flex flex-col space-y-1 md:flex-row md:space-y-0 md:space-x-4"
                    >
                    <FormItem className="flex items-center space-x-3 space-y-0">
                        <FormControl>
                        <RadioGroupItem value="open" />
                        </FormControl>
                        <FormLabel className="font-normal flex items-center">
                           <Unlock className="mr-2 h-4 w-4 text-green-600"/> {t('createTraining.form.registrationTypeOpen')}
                        </FormLabel>
                    </FormItem>
                    <FormItem className="flex items-center space-x-3 space-y-0">
                        <FormControl>
                        <RadioGroupItem value="closed" />
                        </FormControl>
                        <FormLabel className="font-normal flex items-center">
                           <Lock className="mr-2 h-4 w-4 text-red-600"/> {t('createTraining.form.registrationTypeClosed')}
                        </FormLabel>
                    </FormItem>
                    </RadioGroup>
                </FormControl>
                <FormMessage />
                </FormItem>
            )}
        />


        <Button type="submit" className="w-full" disabled={isPending || isLoadingData}>
           {isPending && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
           {isLoadingData ? t('createTraining.form.submitButtonLoading') : (isPending ? t('createTraining.form.submitButtonCreating') : t('createTraining.form.submitButton'))}
        </Button>
      </form>
    </Form>
  );
}
