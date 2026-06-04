import '../../../domain/models/users/profile.dart';
import '../../../utils/result.dart';
import '../repository.dart';

class ProfileRepository extends Repository<Profile, ProfileCreateModel, ProfileUpdateModel> {
  ProfileRepository({required super.remoteService, required super.localService}) : super();

  Future<Result<Profile>> getMy({bool forceRefresh = false}) async {
    return await super.get('my', forceRefresh: forceRefresh);
  }
}
