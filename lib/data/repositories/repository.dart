import 'package:uuid/uuid.dart';

import '../../domain/models/entity.dart';
import '../../utils/result.dart';
import '../services/local/local_service.dart';
import '../services/remote/remote_service.dart';

const Uuid uuid = Uuid();
const Duration cacheTTL = Duration(minutes: 5);

abstract class Repository<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> {
  late final LocalService<TEntity> localService;
  late final RemoteService<TEntity, TCreateModel, TUpdateModel> remoteService;

  Repository({required this.localService, required this.remoteService});

  /// Creates a new [TEntity].
  Future<Result<TEntity>> create(TCreateModel model) async {
    try {
      final apiResult = await remoteService.create(model);
      final dbResult = await localService.createOrUpdate(apiResult);

      return Result.ok(dbResult);
    } on Exception catch (e) {
      throw Result.error(e);
    }
  }

  /// Deletes the [TEntity] with specified id.
  Future<Result<void>> delete(String id, String etag) async {
    try {
      await remoteService.delete(id, etag);
      await localService.delete(id);

      return Result.ok(null);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  /// Returns a full [TEntity] given the id.
  Future<Result<TEntity>> get(String id, {bool onlyActive = true, bool forceRefresh = false}) async {
    try {
      TEntity? cachedEntity;
      String? etag;

      if (!forceRefresh) {
        final dbResult = await localService.get(id);
        if (dbResult != null) {
          if (dbResult.cachedAt.isAfter(DateTime.now().subtract(cacheTTL))) {
            return Result.ok(dbResult);
          }

          cachedEntity = dbResult;
          etag = dbResult.etag;
        }
      }

      final apiResult = await remoteService.get(id, onlyActive: true, etag: etag);
      if (apiResult != null) {
        final refreshedEntity = await localService.createOrUpdate(apiResult);
        return Result.ok(refreshedEntity);
      }

      // if (cachedEntity == null) {
      //   throw Exception('Invalid operation branch! This should not happen.');
      // }

      return Result.ok(cachedEntity);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  /// Returns the list of [TEntity] for the current user.
  Future<Result<List<String>>> list({String? query, bool forceRefresh = false}) async {
    try {
      if (!forceRefresh) {
        final dbResult = await localService.list(query: query);
        if (dbResult.isNotEmpty == true) {
          return Result.ok(dbResult);
        }
      }

      final apiResult = await remoteService.list(query: query);
      return Result.ok(apiResult);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  Future<Result<TEntity>> update(String id, TUpdateModel model, String etag) async {
    try {
      final apiResult = await remoteService.update(id, model, etag);
      final dbResult = await localService.createOrUpdate(apiResult);

      return Result.ok(dbResult);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
