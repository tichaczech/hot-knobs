import 'package:reaxdb_dart/reaxdb_dart.dart';

import '../../../domain/models/entity.dart';
import '../../../domain/target_mapping.dart';
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

  TEntity fromMap(Map<String, dynamic> map);

  @override
  Future<TEntity> createOrUpdate(TEntity entity) async {
      final String dbId = await _getDatabaseId(entity.id);
      await _db.put(dbId, entity.toTargetMap(MapTarget.reaxdb));

      return entity;
  }

  @override
  Future<void> delete(String id) async {
      final String dbId = await _getDatabaseId(id);
      await _db.delete(dbId);
  }

  @override
  Future<TEntity?> get(String id, {bool onlyActive = true}) async {
      final String dbId = await _getDatabaseId(id);
      final result = await _db.get(dbId);

      if (result == null) {
        return null;
      }

      return fromMap(result);
  }

  @override
  Future<List<String>> list({String? query, bool onlyActive = true, Duration maxAge = cacheTTL}) async {
    final String dbId = await _getDatabaseId('*');
    final result = await _db.get(dbId);

    // if (result == null) {
    //   return Result.ok([]);
    // }

    print('List result: $result');

    return []; // TODO: Implement!
  }
}
