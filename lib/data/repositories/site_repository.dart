import '../../../domain/models/site.dart';
import './repository.dart';

class SiteRepository extends Repository<Site, SiteCreateModel, SiteUpdateModel> {
  SiteRepository({required super.remoteService, required super.localService}) : super();
}
