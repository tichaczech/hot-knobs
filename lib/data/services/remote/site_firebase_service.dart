import '../../../domain/models/site.dart';
import 'firebase_service.dart';

class SiteFirebaseService extends FirebaseService<Site, SiteCreateModel, SiteUpdateModel> {
  SiteFirebaseService({required super.firestore}); // : super(collectionName: 'sites');

  @override
  Site fromMap(Map<String, dynamic> map) => SiteMapper.fromMap(map);
}
