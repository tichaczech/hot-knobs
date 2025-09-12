import '../../../domain/models/user_profile.dart';
import '../../../utils/result.dart';
import 'firebase_service.dart';

class UserProfileFirebaseService extends FirebaseService<UserProfile, UserProfileCreateModel, UserProfileUpdateModel> {
  UserProfileFirebaseService({required super.firestore});

  @override
  Result<UserProfile> fromMap(Map<String, dynamic> map) {
    try {
      return Result.ok(UserProfileMapper.fromMap(map));
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
