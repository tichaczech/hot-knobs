import '../../../domain/models/entity.dart';
import '../../../utils/result.dart';
import '../../repositories/repository.dart';

abstract class LocalService<TEntity extends Entity> {
  Future<Result<TEntity>> createOrUpdate(TEntity entity);
  Future<Result<void>> delete(String id);
  Future<Result<TEntity?>> get(String id, {bool onlyActive = true});
  Future<Result<List<String>>> list({String? query, bool onlyActive = true, Duration maxAge = cacheTTL});
}
