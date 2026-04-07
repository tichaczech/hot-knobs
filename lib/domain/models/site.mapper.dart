// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'site.dart';

class SiteMapper extends SubClassMapperBase<Site> {
  SiteMapper._();

  static SiteMapper? _instance;
  static SiteMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SiteMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
      MapperContainer.globals.useAll([LatLngDecoderOnlyMapper()]);
      LocationMapper.ensureInitialized();
      OpeningHoursMapper.ensureInitialized();
      SiteTypeMapper.ensureInitialized();
      SkillLevelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'Site';

  static String _$id(Site v) => v.id;
  static const Field<Site, String> _f$id = Field('id', _$id);
  static bool _$active(Site v) => v.active;
  static const Field<Site, bool> _f$active = Field('active', _$active);
  static String? _$address(Site v) => v.address;
  static const Field<Site, String> _f$address = Field(
    'address',
    _$address,
    opt: true,
  );
  static Location? _$arrival(Site v) => v.arrival;
  static const Field<Site, Location> _f$arrival = Field(
    'arrival',
    _$arrival,
    opt: true,
  );
  static DateTime _$cachedAt(Site v) => v.cachedAt;
  static const Field<Site, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(Site v) => v.createdAt;
  static const Field<Site, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(Site v) => v.createdBy;
  static const Field<Site, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String? _$description(Site v) => v.description;
  static const Field<Site, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static String _$etag(Site v) => v.etag;
  static const Field<Site, String> _f$etag = Field('etag', _$etag);
  static List<String>? _$images(Site v) => v.images;
  static const Field<Site, List<String>> _f$images = Field(
    'images',
    _$images,
    opt: true,
  );
  static int? _$length(Site v) => v.length;
  static const Field<Site, int> _f$length = Field(
    'length',
    _$length,
    opt: true,
  );
  static Location _$location(Site v) => v.location;
  static const Field<Site, Location> _f$location = Field(
    'location',
    _$location,
  );
  static OpeningHours? _$openingHours(Site v) => v.openingHours;
  static const Field<Site, OpeningHours> _f$openingHours = Field(
    'openingHours',
    _$openingHours,
    opt: true,
  );
  static Location? _$parking(Site v) => v.parking;
  static const Field<Site, Location> _f$parking = Field(
    'parking',
    _$parking,
    opt: true,
  );
  static SiteType _$siteType(Site v) => v.siteType;
  static const Field<Site, SiteType> _f$siteType = Field(
    'siteType',
    _$siteType,
  );
  static SkillLevel _$skillLevel(Site v) => v.skillLevel;
  static const Field<Site, SkillLevel> _f$skillLevel = Field(
    'skillLevel',
    _$skillLevel,
  );
  static DateTime _$updatedAt(Site v) => v.updatedAt;
  static const Field<Site, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(Site v) => v.updatedBy;
  static const Field<Site, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );
  static String _$name(Site v) => v.name;
  static const Field<Site, String> _f$name = Field('name', _$name);

  @override
  final MappableFields<Site> fields = const {
    #id: _f$id,
    #active: _f$active,
    #address: _f$address,
    #arrival: _f$arrival,
    #cachedAt: _f$cachedAt,
    #createdAt: _f$createdAt,
    #createdBy: _f$createdBy,
    #description: _f$description,
    #etag: _f$etag,
    #images: _f$images,
    #length: _f$length,
    #location: _f$location,
    #openingHours: _f$openingHours,
    #parking: _f$parking,
    #siteType: _f$siteType,
    #skillLevel: _f$skillLevel,
    #updatedAt: _f$updatedAt,
    #updatedBy: _f$updatedBy,
    #name: _f$name,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'site';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static Site _instantiate(DecodingData data) {
    return Site(
      id: data.dec(_f$id),
      active: data.dec(_f$active),
      address: data.dec(_f$address),
      arrival: data.dec(_f$arrival),
      cachedAt: data.dec(_f$cachedAt),
      createdAt: data.dec(_f$createdAt),
      createdBy: data.dec(_f$createdBy),
      description: data.dec(_f$description),
      etag: data.dec(_f$etag),
      images: data.dec(_f$images),
      length: data.dec(_f$length),
      location: data.dec(_f$location),
      openingHours: data.dec(_f$openingHours),
      parking: data.dec(_f$parking),
      siteType: data.dec(_f$siteType),
      skillLevel: data.dec(_f$skillLevel),
      updatedAt: data.dec(_f$updatedAt),
      updatedBy: data.dec(_f$updatedBy),
      name: data.dec(_f$name),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Site fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Site>(map);
  }

  static Site fromJson(String json) {
    return ensureInitialized().decodeJson<Site>(json);
  }
}

mixin SiteMappable {
  String toJson() {
    return SiteMapper.ensureInitialized().encodeJson<Site>(this as Site);
  }

  Map<String, dynamic> toMap() {
    return SiteMapper.ensureInitialized().encodeMap<Site>(this as Site);
  }

  SiteCopyWith<Site, Site, Site> get copyWith =>
      _SiteCopyWithImpl<Site, Site>(this as Site, $identity, $identity);
  @override
  String toString() {
    return SiteMapper.ensureInitialized().stringifyValue(this as Site);
  }

  @override
  bool operator ==(Object other) {
    return SiteMapper.ensureInitialized().equalsValue(this as Site, other);
  }

  @override
  int get hashCode {
    return SiteMapper.ensureInitialized().hashValue(this as Site);
  }
}

extension SiteValueCopy<$R, $Out> on ObjectCopyWith<$R, Site, $Out> {
  SiteCopyWith<$R, Site, $Out> get $asSite =>
      $base.as((v, t, t2) => _SiteCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class SiteCopyWith<$R, $In extends Site, $Out>
    implements EntityCopyWith<$R, $In, $Out> {
  LocationCopyWith<$R, Location, Location>? get arrival;
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>>? get images;
  LocationCopyWith<$R, Location, Location> get location;
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours;
  LocationCopyWith<$R, Location, Location>? get parking;
  @override
  $R call({
    String? id,
    bool? active,
    String? address,
    Location? arrival,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    String? description,
    String? etag,
    List<String>? images,
    int? length,
    Location? location,
    OpeningHours? openingHours,
    Location? parking,
    SiteType? siteType,
    SkillLevel? skillLevel,
    DateTime? updatedAt,
    String? updatedBy,
    String? name,
  });
  SiteCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _SiteCopyWithImpl<$R, $Out> extends ClassCopyWithBase<$R, Site, $Out>
    implements SiteCopyWith<$R, Site, $Out> {
  _SiteCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Site> $mapper = SiteMapper.ensureInitialized();
  @override
  LocationCopyWith<$R, Location, Location>? get arrival =>
      $value.arrival?.copyWith.$chain((v) => call(arrival: v));
  @override
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>>? get images =>
      $value.images != null
      ? ListCopyWith(
          $value.images!,
          (v, t) => ObjectCopyWith(v, $identity, t),
          (v) => call(images: v),
        )
      : null;
  @override
  LocationCopyWith<$R, Location, Location> get location =>
      $value.location.copyWith.$chain((v) => call(location: v));
  @override
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours =>
      $value.openingHours?.copyWith.$chain((v) => call(openingHours: v));
  @override
  LocationCopyWith<$R, Location, Location>? get parking =>
      $value.parking?.copyWith.$chain((v) => call(parking: v));
  @override
  $R call({
    String? id,
    bool? active,
    Object? address = $none,
    Object? arrival = $none,
    DateTime? cachedAt,
    DateTime? createdAt,
    String? createdBy,
    Object? description = $none,
    String? etag,
    Object? images = $none,
    Object? length = $none,
    Location? location,
    Object? openingHours = $none,
    Object? parking = $none,
    SiteType? siteType,
    SkillLevel? skillLevel,
    DateTime? updatedAt,
    String? updatedBy,
    String? name,
  }) => $apply(
    FieldCopyWithData({
      if (id != null) #id: id,
      if (active != null) #active: active,
      if (address != $none) #address: address,
      if (arrival != $none) #arrival: arrival,
      if (cachedAt != null) #cachedAt: cachedAt,
      if (createdAt != null) #createdAt: createdAt,
      if (createdBy != null) #createdBy: createdBy,
      if (description != $none) #description: description,
      if (etag != null) #etag: etag,
      if (images != $none) #images: images,
      if (length != $none) #length: length,
      if (location != null) #location: location,
      if (openingHours != $none) #openingHours: openingHours,
      if (parking != $none) #parking: parking,
      if (siteType != null) #siteType: siteType,
      if (skillLevel != null) #skillLevel: skillLevel,
      if (updatedAt != null) #updatedAt: updatedAt,
      if (updatedBy != null) #updatedBy: updatedBy,
      if (name != null) #name: name,
    }),
  );
  @override
  Site $make(CopyWithData data) => Site(
    id: data.get(#id, or: $value.id),
    active: data.get(#active, or: $value.active),
    address: data.get(#address, or: $value.address),
    arrival: data.get(#arrival, or: $value.arrival),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    createdAt: data.get(#createdAt, or: $value.createdAt),
    createdBy: data.get(#createdBy, or: $value.createdBy),
    description: data.get(#description, or: $value.description),
    etag: data.get(#etag, or: $value.etag),
    images: data.get(#images, or: $value.images),
    length: data.get(#length, or: $value.length),
    location: data.get(#location, or: $value.location),
    openingHours: data.get(#openingHours, or: $value.openingHours),
    parking: data.get(#parking, or: $value.parking),
    siteType: data.get(#siteType, or: $value.siteType),
    skillLevel: data.get(#skillLevel, or: $value.skillLevel),
    updatedAt: data.get(#updatedAt, or: $value.updatedAt),
    updatedBy: data.get(#updatedBy, or: $value.updatedBy),
    name: data.get(#name, or: $value.name),
  );

  @override
  SiteCopyWith<$R2, Site, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _SiteCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class SiteCreateModelMapper extends ClassMapperBase<SiteCreateModel> {
  SiteCreateModelMapper._();

  static SiteCreateModelMapper? _instance;
  static SiteCreateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SiteCreateModelMapper._());
      CreateModelMapper.ensureInitialized();
      LocationMapper.ensureInitialized();
      OpeningHoursMapper.ensureInitialized();
      SiteTypeMapper.ensureInitialized();
      SkillLevelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'SiteCreateModel';

  static String? _$address(SiteCreateModel v) => v.address;
  static const Field<SiteCreateModel, String> _f$address = Field(
    'address',
    _$address,
    opt: true,
  );
  static Location? _$arrival(SiteCreateModel v) => v.arrival;
  static const Field<SiteCreateModel, Location> _f$arrival = Field(
    'arrival',
    _$arrival,
    opt: true,
  );
  static String _$description(SiteCreateModel v) => v.description;
  static const Field<SiteCreateModel, String> _f$description = Field(
    'description',
    _$description,
  );
  static int? _$length(SiteCreateModel v) => v.length;
  static const Field<SiteCreateModel, int> _f$length = Field(
    'length',
    _$length,
    opt: true,
  );
  static Location? _$location(SiteCreateModel v) => v.location;
  static const Field<SiteCreateModel, Location> _f$location = Field(
    'location',
    _$location,
    opt: true,
  );
  static String _$name(SiteCreateModel v) => v.name;
  static const Field<SiteCreateModel, String> _f$name = Field('name', _$name);
  static OpeningHours? _$openingHours(SiteCreateModel v) => v.openingHours;
  static const Field<SiteCreateModel, OpeningHours> _f$openingHours = Field(
    'openingHours',
    _$openingHours,
    opt: true,
  );
  static Location? _$parking(SiteCreateModel v) => v.parking;
  static const Field<SiteCreateModel, Location> _f$parking = Field(
    'parking',
    _$parking,
    opt: true,
  );
  static SiteType? _$siteType(SiteCreateModel v) => v.siteType;
  static const Field<SiteCreateModel, SiteType> _f$siteType = Field(
    'siteType',
    _$siteType,
    opt: true,
  );
  static SkillLevel? _$skillLevel(SiteCreateModel v) => v.skillLevel;
  static const Field<SiteCreateModel, SkillLevel> _f$skillLevel = Field(
    'skillLevel',
    _$skillLevel,
    opt: true,
  );

  @override
  final MappableFields<SiteCreateModel> fields = const {
    #address: _f$address,
    #arrival: _f$arrival,
    #description: _f$description,
    #length: _f$length,
    #location: _f$location,
    #name: _f$name,
    #openingHours: _f$openingHours,
    #parking: _f$parking,
    #siteType: _f$siteType,
    #skillLevel: _f$skillLevel,
  };

  static SiteCreateModel _instantiate(DecodingData data) {
    return SiteCreateModel(
      address: data.dec(_f$address),
      arrival: data.dec(_f$arrival),
      description: data.dec(_f$description),
      length: data.dec(_f$length),
      location: data.dec(_f$location),
      name: data.dec(_f$name),
      openingHours: data.dec(_f$openingHours),
      parking: data.dec(_f$parking),
      siteType: data.dec(_f$siteType),
      skillLevel: data.dec(_f$skillLevel),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static SiteCreateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<SiteCreateModel>(map);
  }

  static SiteCreateModel fromJson(String json) {
    return ensureInitialized().decodeJson<SiteCreateModel>(json);
  }
}

mixin SiteCreateModelMappable {
  String toJson() {
    return SiteCreateModelMapper.ensureInitialized()
        .encodeJson<SiteCreateModel>(this as SiteCreateModel);
  }

  Map<String, dynamic> toMap() {
    return SiteCreateModelMapper.ensureInitialized().encodeMap<SiteCreateModel>(
      this as SiteCreateModel,
    );
  }

  SiteCreateModelCopyWith<SiteCreateModel, SiteCreateModel, SiteCreateModel>
  get copyWith =>
      _SiteCreateModelCopyWithImpl<SiteCreateModel, SiteCreateModel>(
        this as SiteCreateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return SiteCreateModelMapper.ensureInitialized().stringifyValue(
      this as SiteCreateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return SiteCreateModelMapper.ensureInitialized().equalsValue(
      this as SiteCreateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return SiteCreateModelMapper.ensureInitialized().hashValue(
      this as SiteCreateModel,
    );
  }
}

extension SiteCreateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, SiteCreateModel, $Out> {
  SiteCreateModelCopyWith<$R, SiteCreateModel, $Out> get $asSiteCreateModel =>
      $base.as((v, t, t2) => _SiteCreateModelCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class SiteCreateModelCopyWith<$R, $In extends SiteCreateModel, $Out>
    implements CreateModelCopyWith<$R, $In, $Out> {
  LocationCopyWith<$R, Location, Location>? get arrival;
  LocationCopyWith<$R, Location, Location>? get location;
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours;
  LocationCopyWith<$R, Location, Location>? get parking;
  @override
  $R call({
    String? address,
    Location? arrival,
    String? description,
    int? length,
    Location? location,
    String? name,
    OpeningHours? openingHours,
    Location? parking,
    SiteType? siteType,
    SkillLevel? skillLevel,
  });
  SiteCreateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _SiteCreateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, SiteCreateModel, $Out>
    implements SiteCreateModelCopyWith<$R, SiteCreateModel, $Out> {
  _SiteCreateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<SiteCreateModel> $mapper =
      SiteCreateModelMapper.ensureInitialized();
  @override
  LocationCopyWith<$R, Location, Location>? get arrival =>
      $value.arrival?.copyWith.$chain((v) => call(arrival: v));
  @override
  LocationCopyWith<$R, Location, Location>? get location =>
      $value.location?.copyWith.$chain((v) => call(location: v));
  @override
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours =>
      $value.openingHours?.copyWith.$chain((v) => call(openingHours: v));
  @override
  LocationCopyWith<$R, Location, Location>? get parking =>
      $value.parking?.copyWith.$chain((v) => call(parking: v));
  @override
  $R call({
    Object? address = $none,
    Object? arrival = $none,
    String? description,
    Object? length = $none,
    Object? location = $none,
    String? name,
    Object? openingHours = $none,
    Object? parking = $none,
    Object? siteType = $none,
    Object? skillLevel = $none,
  }) => $apply(
    FieldCopyWithData({
      if (address != $none) #address: address,
      if (arrival != $none) #arrival: arrival,
      if (description != null) #description: description,
      if (length != $none) #length: length,
      if (location != $none) #location: location,
      if (name != null) #name: name,
      if (openingHours != $none) #openingHours: openingHours,
      if (parking != $none) #parking: parking,
      if (siteType != $none) #siteType: siteType,
      if (skillLevel != $none) #skillLevel: skillLevel,
    }),
  );
  @override
  SiteCreateModel $make(CopyWithData data) => SiteCreateModel(
    address: data.get(#address, or: $value.address),
    arrival: data.get(#arrival, or: $value.arrival),
    description: data.get(#description, or: $value.description),
    length: data.get(#length, or: $value.length),
    location: data.get(#location, or: $value.location),
    name: data.get(#name, or: $value.name),
    openingHours: data.get(#openingHours, or: $value.openingHours),
    parking: data.get(#parking, or: $value.parking),
    siteType: data.get(#siteType, or: $value.siteType),
    skillLevel: data.get(#skillLevel, or: $value.skillLevel),
  );

  @override
  SiteCreateModelCopyWith<$R2, SiteCreateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _SiteCreateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class SiteUpdateModelMapper extends ClassMapperBase<SiteUpdateModel> {
  SiteUpdateModelMapper._();

  static SiteUpdateModelMapper? _instance;
  static SiteUpdateModelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SiteUpdateModelMapper._());
      UpdateModelMapper.ensureInitialized();
      LocationMapper.ensureInitialized();
      OpeningHoursMapper.ensureInitialized();
      SiteTypeMapper.ensureInitialized();
      SkillLevelMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'SiteUpdateModel';

  static String? _$address(SiteUpdateModel v) => v.address;
  static const Field<SiteUpdateModel, String> _f$address = Field(
    'address',
    _$address,
    opt: true,
  );
  static Location? _$arrival(SiteUpdateModel v) => v.arrival;
  static const Field<SiteUpdateModel, Location> _f$arrival = Field(
    'arrival',
    _$arrival,
    opt: true,
  );
  static String _$description(SiteUpdateModel v) => v.description;
  static const Field<SiteUpdateModel, String> _f$description = Field(
    'description',
    _$description,
  );
  static int? _$length(SiteUpdateModel v) => v.length;
  static const Field<SiteUpdateModel, int> _f$length = Field(
    'length',
    _$length,
    opt: true,
  );
  static Location? _$location(SiteUpdateModel v) => v.location;
  static const Field<SiteUpdateModel, Location> _f$location = Field(
    'location',
    _$location,
    opt: true,
  );
  static String _$name(SiteUpdateModel v) => v.name;
  static const Field<SiteUpdateModel, String> _f$name = Field('name', _$name);
  static OpeningHours? _$openingHours(SiteUpdateModel v) => v.openingHours;
  static const Field<SiteUpdateModel, OpeningHours> _f$openingHours = Field(
    'openingHours',
    _$openingHours,
    opt: true,
  );
  static Location? _$parking(SiteUpdateModel v) => v.parking;
  static const Field<SiteUpdateModel, Location> _f$parking = Field(
    'parking',
    _$parking,
    opt: true,
  );
  static SiteType? _$siteType(SiteUpdateModel v) => v.siteType;
  static const Field<SiteUpdateModel, SiteType> _f$siteType = Field(
    'siteType',
    _$siteType,
    opt: true,
  );
  static SkillLevel? _$skillLevel(SiteUpdateModel v) => v.skillLevel;
  static const Field<SiteUpdateModel, SkillLevel> _f$skillLevel = Field(
    'skillLevel',
    _$skillLevel,
    opt: true,
  );

  @override
  final MappableFields<SiteUpdateModel> fields = const {
    #address: _f$address,
    #arrival: _f$arrival,
    #description: _f$description,
    #length: _f$length,
    #location: _f$location,
    #name: _f$name,
    #openingHours: _f$openingHours,
    #parking: _f$parking,
    #siteType: _f$siteType,
    #skillLevel: _f$skillLevel,
  };

  static SiteUpdateModel _instantiate(DecodingData data) {
    return SiteUpdateModel(
      address: data.dec(_f$address),
      arrival: data.dec(_f$arrival),
      description: data.dec(_f$description),
      length: data.dec(_f$length),
      location: data.dec(_f$location),
      name: data.dec(_f$name),
      openingHours: data.dec(_f$openingHours),
      parking: data.dec(_f$parking),
      siteType: data.dec(_f$siteType),
      skillLevel: data.dec(_f$skillLevel),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static SiteUpdateModel fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<SiteUpdateModel>(map);
  }

  static SiteUpdateModel fromJson(String json) {
    return ensureInitialized().decodeJson<SiteUpdateModel>(json);
  }
}

mixin SiteUpdateModelMappable {
  String toJson() {
    return SiteUpdateModelMapper.ensureInitialized()
        .encodeJson<SiteUpdateModel>(this as SiteUpdateModel);
  }

  Map<String, dynamic> toMap() {
    return SiteUpdateModelMapper.ensureInitialized().encodeMap<SiteUpdateModel>(
      this as SiteUpdateModel,
    );
  }

  SiteUpdateModelCopyWith<SiteUpdateModel, SiteUpdateModel, SiteUpdateModel>
  get copyWith =>
      _SiteUpdateModelCopyWithImpl<SiteUpdateModel, SiteUpdateModel>(
        this as SiteUpdateModel,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return SiteUpdateModelMapper.ensureInitialized().stringifyValue(
      this as SiteUpdateModel,
    );
  }

  @override
  bool operator ==(Object other) {
    return SiteUpdateModelMapper.ensureInitialized().equalsValue(
      this as SiteUpdateModel,
      other,
    );
  }

  @override
  int get hashCode {
    return SiteUpdateModelMapper.ensureInitialized().hashValue(
      this as SiteUpdateModel,
    );
  }
}

extension SiteUpdateModelValueCopy<$R, $Out>
    on ObjectCopyWith<$R, SiteUpdateModel, $Out> {
  SiteUpdateModelCopyWith<$R, SiteUpdateModel, $Out> get $asSiteUpdateModel =>
      $base.as((v, t, t2) => _SiteUpdateModelCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class SiteUpdateModelCopyWith<$R, $In extends SiteUpdateModel, $Out>
    implements UpdateModelCopyWith<$R, $In, $Out> {
  LocationCopyWith<$R, Location, Location>? get arrival;
  LocationCopyWith<$R, Location, Location>? get location;
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours;
  LocationCopyWith<$R, Location, Location>? get parking;
  @override
  $R call({
    String? address,
    Location? arrival,
    String? description,
    int? length,
    Location? location,
    String? name,
    OpeningHours? openingHours,
    Location? parking,
    SiteType? siteType,
    SkillLevel? skillLevel,
  });
  SiteUpdateModelCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _SiteUpdateModelCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, SiteUpdateModel, $Out>
    implements SiteUpdateModelCopyWith<$R, SiteUpdateModel, $Out> {
  _SiteUpdateModelCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<SiteUpdateModel> $mapper =
      SiteUpdateModelMapper.ensureInitialized();
  @override
  LocationCopyWith<$R, Location, Location>? get arrival =>
      $value.arrival?.copyWith.$chain((v) => call(arrival: v));
  @override
  LocationCopyWith<$R, Location, Location>? get location =>
      $value.location?.copyWith.$chain((v) => call(location: v));
  @override
  OpeningHoursCopyWith<$R, OpeningHours, OpeningHours>? get openingHours =>
      $value.openingHours?.copyWith.$chain((v) => call(openingHours: v));
  @override
  LocationCopyWith<$R, Location, Location>? get parking =>
      $value.parking?.copyWith.$chain((v) => call(parking: v));
  @override
  $R call({
    Object? address = $none,
    Object? arrival = $none,
    String? description,
    Object? length = $none,
    Object? location = $none,
    String? name,
    Object? openingHours = $none,
    Object? parking = $none,
    Object? siteType = $none,
    Object? skillLevel = $none,
  }) => $apply(
    FieldCopyWithData({
      if (address != $none) #address: address,
      if (arrival != $none) #arrival: arrival,
      if (description != null) #description: description,
      if (length != $none) #length: length,
      if (location != $none) #location: location,
      if (name != null) #name: name,
      if (openingHours != $none) #openingHours: openingHours,
      if (parking != $none) #parking: parking,
      if (siteType != $none) #siteType: siteType,
      if (skillLevel != $none) #skillLevel: skillLevel,
    }),
  );
  @override
  SiteUpdateModel $make(CopyWithData data) => SiteUpdateModel(
    address: data.get(#address, or: $value.address),
    arrival: data.get(#arrival, or: $value.arrival),
    description: data.get(#description, or: $value.description),
    length: data.get(#length, or: $value.length),
    location: data.get(#location, or: $value.location),
    name: data.get(#name, or: $value.name),
    openingHours: data.get(#openingHours, or: $value.openingHours),
    parking: data.get(#parking, or: $value.parking),
    siteType: data.get(#siteType, or: $value.siteType),
    skillLevel: data.get(#skillLevel, or: $value.skillLevel),
  );

  @override
  SiteUpdateModelCopyWith<$R2, SiteUpdateModel, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _SiteUpdateModelCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

