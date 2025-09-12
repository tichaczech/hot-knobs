
import '../../../domain/models/entity.dart';
import '../../../utils/result.dart';

abstract class RemoteService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> {
  Future<Result<TEntity>> create(TCreateModel createModel);
  Future<Result<void>> delete(String id, String etag);
  Future<Result<TEntity?>> get(String id, {bool onlyActive = true, String? etag});
  Future<Result<List<String>>> list({String? query, bool onlyActive = true});
  Future<Result<TEntity>> update(String id, TUpdateModel updateModel, String etag);
}
