import '../../../domain/models/entity.dart';
// import '../../repositories/repository.dart';

abstract class LocalService<TEntity extends Entity> {
  Future<TEntity> createOrUpdate(TEntity entity);
  Future<void> delete(String id);
  Future<TEntity?> get(String id, {bool onlyActive = true});
  Future<List<String>> list({String? query, bool onlyActive = true, Duration maxAge = const Duration(minutes: 5)});
}
