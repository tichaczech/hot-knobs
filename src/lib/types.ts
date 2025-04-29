export type SkillLevel = 'Beginner' | 'Intermediate' | 'Advanced' | 'Pro';
export type UserRole = 'rider' | 'trainer' | 'guest'; // Guest for logged-out state
export type MotorcycleType = '125cc' | '250cc' | '450cc' | 'Electric' | 'Other'; // Added MotorcycleType
export type RegistrationType = 'open' | 'closed'; // Added RegistrationType

// Represents a physical location where trainings can occur
export interface Location {
    id: string;
    name: string;
    address?: string; // Optional address details
    latitude?: number; // Added latitude
    longitude?: number; // Added longitude
    // Could add more fields like website, contact, etc.
}

// Represents a reason for rejection or cancellation
export interface RegistrationStatusReason {
    userId: string;
    reason?: string;
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
  motorcycleTypes: MotorcycleType[]; // Added motorcycleTypes field
  description: string;
  registrationType: RegistrationType; // 'open' or 'closed'
  registeredRiders?: string[]; // Array of user IDs registered (approved for 'closed')
  pendingRegistrations?: string[]; // Array of user IDs awaiting approval for 'closed'
  rejectedRegistrations?: RegistrationStatusReason[]; // Array of rejected user IDs and reasons for 'closed'
  cancelledRegistrations?: RegistrationStatusReason[]; // Array of cancelled user IDs and reasons for 'open'
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

