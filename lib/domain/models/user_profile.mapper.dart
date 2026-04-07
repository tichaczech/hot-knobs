// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
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
      DeviceRegistrationMapper.ensureInitialized();
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
  static List<DeviceRegistration> _$deviceRegistrations(UserProfile v) =>
      v.deviceRegistrations;
  static const Field<UserProfile, List<DeviceRegistration>>
  _f$deviceRegistrations = Field(
    'deviceRegistrations',
    _$deviceRegistrations,
    opt: true,
    def: const [],
  );
  static String _$displayName(UserProfile v) => v.displayName;
  static const Field<UserProfile, String> _f$displayName = Field(
    'displayName',
    _$displayName,
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
  static String? _$photoURL(UserProfile v) => v.photoURL;
  static const Field<UserProfile, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
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
    #deviceRegistrations: _f$deviceRegistrations,
    #displayName: _f$displayName,
    #email: _f$email,
    #etag: _f$etag,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
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
    return UserProfile(
      id: data.dec(_f$id),
      active: data.dec(_f$active),
      cachedAt: data.dec(_f$cachedAt),
      createdAt: data.dec(_f$createdAt),
      createdBy: data.dec(_f$createdBy),
      deviceRegistrations: data.dec(_f$deviceRegistrations),
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      etag: data.dec(_f$etag),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
      updatedAt: data.dec(_f$updatedAt),
      updatedBy: data.dec(_f$updatedBy),
    );
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
  String toJson() {
    return UserProfileMapper.ensureInitialized().encodeJson<UserProfile>(
      this as UserProfile,
    );
  }

  Map<String, dynamic> toMap() {
    return UserProfileMapper.ensureInitialized().encodeMap<UserProfile>(
      this as UserProfile,
    );
  }

  UserProfileCopyWith<UserProfile, UserProfile, UserProfile> get copyWith =>
      _UserProfileCopyWithImpl<UserProfile, UserProfile>(
        this as UserProfile,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return UserProfileMapper.ensureInitialized().stringifyValue(
      this as UserProfile,
    );
  }

  @override
  bool operator ==(Object other) {
    return UserProfileMapper.ensureInitialized().equalsValue(
      this as UserProfile,
      other,
    );
  }

  @override
  int get hashCode {
    return UserProfileMapper.ensureInitialized().hashValue(this as UserProfile);
  }
}

extension UserProfileValueCopy<$R, $Out>
    on ObjectCopyWith<$R, UserProfile, $Out> {
  UserProfileCopyWith<$R, UserProfile, $Out> get $asUserProfile =>
      $base.as((v, t, t2) => _UserProfileCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class UserProfileCopyWith<$R, $In extends UserProfile, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  ListCopyWith<
    $R,
    DeviceRegistration,
    DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  >
  get deviceRegistrations;
  @override
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    List<DeviceRegistration>? deviceRegistrations,
    String? displayName,
    String? email,
    String? etag,
    String? phoneNumber,
    String? photoURL,
    DateTime? updatedAt,
    String? updatedBy,
  });
  UserProfileCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _UserProfileCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, UserProfile, $Out>
    implements UserProfileCopyWith<$R, UserProfile, $Out> {
  _UserProfileCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<UserProfile> $mapper =
      UserProfileMapper.ensureInitialized();
  @override
  ListCopyWith<
    $R,
    DeviceRegistration,
    DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  >
  get deviceRegistrations => ListCopyWith(
    $value.deviceRegistrations,
    (v, t) => v.copyWith.$chain(t),
    (v) => call(deviceRegistrations: v),
  );
  @override
  $R call({
    String? id,
    bool? active,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    List<DeviceRegistration>? deviceRegistrations,
    String? displayName,
    String? email,
    String? etag,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
    DateTime? updatedAt,
    String? updatedBy,
  }) => $apply(
    FieldCopyWithData({
      if (id != null) #id: id,
      if (active != null) #active: active,
      if (cachedAt != null) #cachedAt: cachedAt,
      if (createdAt != null) #createdAt: createdAt,
      if (createdBy != null) #createdBy: createdBy,
      if (deviceRegistrations != null)
        #deviceRegistrations: deviceRegistrations,
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (etag != null) #etag: etag,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
      if (updatedAt != null) #updatedAt: updatedAt,
      if (updatedBy != null) #updatedBy: updatedBy,
    }),
  );
  @override
  UserProfile $make(CopyWithData data) => UserProfile(
    id: data.get(#id, or: $value.id),
    active: data.get(#active, or: $value.active),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    createdAt: data.get(#createdAt, or: $value.createdAt),
    createdBy: data.get(#createdBy, or: $value.createdBy),
    deviceRegistrations: data.get(
      #deviceRegistrations,
      or: $value.deviceRegistrations,
    ),
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    etag: data.get(#etag, or: $value.etag),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
    updatedAt: data.get(#updatedAt, or: $value.updatedAt),
    updatedBy: data.get(#updatedBy, or: $value.updatedBy),
  );

  @override
  UserProfileCopyWith<$R2, UserProfile, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _UserProfileCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class UserProfileCreateModelMapper
    extends ClassMapperBase<UserProfileCreateModel> {
  UserProfileCreateModelMapper._();

  static UserProfileCreateModelMapper? _instance;
  static UserProfileCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = UserProfileCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
      DeviceRegistrationMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'UserProfileCreateModel';

  static DeviceRegistration _$deviceRegistration(UserProfileCreateModel v) =>
      v.deviceRegistration;
  static const Field<UserProfileCreateModel, DeviceRegistration>
  _f$deviceRegistration = Field('deviceRegistration', _$deviceRegistration);
  static String _$displayName(UserProfileCreateModel v) => v.displayName;
  static const Field<UserProfileCreateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
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
  static String? _$photoURL(UserProfileCreateModel v) => v.photoURL;
  static const Field<UserProfileCreateModel, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
    opt: true,
  );
  static String _$uid(UserProfileCreateModel v) => v.uid;
  static const Field<UserProfileCreateModel, String> _f$uid = Field(
    'uid',
    _$uid,
  );

  @override
  final MappableFields<UserProfileCreateModel> fields = const {
    #deviceRegistration: _f$deviceRegistration,
    #displayName: _f$displayName,
    #email: _f$email,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
    #uid: _f$uid,
  };

  static UserProfileCreateModel _instantiate(DecodingData data) {
    return UserProfileCreateModel(
      deviceRegistration: data.dec(_f$deviceRegistration),
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
      uid: data.dec(_f$uid),
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
  DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  get deviceRegistration;
  @override
  $R call({
    DeviceRegistration? deviceRegistration,
    String? displayName,
    String? email,
    String? phoneNumber,
    String? photoURL,
    String? uid,
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
  DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  get deviceRegistration => $value.deviceRegistration.copyWith.$chain(
    (v) => call(deviceRegistration: v),
  );
  @override
  $R call({
    DeviceRegistration? deviceRegistration,
    String? displayName,
    String? email,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
    String? uid,
  }) => $apply(
    FieldCopyWithData({
      if (deviceRegistration != null) #deviceRegistration: deviceRegistration,
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
      if (uid != null) #uid: uid,
    }),
  );
  @override
  UserProfileCreateModel $make(CopyWithData data) => UserProfileCreateModel(
    deviceRegistration: data.get(
      #deviceRegistration,
      or: $value.deviceRegistration,
    ),
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
    uid: data.get(#uid, or: $value.uid),
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
      DeviceRegistrationMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'UserProfileUpdateModel';

  static DeviceRegistration _$deviceRegistration(UserProfileUpdateModel v) =>
      v.deviceRegistration;
  static const Field<UserProfileUpdateModel, DeviceRegistration>
  _f$deviceRegistration = Field('deviceRegistration', _$deviceRegistration);
  static String _$displayName(UserProfileUpdateModel v) => v.displayName;
  static const Field<UserProfileUpdateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
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
  static String? _$photoURL(UserProfileUpdateModel v) => v.photoURL;
  static const Field<UserProfileUpdateModel, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
    opt: true,
  );

  @override
  final MappableFields<UserProfileUpdateModel> fields = const {
    #deviceRegistration: _f$deviceRegistration,
    #displayName: _f$displayName,
    #email: _f$email,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
  };

  static UserProfileUpdateModel _instantiate(DecodingData data) {
    return UserProfileUpdateModel(
      deviceRegistration: data.dec(_f$deviceRegistration),
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
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
  DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  get deviceRegistration;
  @override
  $R call({
    DeviceRegistration? deviceRegistration,
    String? displayName,
    String? email,
    String? phoneNumber,
    String? photoURL,
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
  DeviceRegistrationCopyWith<$R, DeviceRegistration, DeviceRegistration>
  get deviceRegistration => $value.deviceRegistration.copyWith.$chain(
    (v) => call(deviceRegistration: v),
  );
  @override
  $R call({
    DeviceRegistration? deviceRegistration,
    String? displayName,
    String? email,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
  }) => $apply(
    FieldCopyWithData({
      if (deviceRegistration != null) #deviceRegistration: deviceRegistration,
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
    }),
  );
  @override
  UserProfileUpdateModel $make(CopyWithData data) => UserProfileUpdateModel(
    deviceRegistration: data.get(
      #deviceRegistration,
      or: $value.deviceRegistration,
    ),
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
  );

  @override
  UserProfileUpdateModelCopyWith<$R2, UserProfileUpdateModel, $Out2>
  $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _UserProfileUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

