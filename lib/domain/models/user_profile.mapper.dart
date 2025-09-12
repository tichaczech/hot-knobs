// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'user_profile.dart';

class UserProfileMapper extends SubClassMapperBase<UserProfile> {
  UserProfileMapper._();

  static UserProfileMapper? _instance;
  static UserProfileMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = UserProfileMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
    }
    return _instance!;
  }

  @override
  final String id = 'UserProfile';

  static String _$id(UserProfile v) => v.id;
  static const Field<UserProfile, String> _f$id = Field('id', _$id);
  static bool _$active(UserProfile v) => v.active;
  static const Field<UserProfile, bool> _f$active = Field('active', _$active);
  static DateTime _$cachedAt(UserProfile v) => v.cachedAt;
  static const Field<UserProfile, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(UserProfile v) => v.createdAt;
  static const Field<UserProfile, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(UserProfile v) => v.createdBy;
  static const Field<UserProfile, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String? _$displayName(UserProfile v) => v.displayName;
  static const Field<UserProfile, String> _f$displayName = Field(
    'displayName',
    _$displayName,
    opt: true,
  );
  static String _$email(UserProfile v) => v.email;
  static const Field<UserProfile, String> _f$email = Field('email', _$email);
  static String _$etag(UserProfile v) => v.etag;
  static const Field<UserProfile, String> _f$etag = Field('etag', _$etag);
  static String? _$phoneNumber(UserProfile v) => v.phoneNumber;
  static const Field<UserProfile, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoUrl(UserProfile v) => v.photoUrl;
  static const Field<UserProfile, String> _f$photoUrl = Field(
    'photoUrl',
    _$photoUrl,
    opt: true,
  );
  static DateTime _$updatedAt(UserProfile v) => v.updatedAt;
  static const Field<UserProfile, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(UserProfile v) => v.updatedBy;
  static const Field<UserProfile, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );

  @override
  final MappableFields<UserProfile> fields = const {
    #id: _f$id,
    #active: _f$active,
    #cachedAt: _f$cachedAt,
    #createdAt: _f$createdAt,
    #createdBy: _f$createdBy,
    #displayName: _f$displayName,
    #email: _f$email,
    #etag: _f$etag,
    #phoneNumber: _f$phoneNumber,
    #photoUrl: _f$photoUrl,
    #updatedAt: _f$updatedAt,
    #updatedBy: _f$updatedBy,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'userProfile';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static UserProfile _instantiate(DecodingData data) {
    throw MapperException.missingConstructor('UserProfile');
  }

  @override
  final Function instantiate = _instantiate;

  static UserProfile fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<UserProfile>(map);
  }

  static UserProfile fromJson(String json) {
    return ensureInitialized().decodeJson<UserProfile>(json);
  }
}

mixin UserProfileMappable {
  String toJson();
  Map<String, dynamic> toMap();
  UserProfileCopyWith<UserProfile, UserProfile, UserProfile> get copyWith;
}

abstract class UserProfileCopyWith<$R, $In extends UserProfile, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    String? displayName,
    String? email,
    String? etag,
    String? phoneNumber,
    String? photoUrl,
    DateTime? updatedAt,
    String? updatedBy,
  });
  UserProfileCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class UserProfileCreateModelMapper
    extends ClassMapperBase<UserProfileCreateModel> {
  UserProfileCreateModelMapper._();

  static UserProfileCreateModelMapper? _instance;
  static UserProfileCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = UserProfileCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'UserProfileCreateModel';

  static String? _$displayName(UserProfileCreateModel v) => v.displayName;
  static const Field<UserProfileCreateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
    opt: true,
  );
  static String _$email(UserProfileCreateModel v) => v.email;
  static const Field<UserProfileCreateModel, String> _f$email = Field(
    'email',
    _$email,
  );
  static String? _$phoneNumber(UserProfileCreateModel v) => v.phoneNumber;
  static const Field<UserProfileCreateModel, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoUrl(UserProfileCreateModel v) => v.photoUrl;
  static const Field<UserProfileCreateModel, String> _f$photoUrl = Field(
    'photoUrl',
    _$photoUrl,
    opt: true,
  );

  @override
  final MappableFields<UserProfileCreateModel> fields = const {
    #displayName: _f$displayName,
    #email: _f$email,
    #phoneNumber: _f$phoneNumber,
    #photoUrl: _f$photoUrl,
  };

  static UserProfileCreateModel _instantiate(DecodingData data) {
    return UserProfileCreateModel(
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      phoneNumber: data.dec(_f$phoneNumber),
      photoUrl: data.dec(_f$photoUrl),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static UserProfileCreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<UserProfileCreateModel>(map);
  }

  static UserProfileCreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<UserProfileCreateModel>(json);
  }
}

mixin UserProfileCreateModelMappable {
  String toJson() {
    return UserProfileCreateModelMapper.ensureInitialized()
        .encodeJson<UserProfileCreateModel>(this as UserProfileCreateModel);
  }

  Map<String, dynamic> toMap() {
    return UserProfileCreateModelMapper.ensureInitialized()
        .encodeMap<UserProfileCreateModel>(this as UserProfileCreateModel);
  }

  UserProfileCreateModelCopyWith<
    UserProfileCreateModel,
    UserProfileCreateModel,
    UserProfileCreateModel
  >
  get copyWith =>
      _UserProfileCreateModelCopyWithImpl<
        UserProfileCreateModel,
        UserProfileCreateModel
      >(this as UserProfileCreateModel, $identity, $identity);
  @override
  String toString() {
    return UserProfileCreateModelMapper.ensureInitialized().stringifyValue(
      this as UserProfileCreateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return UserProfileCreateModelMapper.ensureInitialized().equalsValue(
      this as UserProfileCreateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return UserProfileCreateModelMapper.ensureInitialized().hashValue(
      this as UserProfileCreateModel,
    );
  }
}

extension UserProfileCreateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, UserProfileCreateModel, $Out> {
  UserProfileCreateModelCopyWith<$R, UserProfileCreateModel, $Out>
  get $asUserProfileCreateModel => $base.as(
    (v, t, t2) => _UserProfileCreateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class UserProfileCreateModelCopyWith<
  $R,
  $In extends UserProfileCreateModel,
  $Out
>
    implements CreateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? displayName,
    String? email,
    String? phoneNumber,
    String? photoUrl,
  });
  UserProfileCreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _UserProfileCreateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, UserProfileCreateModel, $Out>
    implements
        UserProfileCreateModelCopyWith<$R, UserProfileCreateModel, $Out> {
  _UserProfileCreateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<UserProfileCreateModel> $mapper =
      UserProfileCreateModelMapper.ensureInitialized();
  @override
  $R call({
    Object? displayName = $none,
    String? email,
    Object? phoneNumber = $none,
    Object? photoUrl = $none,
  }) => $apply(
    FieldCopyWithData({
      if (displayName != $none) #displayName: displayName,
      if (email != null) #email: email,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoUrl != $none) #photoUrl: photoUrl,
    }),
  );
  @override
  UserProfileCreateModel $make(CopyWithData data) => UserProfileCreateModel(
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoUrl: data.get(#photoUrl, or: $value.photoUrl),
  );

  @override
  UserProfileCreateModelCopyWith<$R2, UserProfileCreateModel, $Out2>
  $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _UserProfileCreateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class UserProfileUpdateModelMapper
    extends ClassMapperBase<UserProfileUpdateModel> {
  UserProfileUpdateModelMapper._();

  static UserProfileUpdateModelMapper? _instance;
  static UserProfileUpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = UserProfileUpdateModelMapper._());
      UpdateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'UserProfileUpdateModel';

  static String? _$displayName(UserProfileUpdateModel v) => v.displayName;
  static const Field<UserProfileUpdateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
    opt: true,
  );
  static String _$email(UserProfileUpdateModel v) => v.email;
  static const Field<UserProfileUpdateModel, String> _f$email = Field(
    'email',
    _$email,
  );
  static String? _$phoneNumber(UserProfileUpdateModel v) => v.phoneNumber;
  static const Field<UserProfileUpdateModel, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoUrl(UserProfileUpdateModel v) => v.photoUrl;
  static const Field<UserProfileUpdateModel, String> _f$photoUrl = Field(
    'photoUrl',
    _$photoUrl,
    opt: true,
  );

  @override
  final MappableFields<UserProfileUpdateModel> fields = const {
    #displayName: _f$displayName,
    #email: _f$email,
    #phoneNumber: _f$phoneNumber,
    #photoUrl: _f$photoUrl,
  };

  static UserProfileUpdateModel _instantiate(DecodingData data) {
    return UserProfileUpdateModel(
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      phoneNumber: data.dec(_f$phoneNumber),
      photoUrl: data.dec(_f$photoUrl),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static UserProfileUpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<UserProfileUpdateModel>(map);
  }

  static UserProfileUpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<UserProfileUpdateModel>(json);
  }
}

mixin UserProfileUpdateModelMappable {
  String toJson() {
    return UserProfileUpdateModelMapper.ensureInitialized()
        .encodeJson<UserProfileUpdateModel>(this as UserProfileUpdateModel);
  }

  Map<String, dynamic> toMap() {
    return UserProfileUpdateModelMapper.ensureInitialized()
        .encodeMap<UserProfileUpdateModel>(this as UserProfileUpdateModel);
  }

  UserProfileUpdateModelCopyWith<
    UserProfileUpdateModel,
    UserProfileUpdateModel,
    UserProfileUpdateModel
  >
  get copyWith =>
      _UserProfileUpdateModelCopyWithImpl<
        UserProfileUpdateModel,
        UserProfileUpdateModel
      >(this as UserProfileUpdateModel, $identity, $identity);
  @override
  String toString() {
    return UserProfileUpdateModelMapper.ensureInitialized().stringifyValue(
      this as UserProfileUpdateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return UserProfileUpdateModelMapper.ensureInitialized().equalsValue(
      this as UserProfileUpdateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return UserProfileUpdateModelMapper.ensureInitialized().hashValue(
      this as UserProfileUpdateModel,
    );
  }
}

extension UserProfileUpdateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, UserProfileUpdateModel, $Out> {
  UserProfileUpdateModelCopyWith<$R, UserProfileUpdateModel, $Out>
  get $asUserProfileUpdateModel => $base.as(
    (v, t, t2) => _UserProfileUpdateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class UserProfileUpdateModelCopyWith<
  $R,
  $In extends UserProfileUpdateModel,
  $Out
>
    implements UpdateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? displayName,
    String? email,
    String? phoneNumber,
    String? photoUrl,
  });
  UserProfileUpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _UserProfileUpdateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, UserProfileUpdateModel, $Out>
    implements
        UserProfileUpdateModelCopyWith<$R, UserProfileUpdateModel, $Out> {
  _UserProfileUpdateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<UserProfileUpdateModel> $mapper =
      UserProfileUpdateModelMapper.ensureInitialized();
  @override
  $R call({
    Object? displayName = $none,
    String? email,
    Object? phoneNumber = $none,
    Object? photoUrl = $none,
  }) => $apply(
    FieldCopyWithData({
      if (displayName != $none) #displayName: displayName,
      if (email != null) #email: email,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoUrl != $none) #photoUrl: photoUrl,
    }),
  );
  @override
  UserProfileUpdateModel $make(CopyWithData data) => UserProfileUpdateModel(
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoUrl: data.get(#photoUrl, or: $value.photoUrl),
  );

  @override
  UserProfileUpdateModelCopyWith<$R2, UserProfileUpdateModel, $Out2>
  $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _UserProfileUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

