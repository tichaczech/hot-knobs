// export type SkillLevel = 'Beginner' | 'Intermediate' | 'Advanced' | 'Pro';
// export type MotorcycleType = '125cc' | '250cc' | '450cc' | 'Electric' | 'Other';
export type UserRole = 'admin' | 'rider' | 'trainer' | 'guest'; // Guest for logged-out state
export type RegistrationType = 'open' | 'closed';
export type RegistrationStatus = 'created' | 'confirmed' | 'rejected' | 'cancelled' | 'waiting';

export interface User {
    id: string;
    name: string;
    email: string;
    role: UserRole;
}

export interface GPSCoordinates {
    latitude: number;
    longitude: number;
}

export interface Site {
    id: string;
    name: string;
    address?: string; // Optional address details
    location?: GPSCoordinates; // Site coordinates
    arrival?: { // Arrival to the Site
        instructions?: string; // Instructions for arrival
        point?: GPSCoordinates; // Coordinates for arrival
    }
    parking?: { // Parking at the Site
        instructions?: string; // Instructions for parking
        point?: GPSCoordinates; // Coordinates for parking
    }
    // Could add more fields like website, contact, etc.
}

// Represents an organization that organizes events (race, training or open practice)
export interface Organizer {
    id: string;
    name: string;
    // Could add more fields like website, contact, etc.
}

export interface Event {
    id: string;
    title: string;
    date: Date;
    organizerId: string; // ID of the Organizer who organizes the event
    organizerName: string; // Denormalized Organizer name for easy display
    siteId: string; // ID of the Site where the event takes place
    siteName: string; // Denormalized Site name for easy display
    description: string;
    registrationType: RegistrationType; // 'open' or 'closed'
    maxRiders?: number;
  }

export interface Trainer {
    id: string;
    name: string;
    // Could add more fields like website, contact, etc.
}

export interface SkillLevel {
    id: string;
    name: string;
    organizerId: string; // ID of the Organizer who organizes the event
}

export interface Category {
    id: string;
    name: string;
    organizerId: string; // ID of the Organizer who organizes the event
}

export interface TrainingSession extends Event {
    trainerId: string; // ID of the Trainer who will lead the session
    trainerName: string; // Name of the Trainer
    skillLevels: SkillLevel[];
    categories: Category[];
  }
  
export interface Participant {
    id: string;
    name: string;
    organizers: {
        id: string;
        data: any; // Placeholder for organizer data
    }
    // Could add more fields like website, contact, etc.
}

// Represents a user's registration for a specific training session
export interface Registration {
    id: string;
    eventId: string;
    participantId: string;
    status: RegistrationStatus;
    registeredAt: Date; // Timestamp when the registration/request was made
    registeredBy: string; // ID of the User who made the registration/request
    reason?: string; // Optional reason for Rejection or Cancellation (by trainer or rider)
    // Timestamps for status changes could be added if needed (e.g., confirmedAt, rejectedAt)
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
