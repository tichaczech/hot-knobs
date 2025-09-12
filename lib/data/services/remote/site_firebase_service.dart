import '../../../domain/models/site.dart';
import '../../../utils/result.dart';
import 'firebase_service.dart';

class SiteFirebaseService extends FirebaseService<Site, SiteCreateModel, SiteUpdateModel> {
  SiteFirebaseService({required super.firestore});

  @override
  Result<Site> fromMap(Map<String, dynamic> map) {
    try {
      return Result.ok(SiteMapper.fromMap(map));
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
