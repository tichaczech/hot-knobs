import 'package:dart_mappable/dart_mappable.dart';
import '../mappers/datetime.dart';
import './target_mapping.dart';

part 'entity.mapper.dart';

@MappableClass(discriminatorKey: '__discriminator', includeCustomMappers: [DateTimeDecoderOnlyMapper()])
abstract class Entity with EntityMappable, ModelTargetMapping {
  final String id;
  final bool active;
  final DateTime cachedAt;
  // final DateTime createdAt;
  // final String createdBy;
  final String etag;
  // final DateTime updatedAt;
  // final String updatedBy;

  Entity({
    required this.id,
    required this.active,
    required this.cachedAt,
    // required this.createdAt,
    // required this.createdBy,
    required this.etag,
    // required this.updatedAt,
    // required this.updatedBy
  });
}

@MappableClass(includeCustomMappers: [DateTimeDecoderOnlyMapper()])
abstract class CreateModel with CreateModelMappable, ModelTargetMapping {}

@MappableClass(includeCustomMappers: [DateTimeDecoderOnlyMapper()])
abstract class UpdateModel with UpdateModelMappable, ModelTargetMapping {}
