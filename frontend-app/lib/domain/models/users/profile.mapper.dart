// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'profile.dart';

class ProfileMapper extends SubClassMapperBase<Profile> {
  ProfileMapper._();

  static ProfileMapper? _instance;
  static ProfileMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = ProfileMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
    }
    return _instance!;
  }

  @override
  final String id = 'Profile';

  static bool _$active(Profile v) => v.active;
  static const Field<Profile, bool> _f$active = Field('active', _$active);
  static String _$id(Profile v) => v.id;
  static const Field<Profile, String> _f$id = Field('id', _$id);
  static DateTime _$cachedAt(Profile v) => v.cachedAt;
  static const Field<Profile, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static String _$displayName(Profile v) => v.displayName;
  static const Field<Profile, String> _f$displayName = Field(
    'displayName',
    _$displayName,
  );
  static String _$email(Profile v) => v.email;
  static const Field<Profile, String> _f$email = Field('email', _$email);
  static String _$userId(Profile v) => v.userId;
  static const Field<Profile, String> _f$userId = Field('userId', _$userId);
  static String _$etag(Profile v) => v.etag;
  static const Field<Profile, String> _f$etag = Field('etag', _$etag);
  static String? _$phoneNumber(Profile v) => v.phoneNumber;
  static const Field<Profile, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoURL(Profile v) => v.photoURL;
  static const Field<Profile, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
    opt: true,
  );

  @override
  final MappableFields<Profile> fields = const {
    #active: _f$active,
    #id: _f$id,
    #cachedAt: _f$cachedAt,
    #displayName: _f$displayName,
    #email: _f$email,
    #userId: _f$userId,
    #etag: _f$etag,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'profile';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static Profile _instantiate(DecodingData data) {
    return Profile(
      active: data.dec(_f$active),
      id: data.dec(_f$id),
      cachedAt: data.dec(_f$cachedAt),
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      userId: data.dec(_f$userId),
      etag: data.dec(_f$etag),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Profile fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Profile>(map);
  }

  static Profile fromJson(String json) {
    return ensureInitialized().decodeJson<Profile>(json);
  }
}

mixin ProfileMappable {
  String toJson() {
    return ProfileMapper.ensureInitialized().encodeJson<Profile>(
      this as Profile,
    );
  }

  Map<String, dynamic> toMap() {
    return ProfileMapper.ensureInitialized().encodeMap<Profile>(
      this as Profile,
    );
  }

  ProfileCopyWith<Profile, Profile, Profile> get copyWith =>
      _ProfileCopyWithImpl<Profile, Profile>(
        this as Profile,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return ProfileMapper.ensureInitialized().stringifyValue(this as Profile);
  }

  @override
  bool operator ==(Object other) {
    return ProfileMapper.ensureInitialized().equalsValue(
      this as Profile,
      other,
    );
  }

  @override
  int get hashCode {
    return ProfileMapper.ensureInitialized().hashValue(this as Profile);
  }
}

extension ProfileValueCopy<$R, $Out> on ObjectCopyWith<$R, Profile, $Out> {
  ProfileCopyWith<$R, Profile, $Out> get $asProfile =>
      $base.as((v, t, t2) => _ProfileCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class ProfileCopyWith<$R, $In extends Profile, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  @override
  $R call({
    bool? active,
    String? id,
    DateTime? cachedAt,
    String? displayName,
    String? email,
    String? userId,
    String? etag,
    String? phoneNumber,
    String? photoURL,
  });
  ProfileCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _ProfileCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, Profile, $Out>
    implements ProfileCopyWith<$R, Profile, $Out> {
  _ProfileCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Profile> $mapper =
      ProfileMapper.ensureInitialized();
  @override
  $R call({
    bool? active,
    String? id,
    DateTime? cachedAt,
    String? displayName,
    String? email,
    String? userId,
    String? etag,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
  }) => $apply(
    FieldCopyWithData({
      if (active != null) #active: active,
      if (id != null) #id: id,
      if (cachedAt != null) #cachedAt: cachedAt,
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (userId != null) #userId: userId,
      if (etag != null) #etag: etag,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
    }),
  );
  @override
  Profile $make(CopyWithData data) => Profile(
    active: data.get(#active, or: $value.active),
    id: data.get(#id, or: $value.id),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    userId: data.get(#userId, or: $value.userId),
    etag: data.get(#etag, or: $value.etag),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
  );

  @override
  ProfileCopyWith<$R2, Profile, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _ProfileCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class ProfileCreateModelMapper extends ClassMapperBase<ProfileCreateModel> {
  ProfileCreateModelMapper._();

  static ProfileCreateModelMapper? _instance;
  static ProfileCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = ProfileCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'ProfileCreateModel';

  static String _$displayName(ProfileCreateModel v) => v.displayName;
  static const Field<ProfileCreateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
  );
  static String _$email(ProfileCreateModel v) => v.email;
  static const Field<ProfileCreateModel, String> _f$email = Field(
    'email',
    _$email,
  );
  static String _$userId(ProfileCreateModel v) => v.userId;
  static const Field<ProfileCreateModel, String> _f$userId = Field(
    'userId',
    _$userId,
  );
  static String? _$phoneNumber(ProfileCreateModel v) => v.phoneNumber;
  static const Field<ProfileCreateModel, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoURL(ProfileCreateModel v) => v.photoURL;
  static const Field<ProfileCreateModel, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
    opt: true,
  );

  @override
  final MappableFields<ProfileCreateModel> fields = const {
    #displayName: _f$displayName,
    #email: _f$email,
    #userId: _f$userId,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
  };

  static ProfileCreateModel _instantiate(DecodingData data) {
    return ProfileCreateModel(
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      userId: data.dec(_f$userId),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static ProfileCreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<ProfileCreateModel>(map);
  }

  static ProfileCreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<ProfileCreateModel>(json);
  }
}

mixin ProfileCreateModelMappable {
  String toJson() {
    return ProfileCreateModelMapper.ensureInitialized()
        .encodeJson<ProfileCreateModel>(this as ProfileCreateModel);
  }

  Map<String, dynamic> toMap() {
    return ProfileCreateModelMapper.ensureInitialized()
        .encodeMap<ProfileCreateModel>(this as ProfileCreateModel);
  }

  ProfileCreateModelCopyWith<
    ProfileCreateModel,
    ProfileCreateModel,
    ProfileCreateModel
  >
  get copyWith =>
      _ProfileCreateModelCopyWithImpl<ProfileCreateModel, ProfileCreateModel>(
        this as ProfileCreateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return ProfileCreateModelMapper.ensureInitialized().stringifyValue(
      this as ProfileCreateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return ProfileCreateModelMapper.ensureInitialized().equalsValue(
      this as ProfileCreateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return ProfileCreateModelMapper.ensureInitialized().hashValue(
      this as ProfileCreateModel,
    );
  }
}

extension ProfileCreateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, ProfileCreateModel, $Out> {
  ProfileCreateModelCopyWith<$R, ProfileCreateModel, $Out>
  get $asProfileCreateModel => $base.as(
    (v, t, t2) => _ProfileCreateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class ProfileCreateModelCopyWith<
  $R,
  $In extends ProfileCreateModel,
  $Out
>
    implements CreateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? displayName,
    String? email,
    String? userId,
    String? phoneNumber,
    String? photoURL,
  });
  ProfileCreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _ProfileCreateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, ProfileCreateModel, $Out>
    implements ProfileCreateModelCopyWith<$R, ProfileCreateModel, $Out> {
  _ProfileCreateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<ProfileCreateModel> $mapper =
      ProfileCreateModelMapper.ensureInitialized();
  @override
  $R call({
    String? displayName,
    String? email,
    String? userId,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
  }) => $apply(
    FieldCopyWithData({
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (userId != null) #userId: userId,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
    }),
  );
  @override
  ProfileCreateModel $make(CopyWithData data) => ProfileCreateModel(
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    userId: data.get(#userId, or: $value.userId),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
  );

  @override
  ProfileCreateModelCopyWith<$R2, ProfileCreateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _ProfileCreateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class ProfileUpdateModelMapper extends ClassMapperBase<ProfileUpdateModel> {
  ProfileUpdateModelMapper._();

  static ProfileUpdateModelMapper? _instance;
  static ProfileUpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = ProfileUpdateModelMapper._());
      UpdateModelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'ProfileUpdateModel';

  static String _$displayName(ProfileUpdateModel v) => v.displayName;
  static const Field<ProfileUpdateModel, String> _f$displayName = Field(
    'displayName',
    _$displayName,
  );
  static String _$email(ProfileUpdateModel v) => v.email;
  static const Field<ProfileUpdateModel, String> _f$email = Field(
    'email',
    _$email,
  );
  static String? _$phoneNumber(ProfileUpdateModel v) => v.phoneNumber;
  static const Field<ProfileUpdateModel, String> _f$phoneNumber = Field(
    'phoneNumber',
    _$phoneNumber,
    opt: true,
  );
  static String? _$photoURL(ProfileUpdateModel v) => v.photoURL;
  static const Field<ProfileUpdateModel, String> _f$photoURL = Field(
    'photoURL',
    _$photoURL,
    opt: true,
  );

  @override
  final MappableFields<ProfileUpdateModel> fields = const {
    #displayName: _f$displayName,
    #email: _f$email,
    #phoneNumber: _f$phoneNumber,
    #photoURL: _f$photoURL,
  };

  static ProfileUpdateModel _instantiate(DecodingData data) {
    return ProfileUpdateModel(
      displayName: data.dec(_f$displayName),
      email: data.dec(_f$email),
      phoneNumber: data.dec(_f$phoneNumber),
      photoURL: data.dec(_f$photoURL),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static ProfileUpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<ProfileUpdateModel>(map);
  }

  static ProfileUpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<ProfileUpdateModel>(json);
  }
}

mixin ProfileUpdateModelMappable {
  String toJson() {
    return ProfileUpdateModelMapper.ensureInitialized()
        .encodeJson<ProfileUpdateModel>(this as ProfileUpdateModel);
  }

  Map<String, dynamic> toMap() {
    return ProfileUpdateModelMapper.ensureInitialized()
        .encodeMap<ProfileUpdateModel>(this as ProfileUpdateModel);
  }

  ProfileUpdateModelCopyWith<
    ProfileUpdateModel,
    ProfileUpdateModel,
    ProfileUpdateModel
  >
  get copyWith =>
      _ProfileUpdateModelCopyWithImpl<ProfileUpdateModel, ProfileUpdateModel>(
        this as ProfileUpdateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return ProfileUpdateModelMapper.ensureInitialized().stringifyValue(
      this as ProfileUpdateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return ProfileUpdateModelMapper.ensureInitialized().equalsValue(
      this as ProfileUpdateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return ProfileUpdateModelMapper.ensureInitialized().hashValue(
      this as ProfileUpdateModel,
    );
  }
}

extension ProfileUpdateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, ProfileUpdateModel, $Out> {
  ProfileUpdateModelCopyWith<$R, ProfileUpdateModel, $Out>
  get $asProfileUpdateModel => $base.as(
    (v, t, t2) => _ProfileUpdateModelCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class ProfileUpdateModelCopyWith<
  $R,
  $In extends ProfileUpdateModel,
  $Out
>
    implements UpdateModelCopyWith<$R, $In, $Out> {
  @override
  $R call({
    String? displayName,
    String? email,
    String? phoneNumber,
    String? photoURL,
  });
  ProfileUpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _ProfileUpdateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, ProfileUpdateModel, $Out>
    implements ProfileUpdateModelCopyWith<$R, ProfileUpdateModel, $Out> {
  _ProfileUpdateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<ProfileUpdateModel> $mapper =
      ProfileUpdateModelMapper.ensureInitialized();
  @override
  $R call({
    String? displayName,
    String? email,
    Object? phoneNumber = $none,
    Object? photoURL = $none,
  }) => $apply(
    FieldCopyWithData({
      if (displayName != null) #displayName: displayName,
      if (email != null) #email: email,
      if (phoneNumber != $none) #phoneNumber: phoneNumber,
      if (photoURL != $none) #photoURL: photoURL,
    }),
  );
  @override
  ProfileUpdateModel $make(CopyWithData data) => ProfileUpdateModel(
    displayName: data.get(#displayName, or: $value.displayName),
    email: data.get(#email, or: $value.email),
    phoneNumber: data.get(#phoneNumber, or: $value.phoneNumber),
    photoURL: data.get(#photoURL, or: $value.photoURL),
  );

  @override
  ProfileUpdateModelCopyWith<$R2, ProfileUpdateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _ProfileUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

