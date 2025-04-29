import type { TrainingSession, User } from './types';

export const placeholderUsers: User[] = [
  { id: 'user-1', name: 'Alice Rider', email: 'alice@example.com', role: 'rider' },
  { id: 'user-2', name: 'Bob Trainer', email: 'bob@example.com', role: 'trainer' },
  { id: 'user-3', name: 'Charlie Rider', email: 'charlie@example.com', role: 'rider' },
];

export const placeholderTrainings: TrainingSession[] = [
  {
    id: 'ts-1',
    trainerId: 'user-2',
    trainerName: 'Bob Trainer',
    title: 'Enduro Basics Clinic',
    date: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000), // 2 days from now
    location: 'Rocky Valley Trails',
    skillLevel: 'Beginner',
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
    location: 'MX Speed Park',
    skillLevel: 'Intermediate',
    description: 'Advanced cornering drills, ruts, and berms. Improve your lap times!',
    registeredRiders: [],
    maxRiders: 8,
  },
  {
    id: 'ts-3',
    trainerId: 'user-2', // Example with a different trainer if needed
    trainerName: 'Bob Trainer',
    title: 'Advanced Hill Climb Techniques',
    date: new Date(Date.now() + 10 * 24 * 60 * 60 * 1000), // 10 days from now
    location: 'Steep Mountain Pass',
    skillLevel: 'Advanced',
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
    location: 'MX Speed Park - Training Area',
    skillLevel: 'Beginner',
    description: 'Safely learn the basics of jumping small tabletops and rollers.',
    registeredRiders: ['user-3'],
    maxRiders: 12,
  },
];

// Simple function to simulate fetching data
export async function getTrainings(): Promise<TrainingSession[]> {
  // In a real app, this would fetch from a database
  await new Promise(resolve => setTimeout(resolve, 50)); // Simulate network delay
  return placeholderTrainings;
}

export async function getTrainingById(id: string): Promise<TrainingSession | undefined> {
  await new Promise(resolve => setTimeout(resolve, 50));
  return placeholderTrainings.find(t => t.id === id);
}

export async function getMyRegisteredTrainings(userId: string): Promise<TrainingSession[]> {
    await new Promise(resolve => setTimeout(resolve, 50));
    return placeholderTrainings.filter(t => t.registeredRiders?.includes(userId));
}

// Simulate getting the current user - replace with actual auth logic
export async function getCurrentUser(): Promise<User | null> {
    await new Promise(resolve => setTimeout(resolve, 20));
    // Cycle between rider, trainer, and guest for testing different views
    const roles: User['role'][] = ['rider', 'trainer', 'guest'];
    const randomIndex = Math.floor(Math.random() * 3); // Change this to 0 or 1 to fix user
    const role = roles[randomIndex];

    if (role === 'rider') return placeholderUsers[0];
    if (role === 'trainer') return placeholderUsers[1];
    return null; // Guest
}

// Simulate registration action
export async function registerForTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string }> {
    console.log(`Simulating registration: User ${userId} for Training ${trainingId}`);
    await new Promise(resolve => setTimeout(resolve, 150)); // Simulate API call

    const training = placeholderTrainings.find(t => t.id === trainingId);
    if (!training) {
        return { success: false, message: 'Training not found.' };
    }

    if (training.registeredRiders?.includes(userId)) {
        return { success: false, message: 'Already registered for this training.' };
    }

     if (training.registeredRiders && training.maxRiders && training.registeredRiders.length >= training.maxRiders) {
        return { success: false, message: 'Training is full.' };
    }

    // In a real app, update the database here
    training.registeredRiders = [...(training.registeredRiders || []), userId];

    return { success: true, message: 'Successfully registered!' };
}

// Simulate training creation action
export async function createTraining(trainerId: string, data: Omit<TrainingSession, 'id' | 'trainerId' | 'trainerName' | 'registeredRiders'>): Promise<{ success: boolean; message: string; trainingId?: string }> {
    console.log(`Simulating training creation by Trainer ${trainerId}`);
    await new Promise(resolve => setTimeout(resolve, 200)); // Simulate API call

    const trainer = placeholderUsers.find(u => u.id === trainerId && u.role === 'trainer');
    if (!trainer) {
        return { success: false, message: 'Invalid trainer.' };
    }

    const newTraining: TrainingSession = {
        ...data,
        id: `ts-${Date.now()}`, // Simple unique ID generation
        trainerId: trainer.id,
        trainerName: trainer.name,
        registeredRiders: [],
    };

    // In a real app, save to the database here
    placeholderTrainings.push(newTraining);
    console.log('New Training Added:', newTraining);


    return { success: true, message: 'Training created successfully!', trainingId: newTraining.id };
}


export async function unregisterFromTraining(userId: string, trainingId: string): Promise<{ success: boolean; message: string }> {
  console.log(`Simulating unregistration: User ${userId} from Training ${trainingId}`);
  await new Promise(resolve => setTimeout(resolve, 150)); // Simulate API call

  const trainingIndex = placeholderTrainings.findIndex(t => t.id === trainingId);
  if (trainingIndex === -1) {
    return { success: false, message: 'Training not found.' };
  }

  const training = placeholderTrainings[trainingIndex];
  if (!training.registeredRiders?.includes(userId)) {
    return { success: false, message: 'Not registered for this training.' };
  }

  // In a real app, update the database here
  training.registeredRiders = training.registeredRiders.filter(id => id !== userId);

  return { success: true, message: 'Successfully unregistered.' };
}
