import '../../../domain/models/user_profile.dart';
import '../../../domain/target_mapping.dart';
import 'firebase_service.dart';

class UserProfileFirebaseService extends FirebaseService<UserProfile, UserProfileCreateModel, UserProfileUpdateModel> {
  UserProfileFirebaseService({required super.firestore}) : super(collectionName: 'userProfiles') {
    super.mapCreateModel = onMapCreateModel;
  }

  Map<String, dynamic> onMapCreateModel(UserProfileCreateModel model) {
    final entity = model.toTargetMap(MapTarget.firestore);
    entity['deviceRegistrations'] = [entity.remove('deviceRegistration')];
    entity['id'] = entity.remove('uid');

    return entity;
  }

  @override
  UserProfile fromMap(Map<String, dynamic> map) => UserProfileMapper.fromMap(map);
}
