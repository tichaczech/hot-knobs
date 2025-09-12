import 'package:frontend/data/repositories/repository.dart';
import 'package:frontend/data/services/local/local_service.dart';
import 'package:frontend/domain/models/entity.dart';
import 'package:frontend/utils/result.dart';

abstract class DummyService<TEntity extends Entity> implements LocalService<TEntity> {
  @override
  Future<Result<TEntity>> createOrUpdate(TEntity entity) {
    return Future.value(Result.ok(entity));
  }

  @override
  Future<Result<void>> delete(String id) {
    return Future.value(Result.ok(null));
  }

  @override
  Future<Result<TEntity?>> get(String id, {bool onlyActive = true}) {
    return Future.value(Result.ok(null));
  }

  @override
  Future<Result<List<String>>> list({String? query, bool onlyActive = true, Duration maxAge = cacheTTL}) {
    return Future.value(Result.ok([]));
  }
}
