import 'package:logging/logging.dart';

import '../../../../domain/models/users/profile.dart';
import '../restapi_service.dart';

class ProfileRestAPIService extends RestAPIService<Profile, ProfileCreateModel, ProfileUpdateModel> {
  ProfileRestAPIService({required super.authProvider, required super.configProvider, super.httpClient}) : super(endpoint: 'profiles', log: Logger('Data:Services:Remote:Users:ProfileRestAPIService'), createHttpMethod: HttpMethod.put);

  @override
  Profile fromMap(Map<String, dynamic> map) => ProfileMapper.fromMap(map);
}
