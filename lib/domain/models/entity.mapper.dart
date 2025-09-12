// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'entity.dart';

class EntityMapper extends ClassMapperBase<Entity> {
  EntityMapper._();

  static EntityMapper? _instance;
  static EntityMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = EntityMapper._());
      MapperContainer.globals.useAll([DateTimeDecoderOnlyMapper()]);
    }
    return _instance!;
  }

  @override
  final String id = 'Entity';

  static String _$id(Entity v) => v.id;
  static const Field<Entity, String> _f$id = Field('id', _$id);
  static bool _$active(Entity v) => v.active;
  static const Field<Entity, bool> _f$active = Field('active', _$active);
  static DateTime _$cachedAt(Entity v) => v.cachedAt;
  static const Field<Entity, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(Entity v) => v.createdAt;
  static const Field<Entity, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(Entity v) => v.createdBy;
  static const Field<Entity, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String _$etag(Entity v) => v.etag;
  static const Field<Entity, String> _f$etag = Field('etag', _$etag);
  static DateTime _$updatedAt(Entity v) => v.updatedAt;
  static const Field<Entity, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(Entity v) => v.updatedBy;
  static const Field<Entity, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );

  @override
  final MappableFields<Entity> fields = const {
    #id: _f$id,
    #active: _f$active,
    #cachedAt: _f$cachedAt,
    #createdAt: _f$createdAt,
    #createdBy: _f$createdBy,
    #etag: _f$etag,
    #updatedAt: _f$updatedAt,
    #updatedBy: _f$updatedBy,
  };

  static Entity _instantiate(DecodingData data) {
    throw MapperException.missingConstructor('Entity');
  }

  @override
  final Function instantiate = _instantiate;

  static Entity fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Entity>(map);
  }

  static Entity fromJson(String json) {
    return ensureInitialized().decodeJson<Entity>(json);
  }
}

mixin EntityMappable {
  String toJson();
  Map<String, dynamic> toMap();
  EntityCopyWith<Entity, Entity, Entity> get copyWith;
}

abstract class EntityCopyWith<$R, $In extends Entity, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    String? etag,
    DateTime? updatedAt,
    String? updatedBy,
  });
  EntityCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class CreateModelMapper extends ClassMapperBase<CreateModel> {
  CreateModelMapper._();

  static CreateModelMapper? _instance;
  static CreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = CreateModelMapper._());
      MapperContainer.globals.useAll([DateTimeDecoderOnlyMapper()]);
    }
    return _instance!;
  }

  @override
  final String id = 'CreateModel';

  @override
  final MappableFields<CreateModel> fields = const {};

  static CreateModel _instantiate(DecodingData data) {
    throw MapperException.missingConstructor('CreateModel');
  }

  @override
  final Function instantiate = _instantiate;

  static CreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<CreateModel>(map);
  }

  static CreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<CreateModel>(json);
  }
}

mixin CreateModelMappable {
  String toJson();
  Map<String, dynamic> toMap();
  CreateModelCopyWith<CreateModel, CreateModel, CreateModel> get copyWith;
}

abstract class CreateModelCopyWith<$R, $In extends CreateModel, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call();
  CreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class UpdateModelMapper extends ClassMapperBase<UpdateModel> {
  UpdateModelMapper._();

  static UpdateModelMapper? _instance;
  static UpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = UpdateModelMapper._());
      MapperContainer.globals.useAll([DateTimeDecoderOnlyMapper()]);
    }
    return _instance!;
  }

  @override
  final String id = 'UpdateModel';

  @override
  final MappableFields<UpdateModel> fields = const {};

  static UpdateModel _instantiate(DecodingData data) {
    throw MapperException.missingConstructor('UpdateModel');
  }

  @override
  final Function instantiate = _instantiate;

  static UpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<UpdateModel>(map);
  }

  static UpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<UpdateModel>(json);
  }
}

mixin UpdateModelMappable {
  String toJson();
  Map<String, dynamic> toMap();
  UpdateModelCopyWith<UpdateModel, UpdateModel, UpdateModel> get copyWith;
}

abstract class UpdateModelCopyWith<$R, $In extends UpdateModel, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call();
  UpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

