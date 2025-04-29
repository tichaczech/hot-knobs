import type { TrainingSession, User, SkillLevel, MotorcycleType, Location, RegistrationType, CreateTrainingData, RegistrationStatusReason } from './types'; // Updated imports

// --- Placeholder Locations ---
export const placeholderLocations: Location[] = [
    { id: 'loc-1', name: 'Rocky Valley Trails', address: '123 Trailhead Rd, Mountain View', latitude: 37.4220, longitude: -122.0841 },
    { id: 'loc-2', name: 'MX Speed Park', address: '456 Motocross Ln, Fastville', latitude: 34.0522, longitude: -118.2437 },
    { id: 'loc-3', name: 'Steep Mountain Pass', address: '789 Summit Ave, High Peaks', latitude: 39.7392, longitude: -104.9903 },
    { id: 'loc-4', name: 'Desert Scramble Zone', address: '101 Cactus Flats, Sandy Plains', latitude: 33.6846, longitude: -117.8265 },
];

// --- Placeholder Skill Levels ---
export const placeholderSkillLevels: SkillLevel[] = ['Beginner', 'Intermediate', 'Advanced', 'Pro'];

// --- Placeholder Motorcycle Types ---
export const placeholderMotorcycleTypes: MotorcycleType[] = ['125cc', '250cc', '450cc', 'Electric', 'Other'];


// --- Placeholder Users ---
export const placeholderUsers: User[] = [
  { id: 'user-1', name: 'Alice Rider', email: 'alice@example.com', role: 'rider' },
  { id: 'user-2', name: 'Bob Trainer', email: 'bob@example.com', role: 'trainer' },
  { id: 'user-3', name: 'Charlie Rider', email: 'charlie@example.com', role: 'rider' },
  { id: 'user-4', name: 'Diana Pending', email: 'diana@example.com', role: 'rider' }, // New user for pending
];

// --- Placeholder Trainings ---
// Updated to include new fields
export let placeholderTrainings: TrainingSession[] = [ // Use let to allow modification
  {
    id: 'ts-1',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Enduro Basics Clinic (Open)',
    date: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000), // 2 days from now
    locationId: 'loc-1',
    locationName: 'Rocky Valley Trails',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Other'],
    description: 'Focus on fundamental enduro techniques: body positioning, braking, and small obstacles. Open registration.',
    registrationType: 'open', // Explicitly open
    registeredRiders: ['user-1'],
    pendingRegistrations: [],
    rejectedRegistrations: [],
    cancelledRegistrations: [],
    maxRiders: 10,
  },
  {
    id: 'ts-2',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Motocross Cornering Masterclass (Closed)',
    date: new Date(Date.now() + 5 * 24 * 60 * 60 * 1000), // 5 days from now
    locationId: 'loc-2',
    locationName: 'MX Speed Park',
    skillLevels: ['Intermediate', 'Advanced'],
    motorcycleTypes: ['250cc', '450cc'],
    description: 'Advanced cornering drills, ruts, and berms. Improve your lap times! Requires trainer approval.',
    registrationType: 'closed', // Explicitly closed
    registeredRiders: ['user-3'], // Charlie is pre-approved
    pendingRegistrations: ['user-4'], // Diana is pending
    rejectedRegistrations: [{ userId: 'user-1', reason: 'Skill level mismatch' }], // Alice was rejected
    cancelledRegistrations: [],
    maxRiders: 8,
  },
  {
    id: 'ts-3',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Advanced Hill Climb Techniques (Open)',
    date: new Date(Date.now() + 10 * 24 * 60 * 60 * 1000), // 10 days from now
    locationId: 'loc-3',
    locationName: 'Steep Mountain Pass',
    skillLevels: ['Advanced', 'Pro'],
    motorcycleTypes: ['250cc', '450cc', 'Other'],
    description: 'Learn techniques for tackling challenging ascents, line selection, and throttle control. Open registration.',
    registrationType: 'open', // Explicitly open
    registeredRiders: ['user-1', 'user-3'],
    pendingRegistrations: [],
    rejectedRegistrations: [],
    cancelledRegistrations: [],
    maxRiders: 6,
  },
  {
    id: 'ts-4',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Introduction to Motocross Jumps (Open)',
    date: new Date(Date.now() + 15 * 24 * 60 * 60 * 1000), // 15 days from now
    locationId: 'loc-2', // Same location as ts-2
    locationName: 'MX Speed Park',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Electric'],
    description: 'Safely learn the basics of jumping small tabletops and rollers. Open registration.',
    registrationType: 'open', // Explicitly open
    registeredRiders: ['user-3'],
    pendingRegistrations: [],
    rejectedRegistrations: [],
    cancelledRegistrations: [],
    maxRiders: 12,
  },
];

// --- Data Fetching Functions ---

// Fetch all available locations
export async function getLocations(): Promise<Location[]> {
    await new Promise(resolve => setTimeout(resolve, 40)); // Simulate network delay
    return JSON.parse(JSON.stringify(placeholderLocations));
}

// Fetch location by ID
export async function getLocationById(id: string): Promise<Location | undefined> {
    await new Promise(resolve => setTimeout(resolve, 30)); // Simulate network delay
    const location = placeholderLocations.find(loc => loc.id === id);
    return location ? JSON.parse(JSON.stringify(location)) : undefined;
}

// Fetch all available skill levels
export async function getSkillLevels(): Promise<SkillLevel[]> {
    await new Promise(resolve => setTimeout(resolve, 25)); // Simulate network delay
    return JSON.parse(JSON.stringify(placeholderSkillLevels));
}

// Fetch all available motorcycle types
export async function getMotorcycleTypes(): Promise<MotorcycleType[]> {
    await new Promise(resolve => setTimeout(resolve, 25)); // Simulate network delay
    return JSON.parse(JSON.stringify(placeholderMotorcycleTypes));
}


// Fetch all trainings
export async function getTrainings(): Promise<TrainingSession[]> {
  await new Promise(resolve => setTimeout(resolve, 50));
  // Deep copy and ensure date is Date object
  return JSON.parse(JSON.stringify(placeholderTrainings)).map((t: any) => ({ ...t, date: new Date(t.date) }));
}

// Fetch training by ID
export async function getTrainingById(id: string): Promise<TrainingSession | undefined> {
  await new Promise(resolve => setTimeout(resolve, 50));
   const training = placeholderTrainings.find(t => t.id === id);
   // Ensure date is a Date object and return deep copy
   return training ? { ...JSON.parse(JSON.stringify(training)), date: new Date(training.date) } : undefined;
}

// Fetch trainings a user is ACTUALLY registered for (approved or open)
export async function getMyRegisteredTrainings(userId: string): Promise<TrainingSession[]> {
    await new Promise(resolve => setTimeout(resolve, 50));
    const allTrainings = JSON.parse(JSON.stringify(placeholderTrainings)).map((t: any) => ({ ...t, date: new Date(t.date) }));
    return allTrainings.filter(t =>
        (t.registrationType === 'open' && t.registeredRiders?.includes(userId) && !t.cancelledRegistrations?.some((cr: RegistrationStatusReason) => cr.userId === userId)) || // Is registered and not cancelled for open
        (t.registrationType === 'closed' && t.registeredRiders?.includes(userId)) // Is explicitly approved for closed
    );
}

// Fetch trainings a user has pending registration for
export async function getMyPendingTrainings(userId: string): Promise<TrainingSession[]> {
    await new Promise(resolve => setTimeout(resolve, 50));
    const allTrainings = JSON.parse(JSON.stringify(placeholderTrainings)).map((t: any) => ({ ...t, date: new Date(t.date) }));
    return allTrainings.filter(t => t.registrationType === 'closed' && t.pendingRegistrations?.includes(userId));
}


// Simulate getting the current user
export async function getCurrentUser(): Promise<User | null> {
    await new Promise(resolve => setTimeout(resolve, 20));
    const userIndex = 1; // 0: Rider, 1: Trainer, 2: Guest
    const user = userIndex < placeholderUsers.length ? placeholderUsers[userIndex] : null;
    // console.log("Current User:", user); // Keep this commented unless debugging
    return user;
}

// --- Action Functions ---

// Register user for training (handles both open and closed types)
export async function registerForTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string; pending?: boolean }> {
    console.log(`Simulating registration: User ${userId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 150));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) {
        return { success: false, message: 'Training not found.' };
    }
    const training = placeholderTrainings[trainingIndex];

    // Check if already registered, pending, rejected, or cancelled
    if (training.registeredRiders?.includes(userId)) {
        return { success: false, message: 'Already registered for this training.' };
    }
    if (training.pendingRegistrations?.includes(userId)) {
        return { success: false, message: 'Registration already pending approval.' };
    }
    if (training.rejectedRegistrations?.some(r => r.userId === userId)) {
        return { success: false, message: 'Your registration for this training was previously rejected.' };
    }
     if (training.registrationType === 'open' && training.cancelledRegistrations?.some(c => c.userId === userId)) {
        return { success: false, message: 'Your registration for this training was previously cancelled by the trainer.' };
    }


    // Check capacity only for open registrations (closed handled during approval)
    if (training.registrationType === 'open' && training.maxRiders && (training.registeredRiders?.length ?? 0) >= training.maxRiders) {
        return { success: false, message: 'Training is full.' };
    }

    if (training.registrationType === 'open') {
        training.registeredRiders = [...(training.registeredRiders || []), userId];
        console.log("Updated Registrations (Open):", training.registeredRiders);
        return { success: true, message: 'Successfully registered!' };
    } else { // 'closed'
        training.pendingRegistrations = [...(training.pendingRegistrations || []), userId];
        console.log("Updated Pending Registrations (Closed):", training.pendingRegistrations);
        return { success: true, message: 'Registration submitted for approval.', pending: true };
    }
}


// Unregister user from training (handles open, closed approved, and closed pending)
export async function unregisterFromTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string }> {
  console.log(`Simulating unregistration: User ${userId} from Training ${trainingId}`);
  await new Promise(resolve => setTimeout(resolve, 150));

  const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
  if (trainingIndex === -1) {
    return { success: false, message: 'Training not found.' };
  }
  const training = placeholderTrainings[trainingIndex];

  let wasRegistered = false;
  let wasPending = false;

  // Remove from registered list (if applicable)
  if (training.registeredRiders?.includes(userId)) {
    training.registeredRiders = training.registeredRiders.filter(id => id !== userId);
    wasRegistered = true;
  }

  // Remove from pending list (if applicable)
  if (training.pendingRegistrations?.includes(userId)) {
    training.pendingRegistrations = training.pendingRegistrations.filter(id => id !== userId);
    wasPending = true;
  }

   // Remove from cancelled list for open trainings (allows re-registering)
    if (training.registrationType === 'open' && training.cancelledRegistrations?.some(c => c.userId === userId)) {
        training.cancelledRegistrations = training.cancelledRegistrations.filter(c => c.userId !== userId);
         // Note: We don't set wasRegistered/wasPending here, as they were cancelled, not actively registered/pending
    }


  if (!wasRegistered && !wasPending) {
    return { success: false, message: 'You were not registered or pending for this training.' };
  }

  console.log("Updated Registrations/Pending after unregister:", training.registeredRiders, training.pendingRegistrations);
  return { success: true, message: wasPending ? 'Registration request withdrawn.' : 'Successfully unregistered.' };
}


// Approve a pending registration (Trainer action)
export async function approveRegistration(trainerId: string, trainingId: string, riderId: string): Promise<{ success: boolean; message: string }> {
    console.log(`Trainer ${trainerId} approving Rider ${riderId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

    if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };
    if (training.registrationType !== 'closed') return { success: false, message: 'This training does not require approval.' };
    if (!training.pendingRegistrations?.includes(riderId)) return { success: false, message: 'Rider not found in pending list.' };

    // Check capacity before approving
     if (training.maxRiders && (training.registeredRiders?.length ?? 0) >= training.maxRiders) {
        return { success: false, message: 'Training is full. Cannot approve more riders.' };
    }

    // Move from pending to registered
    training.pendingRegistrations = training.pendingRegistrations.filter(id => id !== riderId);
    training.registeredRiders = [...(training.registeredRiders || []), riderId];

    // Remove from rejected list if they were previously rejected and re-applied
    training.rejectedRegistrations = training.rejectedRegistrations?.filter(r => r.userId !== riderId);

    console.log("Approved:", riderId, "Remaining pending:", training.pendingRegistrations, "Registered:", training.registeredRiders);
    return { success: true, message: 'Registration approved.' };
}

// Reject a pending registration (Trainer action)
export async function rejectRegistration(trainerId: string, trainingId: string, riderId: string, reason?: string): Promise<{ success: boolean; message: string }> {
    console.log(`Trainer ${trainerId} rejecting Rider ${riderId} for Training ${trainingId} (Reason: ${reason})`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

     if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };
    if (training.registrationType !== 'closed') return { success: false, message: 'This training does not require approval.' };
     if (!training.pendingRegistrations?.includes(riderId)) return { success: false, message: 'Rider not found in pending list.' };

    // Remove from pending and add to rejected
    training.pendingRegistrations = training.pendingRegistrations.filter(id => id !== riderId);
    const rejection: RegistrationStatusReason = { userId: riderId, reason };
    training.rejectedRegistrations = [...(training.rejectedRegistrations || []), rejection];

    console.log("Rejected:", riderId, "Remaining pending:", training.pendingRegistrations, "Rejected:", training.rejectedRegistrations);
    return { success: true, message: 'Registration rejected.' };
}

// Cancel an existing registration for an 'open' training (Trainer action)
export async function cancelOpenRegistration(trainerId: string, trainingId: string, riderId: string, reason?: string): Promise<{ success: boolean; message: string }> {
    console.log(`Trainer ${trainerId} cancelling Rider ${riderId}'s registration for Open Training ${trainingId} (Reason: ${reason})`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

     if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };
    if (training.registrationType !== 'open') return { success: false, message: 'Cannot cancel registration for a closed training. Use reject/approve.' };
     if (!training.registeredRiders?.includes(riderId)) return { success: false, message: 'Rider not found in registered list.' };

    // Remove from registered and add to cancelled
    training.registeredRiders = training.registeredRiders.filter(id => id !== riderId);
     const cancellation: RegistrationStatusReason = { userId: riderId, reason };
    training.cancelledRegistrations = [...(training.cancelledRegistrations || []), cancellation];

    console.log("Cancelled:", riderId, "Remaining registered:", training.registeredRiders, "Cancelled:", training.cancelledRegistrations);
    return { success: true, message: 'Registration cancelled.' };
}


// Update CreateTrainingData type used in createTraining function
export type { CreateTrainingData };

// Create a new training session
export async function createTraining(trainerId: string, data: CreateTrainingData): Promise<{ success: boolean; message: string; trainingId?: string }> {
    console.log(`Simulating training creation by Trainer ${trainerId} with data:`, data);
    await new Promise(resolve => setTimeout(resolve, 200));

    const trainer = placeholderUsers.find(u => u.id === trainerId && u.role === 'trainer');
    if (!trainer) {
        return { success: false, message: 'Invalid trainer.' };
    }

    const location = placeholderLocations.find(l => l.id === data.locationId);
    if (!location) {
        return { success: false, message: 'Invalid location selected.' };
    }

     if (!data.skillLevels || data.skillLevels.length === 0) {
        return { success: false, message: 'At least one skill level must be selected.' };
    }
     if (!data.motorcycleTypes || data.motorcycleTypes.length === 0) {
        return { success: false, message: 'At least one motorcycle type must be selected.' };
    }
     if (!data.registrationType) { // Ensure registration type is provided
        return { success: false, message: 'Registration type must be selected.' };
    }

    const newTraining: TrainingSession = {
        ...data,
        id: `ts-${Date.now()}`, // Simple unique ID
        trainerId: trainer.id,
        trainerName: trainer.name,
        locationName: location.name, // Add the location name
        date: new Date(data.date), // Ensure date is Date object
        // Initialize new fields
        registeredRiders: [],
        pendingRegistrations: [],
        rejectedRegistrations: [],
        cancelledRegistrations: [],
        // registrationType is already in 'data'
    };

    placeholderTrainings.push(newTraining);
    console.log('New Training Added:', newTraining);

    return { success: true, message: 'Training created successfully!', trainingId: newTraining.id };
}
