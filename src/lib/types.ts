export type SkillLevel = 'Beginner' | 'Intermediate' | 'Advanced' | 'Pro';
export type UserRole = 'rider' | 'trainer' | 'guest'; // Guest for logged-out state
export type MotorcycleType = '125cc' | '250cc' | '450cc' | 'Electric' | 'Other'; // Added MotorcycleType

export interface TrainingSession {
  id: string;
  trainerId: string; // ID of the trainer who created it
  trainerName: string; // Name of the trainer
  title: string;
  date: Date;
  location: string;
  skillLevels: SkillLevel[];
  motorcycleTypes: MotorcycleType[]; // Added motorcycleTypes field
  description: string;
  registeredRiders?: string[]; // Array of user IDs registered
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
