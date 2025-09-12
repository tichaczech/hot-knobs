import 'package:cloud_firestore/cloud_firestore.dart';

import '../../../domain/models/entity.dart';
import '../../../domain/target_mapping.dart';
import '../../../utils/result.dart';
import '../../repositories/repository.dart';
import 'remote_service.dart';

abstract class FirebaseService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> implements RemoteService<TEntity, TCreateModel, TUpdateModel> {
  late final String _collectionName;
  late final FirebaseFirestore _db;

  FirebaseService({required FirebaseFirestore firestore}) {
    _collectionName = TEntity.toString().toLowerCase();

    _db = firestore;
  }

  Result<TEntity> fromMap(Map<String, dynamic> map);

  @override
  Future<Result<TEntity>> create(TCreateModel model) async {
    final entity = model.toTargetMap(MapTarget.firestore);
    entity['active'] = true;
    entity['createdAt'] = DateTime.now();
    entity['createdBy'] = 'user_id';
    entity['etag'] = uuid.v4();
    entity['updatedAt'] = DateTime.now();
    entity['updatedBy'] = 'user_id';

    final ref = await _db.collection(_collectionName).add(entity);
    final snap = await ref.get();
    final result = fromMap({...snap.data()!, 'id': snap.id, 'cachedAt': DateTime.now()});

    return result;
  }

  @override
  Future<Result<void>> delete(String id, String etag) async {
    final ref = _db.collection(_collectionName).doc(id);
    final snap = await ref.get();
    if (!snap.exists) {
      return Result.error(Exception('Document not found'));
    }

    final doc = snap.data();
    if (doc == null) {
      return Result.error(Exception('Document not found!'));
    }

    if (doc['etag'] != etag) {
      return Result.error(Exception('ETag mismatch!'));
    }

    await ref.update({'active': false, 'updatedAt': DateTime.now(), 'updatedBy': 'user_id'});

    return Result.ok(null);
  }

  @override
  Future<Result<TEntity?>> get(String id, {bool onlyActive = true, String? etag}) async {
    final ref = _db.collection(_collectionName).doc(id);
    final snap = await ref.get();
    if (!snap.exists) {
      return Result.error(Exception('Document not found'));
    }

    final doc = snap.data();
    if (doc == null) {
      return Result.error(Exception('Document not found'));
    }

    if (etag != null && doc['etag'] != etag) {
      return Result.error(Exception('ETag mismatch'));
    }

    final result = fromMap({...snap.data()!, 'id': snap.id, 'cachedAt': DateTime.now()});

    return result;
  }

  @override
  Future<Result<List<String>>> list({String? query, bool onlyActive = true}) async {
    final ref = _db.collection(_collectionName);
    Query fQuery = ref.where('active', isEqualTo: onlyActive);
    if (query != null && query.isNotEmpty) {
      fQuery = fQuery.where('name', isEqualTo: query);
    }

    QuerySnapshot snap = await fQuery.get();

    final docs = snap.docs;
    final results = docs.map((doc) => doc.id).toList();

    return Result.ok(results);
  }

  @override
  Future<Result<TEntity>> update(String id, TUpdateModel model, String etag) async {
    final ref = _db.collection(_collectionName).doc(id);
    final snap = await ref.get();
    if (!snap.exists) {
      return Result.error(Exception('Document not found'));
    }

    final doc = snap.data();
    if (doc == null) {
      return Result.error(Exception('Document not found'));
    }

    if (doc['etag'] != etag) {
      return Result.error(Exception('ETag mismatch'));
    }

    final updates = {...model.toTargetMap(MapTarget.firestore), 'active': true, 'etag': uuid.v4(), 'updatedAt': Timestamp.now(), 'updatedBy': 'user_id'};
    await ref.update(updates);

    final result = fromMap({...doc, ...updates, 'id': snap.id, 'cachedAt': DateTime.now()});

    return result;
  }
}
