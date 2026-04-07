import 'package:cloud_firestore/cloud_firestore.dart';

import '../../../domain/models/entity.dart';
import '../../../domain/target_mapping.dart';
import '../../repositories/repository.dart';
import 'remote_service.dart';

class DocumentNotFoundException implements Exception {
  final String message;
  DocumentNotFoundException(this.message);

  @override
  String toString() => 'DocumentNotFoundException: $message';
}

class ETagMismatchException implements Exception {
  final String message;
  ETagMismatchException(this.message);

  @override
  String toString() => 'ETagMismatchException: $message';
}

abstract class RestAPIService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> implements RemoteService<TEntity, TCreateModel, TUpdateModel> {
  late final String _endpoint;

  RestAPIService({String? endpoint}) {
    _endpoint = endpoint ?? TEntity.toString().toLowerCase();
  }

  TEntity fromMap(Map<String, dynamic> map);

  @override
  Future<TEntity> create(TCreateModel createModel) async {
    // Implement REST API call to create entity
    throw UnimplementedError();
  }

  @override
  Future<void> delete(String id, String etag) async {
    // Implement REST API call to delete entity
    throw UnimplementedError();
  }

  @override
  Future<TEntity?> get(String id, {bool onlyActive = true, String? etag}) async {
    // Implement REST API call to get entity by ID
    throw UnimplementedError();
  }

  @override
  Future<List<String>> list({String? query, bool onlyActive = true}) async {
    // Implement REST API call to list entities
    throw UnimplementedError();
  }

  @override
  Future<TEntity> update(String id, TUpdateModel updateModel, String etag) async {
    // Implement REST API call to update entity
    throw UnimplementedError();
  }
}

abstract class FirebaseService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> implements RemoteService<TEntity, TCreateModel, TUpdateModel> {
  late final String _collectionName;
  late final FirebaseFirestore _db;

  Map<String, dynamic> Function(TCreateModel)? mapCreateModel;
  Map<String, dynamic> Function(TUpdateModel)? mapUpdateModel;

  FirebaseService({required FirebaseFirestore firestore, String? collectionName}) {
    _collectionName = collectionName ?? TEntity.toString().toLowerCase();

    _db = firestore;
  }

  TEntity fromMap(Map<String, dynamic> map);

  @override
  Future<TEntity> create(TCreateModel createModel) async {
    final createMap = (mapCreateModel != null) ? mapCreateModel!(createModel) : createModel.toTargetMap(MapTarget.firestore);

    createMap['active'] = true;
    createMap['createdAt'] = DateTime.now();
    createMap['createdBy'] = 'user_id';
    createMap['etag'] = uuid.v4();
    createMap['updatedAt'] = DateTime.now();
    createMap['updatedBy'] = 'user_id';

    final id = createMap.remove('id') ?? _db.collection(_collectionName).doc().id;
    final ref = _db.collection(_collectionName).doc(id);
    await ref.set(createMap);

    final entity = await get(id);
    if (entity == null) {
      throw Exception('Failed to create entity');
    }

    return entity;
  }

  @override
  Future<void> delete(String id, String etag) async {
    final (ref, snap, entityMap) = await getDocumentSnapshot(id);
    if (entityMap['etag'] != etag) {
      throw ETagMismatchException('ETag mismatch!');
    }

    await ref.update({'active': false, 'updatedAt': DateTime.now(), 'updatedBy': 'user_id'});
  }

  @override
  Future<TEntity?> get(String id, {bool onlyActive = true, String? etag}) async {
    final (ref, snap, entityMap) = await getDocumentSnapshot(id);
    if (etag != null && entityMap['etag'] == etag) {
      return null;
    }

    final result = fromMap({...entityMap, 'id': snap.id, 'cachedAt': DateTime.now()});

    return result;
  }

  @override
  Future<List<String>> list({String? query, bool onlyActive = true}) async {
    final ref = _db.collection(_collectionName);
    Query fQuery = ref.where('active', isEqualTo: onlyActive);
    if (query != null && query.isNotEmpty) {
      fQuery = fQuery.where('name', isEqualTo: query);
    }

    QuerySnapshot snap = await fQuery.get();
    final results = snap.docs.map((doc) => doc.id).toList();

    return results;
  }

  @override
  Future<TEntity> update(String id, TUpdateModel updateModel, String etag) async {
    final (ref, snap, entityMap) = await getDocumentSnapshot(id);
    if (entityMap['etag'] != etag) {
      throw ETagMismatchException('ETag mismatch');
    }

    final updateMap = (mapUpdateModel != null) ? mapUpdateModel!(updateModel) : updateModel.toTargetMap(MapTarget.firestore);
    final updates = {...updateMap, 'active': true, 'etag': uuid.v4(), 'updatedAt': Timestamp.now(), 'updatedBy': 'user_id'};
    await ref.update(updates);

    final result = fromMap({...entityMap, ...updates, 'id': snap.id, 'cachedAt': DateTime.now()});

    return result;
  }

  Future<(DocumentReference ref, DocumentSnapshot snap, Map<String, dynamic> data)> getDocumentSnapshot(String id) async {
    final ref = _db.collection(_collectionName).doc(id);
    final snap = await ref.get();
    if (!snap.exists) {
      throw DocumentNotFoundException('Document not found!');
    }

    final entityMap = snap.data();
    if (entityMap == null) {
      throw Exception('Document empty!');
    }

    return (ref, snap, entityMap);
  }
}
