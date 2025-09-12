import '../../domain/models/user_profile.dart';
import '../repositories/repository.dart';

class UserProfileRepository extends Repository<UserProfile, UserProfileCreateModel, UserProfileUpdateModel> {
  UserProfileRepository({required super.remoteService, required super.localService}) : super();
}
