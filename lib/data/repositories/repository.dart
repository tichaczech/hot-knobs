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
      if (apiResult is! Ok) {
        return apiResult;
      }

      final dbResult = await localService.createOrUpdate((apiResult as Ok<TEntity>).value!);
      return dbResult;
    } on Exception catch (e) {
      throw Exception(e);
    }
  }

  /// Deletes the [TEntity] with specified id.
  Future<Result<void>> delete(String id, String etag) async {
    try {
      await remoteService.delete(id, etag);
      return localService.delete(id);
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
        if (dbResult is Ok<TEntity> && dbResult.value != null) {
          if (dbResult.value!.cachedAt.isAfter(DateTime.now().subtract(cacheTTL))) {
            return dbResult;
          }

          cachedEntity = dbResult.value;
          etag = dbResult.value?.etag;
        }
      }

      final apiResult = await remoteService.get(id, onlyActive: true, etag: etag);
      switch (apiResult) {
        case Ok():
          if (apiResult.value != null) {
            return await localService.createOrUpdate(apiResult.value!);
          }

          if (cachedEntity == null) {
            throw Exception('Invalid operation branch! This should not happen.');
          }

          return Result.ok(cachedEntity);
        case Error():
          return Result.error(apiResult.error);
      }
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  /// Returns the list of [TEntity] for the current user.
  Future<Result<List<String>>> list({String? query, bool forceRefresh = false}) async {
    try {
      if (!forceRefresh) {
        final dbResult = await localService.list(query: query);
        if (dbResult is Ok<List<String>> && dbResult.value?.isNotEmpty == true) {
          return dbResult;
        }
      }

      return await remoteService.list(query: query);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }

  Future<Result<TEntity>> update(String id, TUpdateModel model, String etag) async {
    try {
      final apiResult = await remoteService.update(id, model, etag);
      if (apiResult is! Ok) {
        return apiResult;
      }

      return localService.createOrUpdate((apiResult as Ok<TEntity>).value!);
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
