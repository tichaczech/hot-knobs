
import '../../../domain/models/entity.dart';

class DocumentDeletedException implements Exception {
  final String message;
  DocumentDeletedException(this.message);

  @override
  String toString() => 'DocumentDeletedException: $message';
}

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

class PreconditionFailedException implements Exception {
  final String message;
  PreconditionFailedException(this.message);

  @override
  String toString() => 'PreconditionFailedException: $message';
}

abstract class RemoteService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> {
  Future<TEntity> create(TCreateModel createModel);
  Future<void> delete(String id, String etag);
  Future<TEntity?> get(String id, {bool onlyActive = true, String? etag});
  Future<List<String>> list({String? query, bool onlyActive = true});
  Future<TEntity> update(String id, TUpdateModel updateModel, String etag);
}
