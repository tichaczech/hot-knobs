import 'package:reaxdb_dart/reaxdb_dart.dart';

import '../../../domain/models/entity.dart';
import '../../../domain/target_mapping.dart';
import '../../../utils/result.dart';
import '../../repositories/repository.dart';
import 'local_service.dart';

abstract class ReaxDBService<TEntity extends Entity> implements LocalService<TEntity> {
  late final String _collectionName;
  late final SimpleReaxDB _db;

  ReaxDBService({required SimpleReaxDB db}) {
    _collectionName = TEntity.toString().toLowerCase();

    _db = db;
  }

  Future<String> _getDatabaseId(String id) async {
    return '$_collectionName:$id';
  }

  Result<TEntity> fromMap(Map<String, dynamic> map);

  @override
  Future<Result<TEntity>> createOrUpdate(TEntity entity) async {
    try {
      final String dbId = await _getDatabaseId(entity.id);
      await _db.put(dbId, entity.toTargetMap(MapTarget.reaxdb));

      return Result.ok(entity);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  @override
  Future<Result<void>> delete(String id) async {
    try {
      final String dbId = await _getDatabaseId(id);
      await _db.delete(dbId);

      return Result.ok(null);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  @override
  Future<Result<TEntity?>> get(String id, {bool onlyActive = true}) async {
    try {
      final String dbId = await _getDatabaseId(id);
      final result = await _db.get(dbId);

      if (result == null) {
        return Result.ok(null);
      }

      return fromMap(result);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  @override
  Future<Result<List<String>>> list({String? query, bool onlyActive = true, Duration maxAge = cacheTTL}) async {
    final String dbId = await _getDatabaseId('*');
    final result = await _db.get(dbId);

    // if (result == null) {
    //   return Result.ok([]);
    // }

    print('List result: $result');

    return Result.ok([]);
  }
}
