export type SkillLevel = 'Beginner' | 'Intermediate' | 'Advanced' | 'Pro';
export type UserRole = 'rider' | 'trainer' | 'guest'; // Guest for logged-out state
export type MotorcycleType = '125cc' | '250cc' | '450cc' | 'Electric' | 'Other';
export type RegistrationType = 'open' | 'closed';
export type RegistrationStatus = 'Created' | 'Confirmed' | 'Rejected' | 'Cancelled' | 'Waiting'; // New status enum

// Represents a physical location where trainings can occur
export interface Location {
    id: string;
    name: string;
    address?: string; // Optional address details
    latitude?: number; // Added latitude
    longitude?: number; // Added longitude
    // Could add more fields like website, contact, etc.
}

// Represents a user's registration for a specific training session
export interface Registration {
    userId: string;
    status: RegistrationStatus;
    registeredAt: Date; // Timestamp when the registration/request was made
    reason?: string; // Optional reason for Rejection or Cancellation (by trainer or rider)
    // Timestamps for status changes could be added if needed (e.g., confirmedAt, rejectedAt)
}

export interface TrainingSession {
  id: string;
  trainerId: string; // ID of the trainer who created it
  trainerName: string; // Name of the trainer
  title: string;
  date: Date;
  locationId: string; // ID referencing the Location
  locationName: string; // Denormalized location name for easy display
  skillLevels: SkillLevel[];
  motorcycleTypes: MotorcycleType[];
  description: string;
  registrationType: RegistrationType; // 'open' or 'closed'
  registrations: Registration[]; // Consolidated list of all registrations and their statuses
  maxRiders?: number;
}

export interface User {
    id: string;
    name: string;
    email: string;
    role: UserRole;
}

// Placeholder for authentication context or user state
export interface AppState {
    currentUser: User | null;
}

// Type definition for data passed to createTraining function
// Ensure this matches the structure expected by the function
export interface CreateTrainingData {
    title: string;
    date: Date;
    locationId: string;
    skillLevels: SkillLevel[];
    motorcycleTypes: MotorcycleType[];
    description: string;
    registrationType: RegistrationType;
    maxRiders?: number;
}

// Interface for results returned by registration-related actions
export interface RegistrationResult {
    success: boolean;
    message: string;
    status?: RegistrationStatus; // Optionally return the resulting status
}
