import 'package:dart_mappable/dart_mappable.dart';

import 'entity.dart';

part 'event.mapper.dart';

enum EventVisibility { public, followersOnly, invitedOnly }

enum RegistrationType { open, approvalRequired, invitedOnly }

@MappableClass(discriminatorValue: 'event')
class Event extends Entity with EventMappable {
  final String? description;
  final int? maxParticipants;
  // final String? operatorId; // ID of the Organizer who organizes the event
  final RegistrationType registrationType;
  final String siteId; // ID of the Site where the event takes place
  final DateTime startDate;
  final String title;
  final EventVisibility visibility;

  Event({
    required super.id,
    required super.active,
    required super.cachedAt,
    required super.createdAt,
    required super.createdBy,
    required super.etag,
    required super.updatedAt,
    required super.updatedBy,
    this.description,
    this.maxParticipants,
    // this.operatorId, --- IGNORE ---
    required this.registrationType,
    required this.siteId,
    required this.startDate,
    required this.title,
    required this.visibility,
  }) : super() {
    print('Event created: $id');
  }
}
