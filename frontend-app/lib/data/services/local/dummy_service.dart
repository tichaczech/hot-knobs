
import '../../../domain/models/entity.dart';
// import '../../repositories/repository.dart';
import 'local_service.dart';

abstract class DummyService<TEntity extends Entity> implements LocalService<TEntity> {
  @override
  Future<TEntity> createOrUpdate(TEntity entity) => Future.value(entity);

  @override
  Future<void> delete(String id) => Future.value(null);

  @override
  Future<TEntity?> get(String id, {bool onlyActive = true}) => Future.value(null);

  @override
  Future<List<String>> list({String? query, bool onlyActive = true, Duration maxAge = const Duration(minutes: 5)}) => Future.value([]);
}
