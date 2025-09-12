import 'package:dart_mappable/dart_mappable.dart';

import 'event.dart';

part 'training_session.mapper.dart';

// export interface SkillLevel {
//     id: string;
//     name: string;
//     organizerId: string; // ID of the Organizer who organizes the event
// }

// export interface Category {
//     id: string;
//     name: string;
//     organizerId: string; // ID of the Organizer who organizes the event
// }

@MappableClass(discriminatorValue: 'event')
class TrainingSession extends Event with TrainingSessionMappable {
  final List<String> categories; // IDs of Categories associated with the session
  final String leadTrainer; // ID of the Trainer who will lead the session
  final List<String> trainers; // IDs of additional Trainers assisting in the session
  final List<String> skillLevels; // IDs of Skill Levels required for the session

  TrainingSession({
    required super.id,
    required super.active,
    required super.cachedAt,
    required super.createdAt,
    required super.createdBy,
    required super.etag,
    required super.updatedAt,
    required super.updatedBy,
    super.description,
    super.maxParticipants,
    required super.registrationType,
    required super.siteId,
    required super.startDate,
    required super.title,
    required super.visibility,
    required this.categories,
    required this.leadTrainer,
    required this.skillLevels,
    this.trainers = const [],
  }) : super() {
    print('TrainingSession created: $id');
  }
}

// export interface Participant {
//     id: string;
//     name: string;
//     organizers: {
//         id: string;
//         data: any; // Placeholder for organizer data
//     }
//     // Could add more fields like website, contact, etc.
// }

// // Represents a user's registration for a specific training session
// export interface Registration {
//     id: string;
//     eventId: string;
//     participantId: string;
//     status: RegistrationStatus;
//     registeredAt: Date; // Timestamp when the registration/request was made
//     registeredBy: string; // ID of the User who made the registration/request
//     reason?: string; // Optional reason for Rejection or Cancellation (by trainer or rider)
//     // Timestamps for status changes could be added if needed (e.g., confirmedAt, rejectedAt)
// }
