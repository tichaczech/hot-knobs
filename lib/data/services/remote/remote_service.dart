
import '../../../domain/models/entity.dart';

abstract class RemoteService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> {
  Future<TEntity> create(TCreateModel createModel);
  Future<void> delete(String id, String etag);
  Future<TEntity?> get(String id, {bool onlyActive = true, String? etag});
  Future<List<String>> list({String? query, bool onlyActive = true});
  Future<TEntity> update(String id, TUpdateModel updateModel, String etag);
}
