import 'package:dart_mappable/dart_mappable.dart';

import 'entity.dart';
import 'types.dart';

part 'user_profile.mapper.dart';

@MappableClass(discriminatorValue: 'userProfile')
abstract class UserProfile extends Entity with UserProfileMappable {
  final List<DeviceRegistration> deviceRegistrations;
  final String? displayName;
  final String email;
  final String? phoneNumber;
  final String? photoUrl;

  UserProfile({
    required super.id,
    required super.active,
    required super.cachedAt,
    required super.createdAt,
    required super.createdBy,
    this.deviceRegistrations = const [],
    this.displayName,
    required this.email,
    required super.etag,
    this.phoneNumber,
    this.photoUrl,
    required super.updatedAt,
    required super.updatedBy,
  });
}

@MappableClass()
class UserProfileCreateModel extends CreateModel with UserProfileCreateModelMappable {
  final DeviceRegistration deviceRegistration;
  final String? displayName;
  final String email;
  final String? phoneNumber;
  final String? photoUrl;
  final String uid;

  UserProfileCreateModel({required this.deviceRegistration, this.displayName, required this.email, this.phoneNumber, this.photoUrl, required this.uid});
}

@MappableClass()
class UserProfileUpdateModel extends UpdateModel with UserProfileUpdateModelMappable {
  final DeviceRegistration deviceRegistration;
  final String? displayName;
  final String email;
  final String? phoneNumber;
  final String? photoUrl;

  UserProfileUpdateModel({required this.deviceRegistration, this.displayName, required this.email, this.phoneNumber, this.photoUrl});
}
