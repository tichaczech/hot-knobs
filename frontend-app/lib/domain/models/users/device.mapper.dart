// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'device.dart';

class DeviceMapper extends SubClassMapperBase<Device> {
  DeviceMapper._();

  static DeviceMapper? _instance;
  static DeviceMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = DeviceMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
      PlatformMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'Device';

  static bool _$active(Device v) => v.active;
  static const Field<Device, bool> _f$active = Field('active', _$active);
  static String _$id(Device v) => v.id;
  static const Field<Device, String> _f$id = Field('id', _$id);
  static DateTime _$cachedAt(Device v) => v.cachedAt;
  static const Field<Device, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static String? _$description(Device v) => v.description;
  static const Field<Device, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static String _$deviceId(Device v) => v.deviceId;
  static const Field<Device, String> _f$deviceId = Field(
    'deviceId',
    _$deviceId,
  );
  static String _$etag(Device v) => v.etag;
  static const Field<Device, String> _f$etag = Field('etag', _$etag);
  static DateTime _$lastActiveAt(Device v) => v.lastActiveAt;
  static const Field<Device, DateTime> _f$lastActiveAt = Field(
    'lastActiveAt',
    _$lastActiveAt,
  );
  static String _$modelName(Device v) => v.modelName;
  static const Field<Device, String> _f$modelName = Field(
    'modelName',
    _$modelName,
  );
  static String _$name(Device v) => v.name;
  static const Field<Device, String> _f$name = Field('name', _$name);
  static Platform _$platform(Device v) => v.platform;
  static const Field<Device, Platform> _f$platform = Field(
    'platform',
    _$platform,
  );
  static String _$profileId(Device v) => v.profileId;
  static const Field<Device, String> _f$profileId = Field(
    'profileId',
    _$profileId,
  );
  static String _$systemName(Device v) => v.systemName;
  static const Field<Device, String> _f$systemName = Field(
    'systemName',
    _$systemName,
  );
  static String _$token(Device v) => v.token;
  static const Field<Device, String> _f$token = Field('token', _$token);

  @override
  final MappableFields<Device> fields = const {
    #active: _f$active,
    #id: _f$id,
    #cachedAt: _f$cachedAt,
    #description: _f$description,
    #deviceId: _f$deviceId,
    #etag: _f$etag,
    #lastActiveAt: _f$lastActiveAt,
    #modelName: _f$modelName,
    #name: _f$name,
    #platform: _f$platform,
    #profileId: _f$profileId,
    #systemName: _f$systemName,
    #token: _f$token,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'device';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static Device _instantiate(DecodingData data) {
    return Device(
      active: data.dec(_f$active),
      id: data.dec(_f$id),
      cachedAt: data.dec(_f$cachedAt),
      description: data.dec(_f$description),
      deviceId: data.dec(_f$deviceId),
      etag: data.dec(_f$etag),
      lastActiveAt: data.dec(_f$lastActiveAt),
      modelName: data.dec(_f$modelName),
      name: data.dec(_f$name),
      platform: data.dec(_f$platform),
      profileId: data.dec(_f$profileId),
      systemName: data.dec(_f$systemName),
      token: data.dec(_f$token),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Device fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Device>(map);
  }

  static Device fromJson(String json) {
    return ensureInitialized().decodeJson<Device>(json);
  }
}

mixin DeviceMappable {
  String toJson() {
    return DeviceMapper.ensureInitialized().encodeJson<Device>(this as Device);
  }

  Map<String, dynamic> toMap() {
    return DeviceMapper.ensureInitialized().encodeMap<Device>(this as Device);
  }

  DeviceCopyWith<Device, Device, Device> get copyWith =>
      _DeviceCopyWithImpl<Device, Device>(this as Device, $identity, $identity);
  @override
  String toString() {
    return DeviceMapper.ensureInitialized().stringifyValue(this as Device);
  }

  @override
  bool operator ==(Object other) {
    return DeviceMapper.ensureInitialized().equalsValue(this as Device, other);
  }

  @override
  int get hashCode {
    return DeviceMapper.ensureInitialized().hashValue(this as Device);
  }
}

extension DeviceValueCopy<$R, $Out> on ObjectCopyWith<$R, Device, $Out> {
  DeviceCopyWith<$R, Device, $Out> get $asDevice =>
      $base.as((v, t, t2) => _DeviceCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class DeviceCopyWith<$R, $In extends Device, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  @override
  $R call({
    bool? active,
    String? id,
    DateTime? cachedAt,
    String? description,
    String? deviceId,
    String? etag,
    DateTime? lastActiveAt,
    String? modelName,
    String? name,
    Platform? platform,
    String? profileId,
    String? systemName,
    String? token,
  });
  DeviceCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _DeviceCopyWithImpl<$R, $Out> extends ClassCopyWithBase<$R, Device, $Out>
    implements DeviceCopyWith<$R, Device, $Out> {
  _DeviceCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Device> $mapper = DeviceMapper.ensureInitialized();
  @override
  $R call({
    bool? active,
    String? id,
    DateTime? cachedAt,
    Object? description = $none,
    String? deviceId,
    String? etag,
    DateTime? lastActiveAt,
    String? modelName,
    String? name,
    Platform? platform,
    String? profileId,
    String? systemName,
    String? token,
  }) => $apply(
    FieldCopyWithData({
      if (active != null) #active: active,
      if (id != null) #id: id,
      if (cachedAt != null) #cachedAt: cachedAt,
      if (description != $none) #description: description,
      if (deviceId != null) #deviceId: deviceId,
      if (etag != null) #etag: etag,
      if (lastActiveAt != null) #lastActiveAt: lastActiveAt,
      if (modelName != null) #modelName: modelName,
      if (name != null) #name: name,
      if (platform != null) #platform: platform,
      if (profileId != null) #profileId: profileId,
      if (systemName != null) #systemName: systemName,
      if (token != null) #token: token,
    }),
  );
  @override
  Device $make(CopyWithData data) => Device(
    active: data.get(#active, or: $value.active),
    id: data.get(#id, or: $value.id),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    description: data.get(#description, or: $value.description),
    deviceId: data.get(#deviceId, or: $value.deviceId),
    etag: data.get(#etag, or: $value.etag),
    lastActiveAt: data.get(#lastActiveAt, or: $value.lastActiveAt),
    modelName: data.get(#modelName, or: $value.modelName),
    name: data.get(#name, or: $value.name),
    platform: data.get(#platform, or: $value.platform),
    profileId: data.get(#profileId, or: $value.profileId),
    systemName: data.get(#systemName, or: $value.systemName),
    token: data.get(#token, or: $value.token),
  );

  @override
  DeviceCopyWith<$R2, Device, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _DeviceCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class DeviceCreateModelMapper extends ClassMapperBase<DeviceCreateModel> {
  DeviceCreateModelMapper._();

  static DeviceCreateModelMapper? _instance;
  static DeviceCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = DeviceCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
      PlatformMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'DeviceCreateModel';

  static String _$deviceId(DeviceCreateModel v) => v.deviceId;
  static const Field<DeviceCreateModel, String> _f$deviceId = Field(
    'deviceId',
    _$deviceId,
  );
  static String _$modelName(DeviceCreateModel v) => v.modelName;
  static const Field<DeviceCreateModel, String> _f$modelName = Field(
    'modelName',
    _$modelName,
  );
  static String _$name(DeviceCreateModel v) => v.name;
  static const Field<DeviceCreateModel, String> _f$name = Field('name', _$name);
  static Platform _$platform(DeviceCreateModel v) => v.platform;
  static const Field<DeviceCreateModel, Platform> _f$platform = Field(
    'platform',
    _$platform,
  );
  static String _$systemName(DeviceCreateModel v) => v.systemName;
  static const Field<DeviceCreateModel, String> _f$systemName = Field(
    'systemName',
    _$systemName,
  );
  static String _$token(DeviceCreateModel v) => v.token;
  static const Field<DeviceCreateModel, String> _f$token = Field(
    'token',
    _$token,
  );

  @override
  final MappableFields<DeviceCreateModel> fields = const {
    #deviceId: _f$deviceId,
    #modelName: _f$modelName,
    #name: _f$name,
    #platform: _f$platform,
    #systemName: _f$systemName,
    #token: _f$token,
  };

  static DeviceCreateModel _instantiate(DecodingData data) {
    return DeviceCreateModel(
      deviceId: data.dec(_f$deviceId),
      modelName: data.dec(_f$modelName),
      name: data.dec(_f$name),
      platform: data.dec(_f$platform),
      systemName: data.dec(_f$systemName),
      token: data.dec(_f$token),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static DeviceCreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<DeviceCreateModel>(map);
  }

  static DeviceCreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<DeviceCreateModel>(json);
  }
}

mixin DeviceCreateModelMappable {
  String toJson() {
    return DeviceCreateModelMapper.ensureInitialized()
        .encodeJson<DeviceCreateModel>(this as DeviceCreateModel);
  }

  Map<String, dynamic> toMap() {
    return DeviceCreateModelMapper.ensureInitialized()
        .encodeMap<DeviceCreateModel>(this as DeviceCreateModel);
  }

  DeviceCreateModelCopyWith<
    DeviceCreateModel,
    DeviceCreateModel,
    DeviceCreateModel
  >
  get copyWith =>
      _DeviceCreateModelCopyWithImpl<DeviceCreateModel, DeviceCreateModel>(
        this as DeviceCreateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return DeviceCreateModelMapper.ensureInitialized().stringifyValue(
      this as DeviceCreateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return DeviceCreateModelMapper.ensureInitialized().equalsValue(
      this as DeviceCreateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return DeviceCreateModelMapper.ensureInitialized().hashValue(
      this as DeviceCreateModel,
    );
  }
}

extension DeviceCreateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, DeviceCreateModel, $Out> {
  DeviceCreateModelCopyWith<$R, DeviceCreateModel, $Out>
  get $asDeviceCreateModel => $base.as(
    (v, t, t2) => _DeviceCreateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class DeviceCreateModelCopyWith<
  $R,
  $In extends DeviceCreateModel,
  $Out
>
    implements CreateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? deviceId,
    String? modelName,
    String? name,
    Platform? platform,
    String? systemName,
    String? token,
  });
  DeviceCreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _DeviceCreateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, DeviceCreateModel, $Out>
    implements DeviceCreateModelCopyWith<$R, DeviceCreateModel, $Out> {
  _DeviceCreateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<DeviceCreateModel> $mapper =
      DeviceCreateModelMapper.ensureInitialized();
  @override
  $R call({
    String? deviceId,
    String? modelName,
    String? name,
    Platform? platform,
    String? systemName,
    String? token,
  }) => $apply(
    FieldCopyWithData({
      if (deviceId != null) #deviceId: deviceId,
      if (modelName != null) #modelName: modelName,
      if (name != null) #name: name,
      if (platform != null) #platform: platform,
      if (systemName != null) #systemName: systemName,
      if (token != null) #token: token,
    }),
  );
  @override
  DeviceCreateModel $make(CopyWithData data) => DeviceCreateModel(
    deviceId: data.get(#deviceId, or: $value.deviceId),
    modelName: data.get(#modelName, or: $value.modelName),
    name: data.get(#name, or: $value.name),
    platform: data.get(#platform, or: $value.platform),
    systemName: data.get(#systemName, or: $value.systemName),
    token: data.get(#token, or: $value.token),
  );

  @override
  DeviceCreateModelCopyWith<$R2, DeviceCreateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _DeviceCreateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class DeviceUpdateModelMapper extends ClassMapperBase<DeviceUpdateModel> {
  DeviceUpdateModelMapper._();

  static DeviceUpdateModelMapper? _instance;
  static DeviceUpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = DeviceUpdateModelMapper._());
      UpdateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'DeviceUpdateModel';

  static String? _$description(DeviceUpdateModel v) => v.description;
  static const Field<DeviceUpdateModel, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static String _$name(DeviceUpdateModel v) => v.name;
  static const Field<DeviceUpdateModel, String> _f$name = Field('name', _$name);

  @override
  final MappableFields<DeviceUpdateModel> fields = const {
    #description: _f$description,
    #name: _f$name,
  };

  static DeviceUpdateModel _instantiate(DecodingData data) {
    return DeviceUpdateModel(
      description: data.dec(_f$description),
      name: data.dec(_f$name),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static DeviceUpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<DeviceUpdateModel>(map);
  }

  static DeviceUpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<DeviceUpdateModel>(json);
  }
}

mixin DeviceUpdateModelMappable {
  String toJson() {
    return DeviceUpdateModelMapper.ensureInitialized()
        .encodeJson<DeviceUpdateModel>(this as DeviceUpdateModel);
  }

  Map<String, dynamic> toMap() {
    return DeviceUpdateModelMapper.ensureInitialized()
        .encodeMap<DeviceUpdateModel>(this as DeviceUpdateModel);
  }

  DeviceUpdateModelCopyWith<
    DeviceUpdateModel,
    DeviceUpdateModel,
    DeviceUpdateModel
  >
  get copyWith =>
      _DeviceUpdateModelCopyWithImpl<DeviceUpdateModel, DeviceUpdateModel>(
        this as DeviceUpdateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return DeviceUpdateModelMapper.ensureInitialized().stringifyValue(
      this as DeviceUpdateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return DeviceUpdateModelMapper.ensureInitialized().equalsValue(
      this as DeviceUpdateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return DeviceUpdateModelMapper.ensureInitialized().hashValue(
      this as DeviceUpdateModel,
    );
  }
}

extension DeviceUpdateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, DeviceUpdateModel, $Out> {
  DeviceUpdateModelCopyWith<$R, DeviceUpdateModel, $Out>
  get $asDeviceUpdateModel => $base.as(
    (v, t, t2) => _DeviceUpdateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class DeviceUpdateModelCopyWith<
  $R,
  $In extends DeviceUpdateModel,
  $Out
>
    implements UpdateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({String? description, String? name});
  DeviceUpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _DeviceUpdateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, DeviceUpdateModel, $Out>
    implements DeviceUpdateModelCopyWith<$R, DeviceUpdateModel, $Out> {
  _DeviceUpdateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<DeviceUpdateModel> $mapper =
      DeviceUpdateModelMapper.ensureInitialized();
  @override
  $R call({Object? description = $none, String? name}) => $apply(
    FieldCopyWithData({
      if (description != $none) #description: description,
      if (name != null) #name: name,
    }),
  );
  @override
  DeviceUpdateModel $make(CopyWithData data) => DeviceUpdateModel(
    description: data.get(#description, or: $value.description),
    name: data.get(#name, or: $value.name),
  );

  @override
  DeviceUpdateModelCopyWith<$R2, DeviceUpdateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _DeviceUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

