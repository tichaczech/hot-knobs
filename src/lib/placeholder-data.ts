import type { TrainingSession, User, SkillLevel, MotorcycleType, Location, RegistrationType, CreateTrainingData, Registration, RegistrationStatus, RegistrationResult } from './types'; // Updated imports

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
  { id: 'user-1', name: 'Bob Trainer', email: 'bob@example.com', role: 'trainer' },
  { id: 'user-2', name: 'Alice Rider', email: 'alice@example.com', role: 'rider' },
  { id: 'user-3', name: 'Charlie Rider', email: 'charlie@example.com', role: 'rider' },
  { id: 'user-4', name: 'Diana Rider', email: 'diana@example.com', role: 'rider' }, // Requested closed training
  { id: 'user-5', name: 'Eve Rider', email: 'eve@example.com', role: 'rider' }, // Will be on waiting list
  { id: 'user-6', name: 'Frank Rider', email: 'frank@example.com', role: 'rider' }, // Will be on waiting list
];

// --- Placeholder Trainings ---
// Updated to use the new 'registrations' structure
export let placeholderTrainings: TrainingSession[] = [ // Use let to allow modification
  {
    id: 'ts-1',
    trainerId: 'user-1',
    trainerName: 'Bob Trainer',
    title: 'Enduro Basics Clinic',
    date: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000), // 2 days from now
    locationId: 'loc-1',
    locationName: 'Rocky Valley Trails',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Other'],
    description: 'Focus on fundamental enduro techniques: body positioning, braking, and small obstacles.',
    registrationType: 'open',
    registrations: [
        { userId: 'user-4', status: 'Confirmed', registeredAt: new Date(Date.now() - 1 * 24 * 60 * 60 * 1000) }, // Diana registered yesterday
        { userId: 'user-3', status: 'Cancelled', registeredAt: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000), reason: "Rider cancelled." }, // Charlie cancelled
    ],
    maxRiders: 1, // Set low capacity to test waiting list easily
  },
  {
    id: 'ts-2',
    trainerId: 'user-1',
    trainerName: 'Bob Trainer',
    title: 'Motocross Cornering Masterclass',
    date: new Date(Date.now() + 5 * 24 * 60 * 60 * 1000), // 5 days from now
    locationId: 'loc-2',
    locationName: 'MX Speed Park',
    skillLevels: ['Intermediate', 'Advanced'],
    motorcycleTypes: ['250cc', '450cc'],
    description: 'Advanced cornering drills, ruts, and berms. Improve your lap times!',
    registrationType: 'closed',
    registrations: [
        { userId: 'user-3', status: 'Confirmed', registeredAt: new Date(Date.now() - 1 * 24 * 60 * 60 * 1000) }, // Charlie is confirmed
        { userId: 'user-4', status: 'Created', registeredAt: new Date(Date.now() - 12 * 60 * 60 * 1000) }, // Diana requested 12 hours ago
        { userId: 'user-2', status: 'Rejected', registeredAt: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000), reason: 'Skill level mismatch' }, // Alice was rejected
    ],
    maxRiders: 8,
  },
  {
    id: 'ts-3',
    trainerId: 'user-1',
    trainerName: 'Bob Trainer',
    title: 'Advanced Hill Climb Techniques',
    date: new Date(Date.now() + 10 * 24 * 60 * 60 * 1000), // 10 days from now
    locationId: 'loc-3',
    locationName: 'Steep Mountain Pass',
    skillLevels: ['Advanced', 'Pro'],
    motorcycleTypes: ['250cc', '450cc', 'Other'],
    description: 'Learn techniques for tackling challenging ascents, line selection, and throttle control.',
    registrationType: 'open',
    registrations: [
        { userId: 'user-2', status: 'Confirmed', registeredAt: new Date() },
        { userId: 'user-3', status: 'Confirmed', registeredAt: new Date() },
    ],
    maxRiders: 6,
  },
  {
    id: 'ts-4',
    trainerId: 'user-1',
    trainerName: 'Bob Trainer',
    title: 'Introduction to Motocross Jumps',
    date: new Date(Date.now() + 15 * 24 * 60 * 60 * 1000), // 15 days from now
    locationId: 'loc-2',
    locationName: 'MX Speed Park',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Electric'],
    description: 'Safely learn the basics of jumping small tabletops and rollers.',
    registrationType: 'open',
    registrations: [
        { userId: 'user-3', status: 'Confirmed', registeredAt: new Date() },
    ],
    maxRiders: 12,
  },
];


// --- Helper Functions for Registration Status ---
const isConfirmed = (r: Registration) => r.status === 'Confirmed';
const isCreated = (r: Registration) => r.status === 'Created';
const isWaiting = (r: Registration) => r.status === 'Waiting';
const isRejected = (r: Registration) => r.status === 'Rejected';
const isCancelled = (r: Registration) => r.status === 'Cancelled';
const isActiveRegistration = (r: Registration) => isConfirmed(r) || isCreated(r) || isWaiting(r); // User has an active interest

// Helper to get registration counts
export function getRegistrationCounts(training: TrainingSession | undefined) {
    if (!training) return { confirmed: 0, waiting: 0, created: 0 };
    return {
        confirmed: training.registrations.filter(isConfirmed).length,
        waiting: training.registrations.filter(isWaiting).length,
        created: training.registrations.filter(isCreated).length,
    };
}

// Helper to check if training is full
export function isTrainingFull(training: TrainingSession | undefined): boolean {
    if (!training?.maxRiders) return false; // No limit
    const confirmedCount = getRegistrationCounts(training).confirmed;
    return confirmedCount >= training.maxRiders;
}

// Helper to find the first user on the waiting list
function getFirstWaitingUser(training: TrainingSession): Registration | undefined {
    return training.registrations
        .filter(isWaiting)
        .sort((a, b) => a.registeredAt.getTime() - b.registeredAt.getTime()) // Oldest waiting first
        .shift();
}

// --- Data Fetching Functions ---

// Fetch all available locations
export async function getLocations(): Promise<Location[]> {
    await new Promise(resolve => setTimeout(resolve, 40));
    return JSON.parse(JSON.stringify(placeholderLocations));
}

// Fetch location by ID
export async function getLocationById(id: string): Promise<Location | undefined> {
    await new Promise(resolve => setTimeout(resolve, 30));
    const location = placeholderLocations.find(loc => loc.id === id);
    return location ? JSON.parse(JSON.stringify(location)) : undefined;
}

// Fetch all available skill levels
export async function getSkillLevels(): Promise<SkillLevel[]> {
    await new Promise(resolve => setTimeout(resolve, 25));
    return JSON.parse(JSON.stringify(placeholderSkillLevels));
}

// Fetch all available motorcycle types
export async function getMotorcycleTypes(): Promise<MotorcycleType[]> {
    await new Promise(resolve => setTimeout(resolve, 25));
    return JSON.parse(JSON.stringify(placeholderMotorcycleTypes));
}


// Fetch all trainings
export async function getTrainings(): Promise<TrainingSession[]> {
  await new Promise(resolve => setTimeout(resolve, 50));
  // Deep copy and ensure dates are Date objects
  return JSON.parse(JSON.stringify(placeholderTrainings)).map((t: any) => ({
      ...t,
      date: new Date(t.date),
      registrations: t.registrations.map((r: any) => ({...r, registeredAt: new Date(r.registeredAt)}))
  }));
}

// Fetch training by ID
export async function getTrainingById(id: string): Promise<TrainingSession | undefined> {
  await new Promise(resolve => setTimeout(resolve, 50));
   const training = placeholderTrainings.find(t => t.id === id);
   // Ensure date is a Date object and return deep copy
   return training ? {
       ...JSON.parse(JSON.stringify(training)),
       date: new Date(training.date),
       registrations: training.registrations.map(r => ({...r, registeredAt: new Date(r.registeredAt)}))
    } : undefined;
}

// Fetch trainings based on user's registration status
export async function getMyTrainingsByStatus(userId: string, statuses: RegistrationStatus[]): Promise<TrainingSession[]> {
    await new Promise(resolve => setTimeout(resolve, 50));
    const allTrainings = await getTrainings(); // Use the deep copy function
    return allTrainings.filter(t =>
        t.registrations.some(r => r.userId === userId && statuses.includes(r.status))
    );
}

// Fetch trainings a user is CONFIRMED for
export async function getMyRegisteredTrainings(userId: string): Promise<TrainingSession[]> {
    return getMyTrainingsByStatus(userId, ['Confirmed']);
}

// Fetch trainings a user has PENDING registration for (Created status for closed trainings)
export async function getMyPendingTrainings(userId: string): Promise<TrainingSession[]> {
     return getMyTrainingsByStatus(userId, ['Created']);
}

// Fetch trainings a user is on the WAITING list for
export async function getMyWaitingListTrainings(userId: string): Promise<TrainingSession[]> {
     return getMyTrainingsByStatus(userId, ['Waiting']);
}

// Simulate getting the current user
export async function getCurrentUser(): Promise<User | null> {
    await new Promise(resolve => setTimeout(resolve, 20));
    const userIndex = 1; // 0: Bob (Trainer), 1: Alice (Rider), 2: Charlie (Rider), ...
    const user = userIndex < placeholderUsers.length ? placeholderUsers[userIndex] : null;
    // console.log("Current User:", user); // Keep this commented unless debugging
    return user ? JSON.parse(JSON.stringify(user)) : null;
}

// --- Action Functions ---

// Register user for training (handles open, closed, and waiting list)
export async function registerForTraining(userId: string, trainingId: string): Promise<RegistrationResult> {
    console.log(`Simulating registration: User ${userId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 150));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) {
        return { success: false, message: 'Training not found.' };
    }
    const training = placeholderTrainings[trainingIndex];
    const existingRegistration = training.registrations.find(r => r.userId === userId);

    // Prevent re-registration if already Cancelled or Rejected
    if (existingRegistration && (isRejected(existingRegistration) || isCancelled(existingRegistration))) {
        return { success: false, message: `Cannot re-register after being ${existingRegistration.status.toLowerCase()}.` };
    }

    // Check if user has an active registration already (Created, Confirmed, Waiting)
    if (existingRegistration && isActiveRegistration(existingRegistration)) {
        return { success: false, message: `Already ${existingRegistration.status.toLowerCase()} for this training.` };
    }

    const isFull = isTrainingFull(training);
    let newStatus: RegistrationStatus;
    let message: string;

    if (training.registrationType === 'open') {
        if (isFull) {
            newStatus = 'Waiting';
            message = 'Training is full. Added to waiting list.';
        } else {
            newStatus = 'Confirmed';
            message = 'Successfully registered!';
        }
    } else { // 'closed'
        // Capacity check happens during approval for closed trainings, users always start as 'Created'
        newStatus = 'Created';
        message = 'Registration submitted for approval.';
    }

    // Update existing registration or add new one
    if (existingRegistration) {
        // This case should ideally not happen due to the checks above, but handles it defensively
        existingRegistration.status = newStatus;
        existingRegistration.registeredAt = new Date();
        existingRegistration.reason = undefined;
    } else {
        const newRegistration: Registration = {
            userId: userId,
            status: newStatus,
            registeredAt: new Date(),
        };
        training.registrations.push(newRegistration);
    }

    console.log(`User ${userId} status for training ${trainingId} set to ${newStatus}. Full: ${isFull}`);
    console.log("Updated Registrations:", training.registrations);
    return { success: true, message: message, status: newStatus };
}


// Cancel user's own registration (Rider action)
export async function cancelMyRegistration(userId: string, trainingId: string): Promise<RegistrationResult> {
    console.log(`Rider ${userId} cancelling registration for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 150));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];
    const registrationIndex = training.registrations.findIndex(r => r.userId === userId);

    if (registrationIndex === -1) return { success: false, message: 'Registration not found.' };

    const registration = training.registrations[registrationIndex];

    if (!isActiveRegistration(registration)) {
         return { success: false, message: `Cannot cancel a registration with status: ${registration.status}` };
    }

    const previousStatus = registration.status;
    registration.status = 'Cancelled';
    registration.reason = 'Rider cancelled.'; // Indicate rider initiated cancellation

    console.log(`Rider ${userId} cancelled. Previous status: ${previousStatus}.`);

    // Check if we need to promote someone from the waiting list
    if (previousStatus === 'Confirmed') {
        promoteWaitingUser(training); // Attempt to promote after cancellation
    }

    console.log("Updated Registrations after rider cancellation:", training.registrations);
    return { success: true, message: 'Registration successfully cancelled.', status: 'Cancelled' };
}


// --- Trainer Actions ---

// Helper function to promote a waiting user if a spot opens up
function promoteWaitingUser(training: TrainingSession): boolean {
    if (isTrainingFull(training)) {
        return false; // Still full, cannot promote
    }

    const waitingUser = getFirstWaitingUser(training);
    if (!waitingUser) {
        return false; // No one is waiting
    }

    if (training.registrationType === 'open') {
        // Directly confirm the waiting user for open trainings
        waitingUser.status = 'Confirmed';
        console.log(`Promoted waiting user ${waitingUser.userId} to Confirmed (Open Training).`);
    } else {
        // Change status to 'Created' for closed trainings, requiring trainer approval
        waitingUser.status = 'Created';
        console.log(`Promoted waiting user ${waitingUser.userId} to Created (Closed Training - Requires Approval).`);
        // TODO: Notify trainer?
    }
    return true;
}


// Approve a 'Created' or 'Waiting' registration (Trainer action)
export async function approveRegistration(trainerId: string, trainingId: string, riderId: string): Promise<RegistrationResult> {
    console.log(`Trainer ${trainerId} approving Rider ${riderId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

    if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };

    const registration = training.registrations.find(r => r.userId === riderId);
    if (!registration) return { success: false, message: 'Registration not found for this user.' };

    if (!isCreated(registration) && !isWaiting(registration)) {
        return { success: false, message: `Cannot approve registration with status: ${registration.status}` };
    }

     // Check capacity before approving
    if (isTrainingFull(training)) {
        // If approving someone from 'Created' state and it's full, move them to 'Waiting' instead.
        if (isCreated(registration)) {
            registration.status = 'Waiting';
             console.log(`Training full. Moved user ${riderId} from Created to Waiting.`);
             return { success: true, message: 'Training is full. User moved to waiting list.', status: 'Waiting' };
        }
        // If trying to approve someone already 'Waiting' but it's still full.
        return { success: false, message: 'Training is full. Cannot approve from waiting list yet.' };
    }

    // Approve the registration
    registration.status = 'Confirmed';
    registration.reason = undefined; // Clear any previous reason

    console.log("Approved:", riderId, "Status:", registration.status);
    console.log("Updated registrations:", training.registrations);
    return { success: true, message: 'Registration approved.', status: 'Confirmed' };
}


// Reject a 'Created' or 'Waiting' registration (Trainer action)
export async function rejectRegistration(trainerId: string, trainingId: string, riderId: string, reason?: string): Promise<RegistrationResult> {
    console.log(`Trainer ${trainerId} rejecting Rider ${riderId} for Training ${trainingId} (Reason: ${reason})`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

    if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };

    const registration = training.registrations.find(r => r.userId === riderId);
    if (!registration) return { success: false, message: 'Registration not found for this user.' };

    if (!isCreated(registration) && !isWaiting(registration)) {
       return { success: false, message: `Cannot reject registration with status: ${registration.status}` };
    }

    // Reject the registration
    registration.status = 'Rejected';
    registration.reason = reason || 'Rejected by trainer.';

    console.log("Rejected:", riderId, "Reason:", registration.reason);
    console.log("Updated registrations:", training.registrations);
    return { success: true, message: 'Registration rejected.', status: 'Rejected' };
}

// Cancel a 'Confirmed' registration (Trainer action - e.g., rider no-show, policy violation)
export async function cancelConfirmedRegistration(trainerId: string, trainingId: string, riderId: string, reason?: string): Promise<RegistrationResult> {
    console.log(`Trainer ${trainerId} cancelling CONFIRMED registration for Rider ${riderId}, Training ${trainingId} (Reason: ${reason})`);
    await new Promise(resolve => setTimeout(resolve, 100));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
    if (trainingIndex === -1) return { success: false, message: 'Training not found.' };
    const training = placeholderTrainings[trainingIndex];

    if (training.trainerId !== trainerId) return { success: false, message: 'Unauthorized action.' };

    const registration = training.registrations.find(r => r.userId === riderId);
    if (!registration) return { success: false, message: 'Registration not found for this user.' };

    if (!isConfirmed(registration)) {
       return { success: false, message: `Cannot cancel registration with status: ${registration.status}. Use Reject for Created/Waiting.` };
    }

    // Cancel the registration
    registration.status = 'Cancelled';
    registration.reason = reason || 'Cancelled by trainer.';

    console.log("Trainer Cancelled:", riderId, "Reason:", registration.reason);

    // Try to promote someone from waiting list
    promoteWaitingUser(training);

    console.log("Updated registrations after trainer cancellation:", training.registrations);
    return { success: true, message: 'Registration cancelled by trainer.', status: 'Cancelled' };
}


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
     if (!data.registrationType) {
        return { success: false, message: 'Registration type must be selected.' };
    }

    const newTraining: TrainingSession = {
        id: `ts-${Date.now()}`, // Simple unique ID
        trainerId: trainer.id,
        trainerName: trainer.name,
        title: data.title,
        date: new Date(data.date), // Ensure date is Date object
        locationId: data.locationId,
        locationName: location.name, // Add the location name
        skillLevels: data.skillLevels,
        motorcycleTypes: data.motorcycleTypes,
        description: data.description,
        registrationType: data.registrationType,
        maxRiders: data.maxRiders,
        registrations: [], // Initialize with empty registrations
    };

    placeholderTrainings.push(newTraining);
    console.log('New Training Added:', newTraining);

    return { success: true, message: 'Training created successfully!', trainingId: newTraining.id };
}


// Helper to get a user's specific registration for a training
export function getUserRegistration(training: TrainingSession | undefined, userId: string | undefined): Registration | undefined {
    if (!training || !userId) return undefined;
    return training.registrations.find(r => r.userId === userId);
}
