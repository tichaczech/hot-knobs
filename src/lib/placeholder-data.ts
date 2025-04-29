import type { TrainingSession, User, SkillLevel, MotorcycleType, Location } from './types';

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
];

// --- Placeholder Trainings ---
// Updated to use locationId and locationName
export const placeholderTrainings: TrainingSession[] = [
  {
    id: 'ts-1',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Enduro Basics Clinic',
    date: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000), // 2 days from now
    locationId: 'loc-1',
    locationName: 'Rocky Valley Trails',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Other'],
    description: 'Focus on fundamental enduro techniques: body positioning, braking, and small obstacles.',
    registeredRiders: ['user-1'],
    maxRiders: 10,
  },
  {
    id: 'ts-2',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Motocross Cornering Masterclass',
    date: new Date(Date.now() + 5 * 24 * 60 * 60 * 1000), // 5 days from now
    locationId: 'loc-2',
    locationName: 'MX Speed Park',
    skillLevels: ['Intermediate', 'Advanced'],
    motorcycleTypes: ['250cc', '450cc'],
    description: 'Advanced cornering drills, ruts, and berms. Improve your lap times!',
    registeredRiders: [],
    maxRiders: 8,
  },
  {
    id: 'ts-3',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Advanced Hill Climb Techniques',
    date: new Date(Date.now() + 10 * 24 * 60 * 60 * 1000), // 10 days from now
    locationId: 'loc-3',
    locationName: 'Steep Mountain Pass',
    skillLevels: ['Advanced', 'Pro'],
    motorcycleTypes: ['250cc', '450cc', 'Other'],
    description: 'Learn techniques for tackling challenging ascents, line selection, and throttle control.',
    registeredRiders: ['user-1', 'user-3'],
    maxRiders: 6,
  },
    {
    id: 'ts-4',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Introduction to Motocross Jumps',
    date: new Date(Date.now() + 15 * 24 * 60 * 60 * 1000), // 15 days from now
    locationId: 'loc-2', // Same location as ts-2
    locationName: 'MX Speed Park',
    skillLevels: ['Beginner'],
    motorcycleTypes: ['125cc', '250cc', 'Electric'],
    description: 'Safely learn the basics of jumping small tabletops and rollers.',
    registeredRiders: ['user-3'],
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

// Fetch trainings a user is registered for
export async function getMyRegisteredTrainings(userId: string): Promise<TrainingSession[]> {
    await new Promise(resolve => setTimeout(resolve, 50));
    const allTrainings = JSON.parse(JSON.stringify(placeholderTrainings)).map((t: any) => ({ ...t, date: new Date(t.date) }));
    return allTrainings.filter(t => t.registeredRiders?.includes(userId));
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

// Register user for training
export async function registerForTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string }> {
    console.log(`Simulating registration: User ${userId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 150));

    const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
     if (trainingIndex === -1) {
        return { success: false, message: 'Training not found.' };
    }
    const training = placeholderTrainings[trainingIndex];

    if (training.registeredRiders?.includes(userId)) {
        return { success: false, message: 'Already registered for this training.' };
    }

     if (training.maxRiders && (training.registeredRiders?.length ?? 0) >= training.maxRiders) {
        return { success: false, message: 'Training is full.' };
    }

    training.registeredRiders = [...(training.registeredRiders || []), userId];
    console.log("Updated Registrations:", training.registeredRiders);

    return { success: true, message: 'Successfully registered!' };
}

// Unregister user from training
export async function unregisterFromTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string }> {
  console.log(`Simulating unregistration: User ${userId} from Training ${trainingId}`);
  await new Promise(resolve => setTimeout(resolve, 150));

  const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
  if (trainingIndex === -1) {
    return { success: false, message: 'Training not found.' };
  }

  const training = placeholderTrainings[trainingIndex];
  if (!training.registeredRiders?.includes(userId)) {
    return { success: false, message: 'Not registered for this training.' };
  }

  training.registeredRiders = training.registeredRiders.filter(id => id !== userId);
  console.log("Updated Registrations after unregister:", training.registeredRiders);

  return { success: true, message: 'Successfully unregistered.' };
}

// Type for data passed to createTraining (omitting fields generated by the backend)
// Ensure locationId is part of the data expected by createTraining
type CreateTrainingData = Omit<TrainingSession, 'id' | 'trainerId' | 'trainerName' | 'registeredRiders' | 'date' | 'locationName'> & { date: Date };

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

    const newTraining: TrainingSession = {
        ...data,
        id: `ts-${Date.now()}`, // Simple unique ID
        trainerId: trainer.id,
        trainerName: trainer.name,
        locationName: location.name, // Add the location name
        registeredRiders: [],
        date: new Date(data.date), // Ensure date is Date object
    };

    placeholderTrainings.push(newTraining);
    console.log('New Training Added:', newTraining);

    return { success: true, message: 'Training created successfully!', trainingId: newTraining.id };
}
