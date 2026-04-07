// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'operator.dart';

class OperatorMapper extends SubClassMapperBase<Operator> {
  OperatorMapper._();

  static OperatorMapper? _instance;
  static OperatorMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = OperatorMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
    }
    return _instance!;
  }

  @override
  final String id = 'Operator';

  static String _$id(Operator v) => v.id;
  static const Field<Operator, String> _f$id = Field('id', _$id);
  static bool _$active(Operator v) => v.active;
  static const Field<Operator, bool> _f$active = Field('active', _$active);
  static DateTime _$cachedAt(Operator v) => v.cachedAt;
  static const Field<Operator, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(Operator v) => v.createdAt;
  static const Field<Operator, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(Operator v) => v.createdBy;
  static const Field<Operator, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String _$etag(Operator v) => v.etag;
  static const Field<Operator, String> _f$etag = Field('etag', _$etag);
  static DateTime _$updatedAt(Operator v) => v.updatedAt;
  static const Field<Operator, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(Operator v) => v.updatedBy;
  static const Field<Operator, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );
  static String _$description(Operator v) => v.description;
  static const Field<Operator, String> _f$description = Field(
    'description',
    _$description,
  );
  static String? _$logoUrl(Operator v) => v.logoUrl;
  static const Field<Operator, String> _f$logoUrl = Field(
    'logoUrl',
    _$logoUrl,
    opt: true,
  );
  static String _$name(Operator v) => v.name;
  static const Field<Operator, String> _f$name = Field('name', _$name);

  @override
  final MappableFields<Operator> fields = const {
    #id: _f$id,
    #active: _f$active,
    #cachedAt: _f$cachedAt,
    #createdAt: _f$createdAt,
    #createdBy: _f$createdBy,
    #etag: _f$etag,
    #updatedAt: _f$updatedAt,
    #updatedBy: _f$updatedBy,
    #description: _f$description,
    #logoUrl: _f$logoUrl,
    #name: _f$name,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'operator';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static Operator _instantiate(DecodingData data) {
    return Operator(
      id: data.dec(_f$id),
      active: data.dec(_f$active),
      cachedAt: data.dec(_f$cachedAt),
      createdAt: data.dec(_f$createdAt),
      createdBy: data.dec(_f$createdBy),
      etag: data.dec(_f$etag),
      updatedAt: data.dec(_f$updatedAt),
      updatedBy: data.dec(_f$updatedBy),
      description: data.dec(_f$description),
      logoUrl: data.dec(_f$logoUrl),
      name: data.dec(_f$name),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Operator fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Operator>(map);
  }

  static Operator fromJson(String json) {
    return ensureInitialized().decodeJson<Operator>(json);
  }
}

mixin OperatorMappable {
  String toJson() {
    return OperatorMapper.ensureInitialized().encodeJson<Operator>(
      this as Operator,
    );
  }

  Map<String, dynamic> toMap() {
    return OperatorMapper.ensureInitialized().encodeMap<Operator>(
      this as Operator,
    );
  }

  OperatorCopyWith<Operator, Operator, Operator> get copyWith =>
      _OperatorCopyWithImpl<Operator, Operator>(
        this as Operator,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return OperatorMapper.ensureInitialized().stringifyValue(this as Operator);
  }

  @override
  bool operator ==(Object other) {
    return OperatorMapper.ensureInitialized().equalsValue(
      this as Operator,
      other,
    );
  }

  @override
  int get hashCode {
    return OperatorMapper.ensureInitialized().hashValue(this as Operator);
  }
}

extension OperatorValueCopy<$R, $Out> on ObjectCopyWith<$R, Operator, $Out> {
  OperatorCopyWith<$R, Operator, $Out> get $asOperator =>
      $base.as((v, t, t2) => _OperatorCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class OperatorCopyWith<$R, $In extends Operator, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    String? etag,
    DateTime? updatedAt,
    String? updatedBy,
    String? description,
    String? logoUrl,
    String? name,
  });
  OperatorCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _OperatorCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, Operator, $Out>
    implements OperatorCopyWith<$R, Operator, $Out> {
  _OperatorCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Operator> $mapper =
      OperatorMapper.ensureInitialized();
  @override
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    String? etag,
    DateTime? updatedAt,
    String? updatedBy,
    String? description,
    Object? logoUrl = $none,
    String? name,
  }) => $apply(
    FieldCopyWithData({
      if (id != null) #id: id,
      if (active != null) #active: active,
      if (cachedAt != null) #cachedAt: cachedAt,
      if (createdAt != null) #createdAt: createdAt,
      if (createdBy != null) #createdBy: createdBy,
      if (etag != null) #etag: etag,
      if (updatedAt != null) #updatedAt: updatedAt,
      if (updatedBy != null) #updatedBy: updatedBy,
      if (description != null) #description: description,
      if (logoUrl != $none) #logoUrl: logoUrl,
      if (name != null) #name: name,
    }),
  );
  @override
  Operator $make(CopyWithData data) => Operator(
    id: data.get(#id, or: $value.id),
    active: data.get(#active, or: $value.active),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    createdAt: data.get(#createdAt, or: $value.createdAt),
    createdBy: data.get(#createdBy, or: $value.createdBy),
    etag: data.get(#etag, or: $value.etag),
    updatedAt: data.get(#updatedAt, or: $value.updatedAt),
    updatedBy: data.get(#updatedBy, or: $value.updatedBy),
    description: data.get(#description, or: $value.description),
    logoUrl: data.get(#logoUrl, or: $value.logoUrl),
    name: data.get(#name, or: $value.name),
  );

  @override
  OperatorCopyWith<$R2, Operator, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _OperatorCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class OperatorCreateModelMapper extends ClassMapperBase<OperatorCreateModel> {
  OperatorCreateModelMapper._();

  static OperatorCreateModelMapper? _instance;
  static OperatorCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = OperatorCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'OperatorCreateModel';

  static String _$description(OperatorCreateModel v) => v.description;
  static const Field<OperatorCreateModel, String> _f$description = Field(
    'description',
    _$description,
  );
  static String _$name(OperatorCreateModel v) => v.name;
  static const Field<OperatorCreateModel, String> _f$name = Field(
    'name',
    _$name,
  );

  @override
  final MappableFields<OperatorCreateModel> fields = const {
    #description: _f$description,
    #name: _f$name,
  };

  static OperatorCreateModel _instantiate(DecodingData data) {
    return OperatorCreateModel(
      description: data.dec(_f$description),
      name: data.dec(_f$name),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static OperatorCreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<OperatorCreateModel>(map);
  }

  static OperatorCreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<OperatorCreateModel>(json);
  }
}

mixin OperatorCreateModelMappable {
  String toJson() {
    return OperatorCreateModelMapper.ensureInitialized()
        .encodeJson<OperatorCreateModel>(this as OperatorCreateModel);
  }

  Map<String, dynamic> toMap() {
    return OperatorCreateModelMapper.ensureInitialized()
        .encodeMap<OperatorCreateModel>(this as OperatorCreateModel);
  }

  OperatorCreateModelCopyWith<
    OperatorCreateModel,
    OperatorCreateModel,
    OperatorCreateModel
  >
  get copyWith =>
      _OperatorCreateModelCopyWithImpl<
        OperatorCreateModel,
        OperatorCreateModel
      >(this as OperatorCreateModel, $identity, $identity);
  @override
  String toString() {
    return OperatorCreateModelMapper.ensureInitialized().stringifyValue(
      this as OperatorCreateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return OperatorCreateModelMapper.ensureInitialized().equalsValue(
      this as OperatorCreateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return OperatorCreateModelMapper.ensureInitialized().hashValue(
      this as OperatorCreateModel,
    );
  }
}

extension OperatorCreateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, OperatorCreateModel, $Out> {
  OperatorCreateModelCopyWith<$R, OperatorCreateModel, $Out>
  get $asOperatorCreateModel => $base.as(
    (v, t, t2) => _OperatorCreateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class OperatorCreateModelCopyWith<
  $R,
  $In extends OperatorCreateModel,
  $Out
>
    implements CreateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({String? description, String? name});
  OperatorCreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _OperatorCreateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, OperatorCreateModel, $Out>
    implements OperatorCreateModelCopyWith<$R, OperatorCreateModel, $Out> {
  _OperatorCreateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<OperatorCreateModel> $mapper =
      OperatorCreateModelMapper.ensureInitialized();
  @override
  $R call({String? description, String? name}) => $apply(
    FieldCopyWithData({
      if (description != null) #description: description,
      if (name != null) #name: name,
    }),
  );
  @override
  OperatorCreateModel $make(CopyWithData data) => OperatorCreateModel(
    description: data.get(#description, or: $value.description),
    name: data.get(#name, or: $value.name),
  );

  @override
  OperatorCreateModelCopyWith<$R2, OperatorCreateModel, $Out2>
  $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _OperatorCreateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class OperatorUpdateModelMapper extends ClassMapperBase<OperatorUpdateModel> {
  OperatorUpdateModelMapper._();

  static OperatorUpdateModelMapper? _instance;
  static OperatorUpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = OperatorUpdateModelMapper._());
      UpdateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'OperatorUpdateModel';

  static String _$description(OperatorUpdateModel v) => v.description;
  static const Field<OperatorUpdateModel, String> _f$description = Field(
    'description',
    _$description,
  );
  static String _$name(OperatorUpdateModel v) => v.name;
  static const Field<OperatorUpdateModel, String> _f$name = Field(
    'name',
    _$name,
  );

  @override
  final MappableFields<OperatorUpdateModel> fields = const {
    #description: _f$description,
    #name: _f$name,
  };

  static OperatorUpdateModel _instantiate(DecodingData data) {
    return OperatorUpdateModel(
      description: data.dec(_f$description),
      name: data.dec(_f$name),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static OperatorUpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<OperatorUpdateModel>(map);
  }

  static OperatorUpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<OperatorUpdateModel>(json);
  }
}

mixin OperatorUpdateModelMappable {
  String toJson() {
    return OperatorUpdateModelMapper.ensureInitialized()
        .encodeJson<OperatorUpdateModel>(this as OperatorUpdateModel);
  }

  Map<String, dynamic> toMap() {
    return OperatorUpdateModelMapper.ensureInitialized()
        .encodeMap<OperatorUpdateModel>(this as OperatorUpdateModel);
  }

  OperatorUpdateModelCopyWith<
    OperatorUpdateModel,
    OperatorUpdateModel,
    OperatorUpdateModel
  >
  get copyWith =>
      _OperatorUpdateModelCopyWithImpl<
        OperatorUpdateModel,
        OperatorUpdateModel
      >(this as OperatorUpdateModel, $identity, $identity);
  @override
  String toString() {
    return OperatorUpdateModelMapper.ensureInitialized().stringifyValue(
      this as OperatorUpdateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return OperatorUpdateModelMapper.ensureInitialized().equalsValue(
      this as OperatorUpdateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return OperatorUpdateModelMapper.ensureInitialized().hashValue(
      this as OperatorUpdateModel,
    );
  }
}

extension OperatorUpdateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, OperatorUpdateModel, $Out> {
  OperatorUpdateModelCopyWith<$R, OperatorUpdateModel, $Out>
  get $asOperatorUpdateModel => $base.as(
    (v, t, t2) => _OperatorUpdateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class OperatorUpdateModelCopyWith<
  $R,
  $In extends OperatorUpdateModel,
  $Out
>
    implements UpdateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({String? description, String? name});
  OperatorUpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _OperatorUpdateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, OperatorUpdateModel, $Out>
    implements OperatorUpdateModelCopyWith<$R, OperatorUpdateModel, $Out> {
  _OperatorUpdateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<OperatorUpdateModel> $mapper =
      OperatorUpdateModelMapper.ensureInitialized();
  @override
  $R call({String? description, String? name}) => $apply(
    FieldCopyWithData({
      if (description != null) #description: description,
      if (name != null) #name: name,
    }),
  );
  @override
  OperatorUpdateModel $make(CopyWithData data) => OperatorUpdateModel(
    description: data.get(#description, or: $value.description),
    name: data.get(#name, or: $value.name),
  );

  @override
  OperatorUpdateModelCopyWith<$R2, OperatorUpdateModel, $Out2>
  $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _OperatorUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

