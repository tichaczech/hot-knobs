import 'package:dart_mappable/dart_mappable.dart';

// import '../mappers/opening_hours.dart';
import 'entity.dart';
import 'types.dart';

part 'user_profile.mapper.dart';

// @MappableClass(discriminatorValue: 'userProfile', includeCustomMappers: [DeviceRegistrationDecoderOnlyMapper()])
@MappableClass(discriminatorValue: 'userProfile')
class UserProfile extends Entity with UserProfileMappable {
  final List<DeviceRegistration> deviceRegistrations;
  final String displayName;
  final String email;
  final String? phoneNumber;
  final String? photoURL;

  UserProfile({
    required super.id,
    required super.active,
    required super.cachedAt,
    required super.createdAt,
    required super.createdBy,
    this.deviceRegistrations = const [],
    required this.displayName,
    required this.email,
    required super.etag,
    this.phoneNumber,
    this.photoURL,
    required super.updatedAt,
    required super.updatedBy
  }) : super() {
    print('UserProfile created: $id');
  }
}

// @MappableClass(includeCustomMappers: [DeviceRegistrationDecoderOnlyMapper()])
@MappableClass()
class UserProfileCreateModel extends CreateModel with UserProfileCreateModelMappable {
  final DeviceRegistration deviceRegistration;
  final String displayName;
  final String email;
  final String? phoneNumber;
  final String? photoURL;
  final String uid;

  UserProfileCreateModel({required this.deviceRegistration, required this.displayName, required this.email, this.phoneNumber, this.photoURL, required this.uid});
}

// @MappableClass(includeCustomMappers: [DeviceRegistrationDecoderOnlyMapper()])
@MappableClass()
class UserProfileUpdateModel extends UpdateModel with UserProfileUpdateModelMappable {
  final DeviceRegistration deviceRegistration;
  final String displayName;
  final String email;
  final String? phoneNumber;
  final String? photoURL;

  UserProfileUpdateModel({required this.deviceRegistration, required this.displayName, required this.email, this.phoneNumber, this.photoURL});
}
