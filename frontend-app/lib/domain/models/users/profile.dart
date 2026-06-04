import 'package:dart_mappable/dart_mappable.dart';

import '../entity.dart';

part 'profile.mapper.dart';

@MappableClass(discriminatorValue: 'profile')
class Profile extends Entity with ProfileMappable {
  final String displayName;
  final String email;
  final String userId;
  final String? phoneNumber;
  final String? photoURL;

  Profile({required super.active, required super.id, required super.cachedAt, required this.displayName, required this.email, required this.userId, required super.etag, this.phoneNumber, this.photoURL}) : super();

  @override
  toString() => 'Profile: $displayName (email: $email, userId: $userId, id: $id)';
}

@MappableClass()
class ProfileCreateModel extends CreateModel with ProfileCreateModelMappable {
  final String displayName;
  final String email;
  final String userId;
  final String? phoneNumber;
  final String? photoURL;

  ProfileCreateModel({required this.displayName, required this.email, required this.userId, this.phoneNumber, this.photoURL}) : super();

  @override
  toString() => 'ProfileCreateModel: $displayName (email: $email, userId: $userId)';
}

@MappableClass()
class ProfileUpdateModel extends UpdateModel with ProfileUpdateModelMappable {
  final String displayName;
  final String email;
  final String? phoneNumber;
  final String? photoURL;

  ProfileUpdateModel({required this.displayName, required this.email, this.phoneNumber, this.photoURL}) : super();

  @override
  toString() => 'ProfileUpdateModel: $displayName (email: $email, phoneNumber: $phoneNumber, photoURL: $photoURL)';
}
