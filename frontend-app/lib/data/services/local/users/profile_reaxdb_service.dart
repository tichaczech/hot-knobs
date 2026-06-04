import '../../../../domain/models/users/profile.dart';
import '../reaxdb_service.dart';

class ProfileReaxDBService extends ReaxDBService<Profile> {
  ProfileReaxDBService({required super.db});

  @override
  Profile fromMap(Map<String, dynamic> map) => ProfileMapper.fromMap(map);
}
