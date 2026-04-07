// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'types.dart';

class DayOfWeekMapper extends EnumMapper<DayOfWeek> {
  DayOfWeekMapper._();

  static DayOfWeekMapper? _instance;
  static DayOfWeekMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = DayOfWeekMapper._());
    }
    return _instance!;
  }

  static DayOfWeek fromValue(dynamic value) {
    ensureInitialized();
    return MapperContainer.globals.fromValue(value);
  }

  @override
  DayOfWeek decode(dynamic value) {
    switch (value) {
      case r'mon':
        return DayOfWeek.mon;
      case r'tue':
        return DayOfWeek.tue;
      case r'wed':
        return DayOfWeek.wed;
      case r'thu':
        return DayOfWeek.thu;
      case r'fri':
        return DayOfWeek.fri;
      case r'sat':
        return DayOfWeek.sat;
      case r'sun':
        return DayOfWeek.sun;
      default:
        throw MapperException.unknownEnumValue(value);
    }
  }

  @override
  dynamic encode(DayOfWeek self) {
    switch (self) {
      case DayOfWeek.mon:
        return r'mon';
      case DayOfWeek.tue:
        return r'tue';
      case DayOfWeek.wed:
        return r'wed';
      case DayOfWeek.thu:
        return r'thu';
      case DayOfWeek.fri:
        return r'fri';
      case DayOfWeek.sat:
        return r'sat';
      case DayOfWeek.sun:
        return r'sun';
    }
  }
}

extension DayOfWeekMapperExtension on DayOfWeek {
  String toValue() {
    DayOfWeekMapper.ensureInitialized();
    return MapperContainer.globals.toValue<DayOfWeek>(this) as String;
  }
}

class SeasonMapper extends EnumMapper<Season> {
  SeasonMapper._();

  static SeasonMapper? _instance;
  static SeasonMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SeasonMapper._());
    }
    return _instance!;
  }

  static Season fromValue(dynamic value) {
    ensureInitialized();
    return MapperContainer.globals.fromValue(value);
  }

  @override
  Season decode(dynamic value) {
    switch (value) {
      case r'spr':
        return Season.spr;
      case r'sum':
        return Season.sum;
      case r'fal':
        return Season.fal;
      case r'win':
        return Season.win;
      default:
        throw MapperException.unknownEnumValue(value);
    }
  }

  @override
  dynamic encode(Season self) {
    switch (self) {
      case Season.spr:
        return r'spr';
      case Season.sum:
        return r'sum';
      case Season.fal:
        return r'fal';
      case Season.win:
        return r'win';
    }
  }
}

extension SeasonMapperExtension on Season {
  String toValue() {
    SeasonMapper.ensureInitialized();
    return MapperContainer.globals.toValue<Season>(this) as String;
  }
}

class SiteTypeMapper extends EnumMapper<SiteType> {
  SiteTypeMapper._();

  static SiteTypeMapper? _instance;
  static SiteTypeMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SiteTypeMapper._());
    }
    return _instance!;
  }

  static SiteType fromValue(dynamic value) {
    ensureInitialized();
    return MapperContainer.globals.fromValue(value);
  }

  @override
  SiteType decode(dynamic value) {
    switch (value) {
      case r'enduro':
        return SiteType.enduro;
      case r'motocross':
        return SiteType.motocross;
      default:
        throw MapperException.unknownEnumValue(value);
    }
  }

  @override
  dynamic encode(SiteType self) {
    switch (self) {
      case SiteType.enduro:
        return r'enduro';
      case SiteType.motocross:
        return r'motocross';
    }
  }
}

extension SiteTypeMapperExtension on SiteType {
  String toValue() {
    SiteTypeMapper.ensureInitialized();
    return MapperContainer.globals.toValue<SiteType>(this) as String;
  }
}

class SkillLevelMapper extends EnumMapper<SkillLevel> {
  SkillLevelMapper._();

  static SkillLevelMapper? _instance;
  static SkillLevelMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = SkillLevelMapper._());
    }
    return _instance!;
  }

  static SkillLevel fromValue(dynamic value) {
    ensureInitialized();
    return MapperContainer.globals.fromValue(value);
  }

  @override
  SkillLevel decode(dynamic value) {
    switch (value) {
      case r'fst':
        return SkillLevel.fst;
      case r'beg':
        return SkillLevel.beg;
      case r'int':
        return SkillLevel.int;
      case r'adv':
        return SkillLevel.adv;
      case r'pro':
        return SkillLevel.pro;
      default:
        throw MapperException.unknownEnumValue(value);
    }
  }

  @override
  dynamic encode(SkillLevel self) {
    switch (self) {
      case SkillLevel.fst:
        return r'fst';
      case SkillLevel.beg:
        return r'beg';
      case SkillLevel.int:
        return r'int';
      case SkillLevel.adv:
        return r'adv';
      case SkillLevel.pro:
        return r'pro';
    }
  }
}

extension SkillLevelMapperExtension on SkillLevel {
  String toValue() {
    SkillLevelMapper.ensureInitialized();
    return MapperContainer.globals.toValue<SkillLevel>(this) as String;
  }
}

class DeviceRegistrationMapper extends ClassMapperBase<DeviceRegistration> {
  DeviceRegistrationMapper._();

  static DeviceRegistrationMapper? _instance;
  static DeviceRegistrationMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = DeviceRegistrationMapper._());
    }
    return _instance!;
  }

  @override
  final String id = 'DeviceRegistration';

  static String? _$apnsToken(DeviceRegistration v) => v.apnsToken;
  static const Field<DeviceRegistration, String> _f$apnsToken = Field(
    'apnsToken',
    _$apnsToken,
    opt: true,
  );
  static String _$fcmToken(DeviceRegistration v) => v.fcmToken;
  static const Field<DeviceRegistration, String> _f$fcmToken = Field(
    'fcmToken',
    _$fcmToken,
  );
  static String _$name(DeviceRegistration v) => v.name;
  static const Field<DeviceRegistration, String> _f$name = Field(
    'name',
    _$name,
  );
  static String _$platform(DeviceRegistration v) => v.platform;
  static const Field<DeviceRegistration, String> _f$platform = Field(
    'platform',
    _$platform,
  );

  @override
  final MappableFields<DeviceRegistration> fields = const {
    #apnsToken: _f$apnsToken,
    #fcmToken: _f$fcmToken,
    #name: _f$name,
    #platform: _f$platform,
  };

  static DeviceRegistration _instantiate(DecodingData data) {
    return DeviceRegistration(
      apnsToken: data.dec(_f$apnsToken),
      fcmToken: data.dec(_f$fcmToken),
      name: data.dec(_f$name),
      platform: data.dec(_f$platform),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static DeviceRegistration fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<DeviceRegistration>(map);
  }

  static DeviceRegistration fromJson(String json) {
    return ensureInitialized().decodeJson<DeviceRegistration>(json);
  }
}

mixin DeviceRegistrationMappable {
  String toJson() {
    return DeviceRegistrationMapper.ensureInitialized()
        .encodeJson<DeviceRegistration>(this as DeviceRegistration);
  }

  Map<String, dynamic> toMap() {
    return DeviceRegistrationMapper.ensureInitialized()
        .encodeMap<DeviceRegistration>(this as DeviceRegistration);
  }

  DeviceRegistrationCopyWith<
    DeviceRegistration,
    DeviceRegistration,
    DeviceRegistration
  >
  get copyWith =>
      _DeviceRegistrationCopyWithImpl<DeviceRegistration, DeviceRegistration>(
        this as DeviceRegistration,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return DeviceRegistrationMapper.ensureInitialized().stringifyValue(
      this as DeviceRegistration,
    );
  }

  @override
  bool operator ==(Object other) {
    return DeviceRegistrationMapper.ensureInitialized().equalsValue(
      this as DeviceRegistration,
      other,
    );
  }

  @override
  int get hashCode {
    return DeviceRegistrationMapper.ensureInitialized().hashValue(
      this as DeviceRegistration,
    );
  }
}

extension DeviceRegistrationValueCopy<$R, $Out>
    on ObjectCopyWith<$R, DeviceRegistration, $Out> {
  DeviceRegistrationCopyWith<$R, DeviceRegistration, $Out>
  get $asDeviceRegistration => $base.as(
    (v, t, t2) => _DeviceRegistrationCopyWithImpl<$R, $Out>(v, t, t2),
  );
}

abstract class DeviceRegistrationCopyWith<
  $R,
  $In extends DeviceRegistration,
  $Out
>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call({
    String? apnsToken,
    String? fcmToken,
    String? name,
    String? platform,
  });
  DeviceRegistrationCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _DeviceRegistrationCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, DeviceRegistration, $Out>
    implements DeviceRegistrationCopyWith<$R, DeviceRegistration, $Out> {
  _DeviceRegistrationCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<DeviceRegistration> $mapper =
      DeviceRegistrationMapper.ensureInitialized();
  @override
  $R call({
    Object? apnsToken = $none,
    String? fcmToken,
    String? name,
    String? platform,
  }) => $apply(
    FieldCopyWithData({
      if (apnsToken != $none) #apnsToken: apnsToken,
      if (fcmToken != null) #fcmToken: fcmToken,
      if (name != null) #name: name,
      if (platform != null) #platform: platform,
    }),
  );
  @override
  DeviceRegistration $make(CopyWithData data) => DeviceRegistration(
    apnsToken: data.get(#apnsToken, or: $value.apnsToken),
    fcmToken: data.get(#fcmToken, or: $value.fcmToken),
    name: data.get(#name, or: $value.name),
    platform: data.get(#platform, or: $value.platform),
  );

  @override
  DeviceRegistrationCopyWith<$R2, DeviceRegistration, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _DeviceRegistrationCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class LocationMapper extends ClassMapperBase<Location> {
  LocationMapper._();

  static LocationMapper? _instance;
  static LocationMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = LocationMapper._());
    }
    return _instance!;
  }

  @override
  final String id = 'Location';

  static String? _$description(Location v) => v.description;
  static const Field<Location, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static LatLng? _$coordinates(Location v) => v.coordinates;
  static const Field<Location, LatLng> _f$coordinates = Field(
    'coordinates',
    _$coordinates,
    opt: true,
  );

  @override
  final MappableFields<Location> fields = const {
    #description: _f$description,
    #coordinates: _f$coordinates,
  };

  static Location _instantiate(DecodingData data) {
    return Location(
      description: data.dec(_f$description),
      coordinates: data.dec(_f$coordinates),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Location fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Location>(map);
  }

  static Location fromJson(String json) {
    return ensureInitialized().decodeJson<Location>(json);
  }
}

mixin LocationMappable {
  String toJson() {
    return LocationMapper.ensureInitialized().encodeJson<Location>(
      this as Location,
    );
  }

  Map<String, dynamic> toMap() {
    return LocationMapper.ensureInitialized().encodeMap<Location>(
      this as Location,
    );
  }

  LocationCopyWith<Location, Location, Location> get copyWith =>
      _LocationCopyWithImpl<Location, Location>(
        this as Location,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return LocationMapper.ensureInitialized().stringifyValue(this as Location);
  }

  @override
  bool operator ==(Object other) {
    return LocationMapper.ensureInitialized().equalsValue(
      this as Location,
      other,
    );
  }

  @override
  int get hashCode {
    return LocationMapper.ensureInitialized().hashValue(this as Location);
  }
}

extension LocationValueCopy<$R, $Out> on ObjectCopyWith<$R, Location, $Out> {
  LocationCopyWith<$R, Location, $Out> get $asLocation =>
      $base.as((v, t, t2) => _LocationCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class LocationCopyWith<$R, $In extends Location, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call({String? description, LatLng? coordinates});
  LocationCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _LocationCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, Location, $Out>
    implements LocationCopyWith<$R, Location, $Out> {
  _LocationCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Location> $mapper =
      LocationMapper.ensureInitialized();
  @override
  $R call({Object? description = $none, Object? coordinates = $none}) => $apply(
    FieldCopyWithData({
      if (description != $none) #description: description,
      if (coordinates != $none) #coordinates: coordinates,
    }),
  );
  @override
  Location $make(CopyWithData data) => Location(
    description: data.get(#description, or: $value.description),
    coordinates: data.get(#coordinates, or: $value.coordinates),
  );

  @override
  LocationCopyWith<$R2, Location, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _LocationCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class OpeningHoursMapper extends ClassMapperBase<OpeningHours> {
  OpeningHoursMapper._();

  static OpeningHoursMapper? _instance;
  static OpeningHoursMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = OpeningHoursMapper._());
      OpeningHoursItemMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'OpeningHours';

  static List<OpeningHoursItem>? _$items(OpeningHours v) => v.items;
  static const Field<OpeningHours, List<OpeningHoursItem>> _f$items = Field(
    'items',
    _$items,
    opt: true,
  );
  static String? _$description(OpeningHours v) => v.description;
  static const Field<OpeningHours, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );

  @override
  final MappableFields<OpeningHours> fields = const {
    #items: _f$items,
    #description: _f$description,
  };

  static OpeningHours _instantiate(DecodingData data) {
    return OpeningHours(
      items: data.dec(_f$items),
      description: data.dec(_f$description),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static OpeningHours fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<OpeningHours>(map);
  }

  static OpeningHours fromJson(String json) {
    return ensureInitialized().decodeJson<OpeningHours>(json);
  }
}

mixin OpeningHoursMappable {
  String toJson() {
    return OpeningHoursMapper.ensureInitialized().encodeJson<OpeningHours>(
      this as OpeningHours,
    );
  }

  Map<String, dynamic> toMap() {
    return OpeningHoursMapper.ensureInitialized().encodeMap<OpeningHours>(
      this as OpeningHours,
    );
  }

  OpeningHoursCopyWith<OpeningHours, OpeningHours, OpeningHours> get copyWith =>
      _OpeningHoursCopyWithImpl<OpeningHours, OpeningHours>(
        this as OpeningHours,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return OpeningHoursMapper.ensureInitialized().stringifyValue(
      this as OpeningHours,
    );
  }

  @override
  bool operator ==(Object other) {
    return OpeningHoursMapper.ensureInitialized().equalsValue(
      this as OpeningHours,
      other,
    );
  }

  @override
  int get hashCode {
    return OpeningHoursMapper.ensureInitialized().hashValue(
      this as OpeningHours,
    );
  }
}

extension OpeningHoursValueCopy<$R, $Out>
    on ObjectCopyWith<$R, OpeningHours, $Out> {
  OpeningHoursCopyWith<$R, OpeningHours, $Out> get $asOpeningHours =>
      $base.as((v, t, t2) => _OpeningHoursCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class OpeningHoursCopyWith<$R, $In extends OpeningHours, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  ListCopyWith<
    $R,
    OpeningHoursItem,
    OpeningHoursItemCopyWith<$R, OpeningHoursItem, OpeningHoursItem>
  >?
  get items;
  $R call({List<OpeningHoursItem>? items, String? description});
  OpeningHoursCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _OpeningHoursCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, OpeningHours, $Out>
    implements OpeningHoursCopyWith<$R, OpeningHours, $Out> {
  _OpeningHoursCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<OpeningHours> $mapper =
      OpeningHoursMapper.ensureInitialized();
  @override
  ListCopyWith<
    $R,
    OpeningHoursItem,
    OpeningHoursItemCopyWith<$R, OpeningHoursItem, OpeningHoursItem>
  >?
  get items => $value.items != null
      ? ListCopyWith(
          $value.items!,
          (v, t) => v.copyWith.$chain(t),
          (v) => call(items: v),
        )
      : null;
  @override
  $R call({Object? items = $none, Object? description = $none}) => $apply(
    FieldCopyWithData({
      if (items != $none) #items: items,
      if (description != $none) #description: description,
    }),
  );
  @override
  OpeningHours $make(CopyWithData data) => OpeningHours(
    items: data.get(#items, or: $value.items),
    description: data.get(#description, or: $value.description),
  );

  @override
  OpeningHoursCopyWith<$R2, OpeningHours, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _OpeningHoursCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

class OpeningHoursItemMapper extends ClassMapperBase<OpeningHoursItem> {
  OpeningHoursItemMapper._();

  static OpeningHoursItemMapper? _instance;
  static OpeningHoursItemMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = OpeningHoursItemMapper._());
      SeasonMapper.ensureInitialized();
      DayOfWeekMapper.ensureInitialized();
    }
    return _instance!;
  }

  @override
  final String id = 'OpeningHoursItem';

  static Season? _$season(OpeningHoursItem v) => v.season;
  static const Field<OpeningHoursItem, Season> _f$season = Field(
    'season',
    _$season,
    opt: true,
  );
  static DayOfWeek _$dayOfWeek(OpeningHoursItem v) => v.dayOfWeek;
  static const Field<OpeningHoursItem, DayOfWeek> _f$dayOfWeek = Field(
    'dayOfWeek',
    _$dayOfWeek,
  );
  static int _$openingTime(OpeningHoursItem v) => v.openingTime;
  static const Field<OpeningHoursItem, int> _f$openingTime = Field(
    'openingTime',
    _$openingTime,
  );
  static int _$closingTime(OpeningHoursItem v) => v.closingTime;
  static const Field<OpeningHoursItem, int> _f$closingTime = Field(
    'closingTime',
    _$closingTime,
  );
  static String? _$description(OpeningHoursItem v) => v.description;
  static const Field<OpeningHoursItem, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );

  @override
  final MappableFields<OpeningHoursItem> fields = const {
    #season: _f$season,
    #dayOfWeek: _f$dayOfWeek,
    #openingTime: _f$openingTime,
    #closingTime: _f$closingTime,
    #description: _f$description,
  };

  static OpeningHoursItem _instantiate(DecodingData data) {
    return OpeningHoursItem(
      season: data.dec(_f$season),
      dayOfWeek: data.dec(_f$dayOfWeek),
      openingTime: data.dec(_f$openingTime),
      closingTime: data.dec(_f$closingTime),
      description: data.dec(_f$description),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static OpeningHoursItem fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<OpeningHoursItem>(map);
  }

  static OpeningHoursItem fromJson(String json) {
    return ensureInitialized().decodeJson<OpeningHoursItem>(json);
  }
}

mixin OpeningHoursItemMappable {
  String toJson() {
    return OpeningHoursItemMapper.ensureInitialized()
        .encodeJson<OpeningHoursItem>(this as OpeningHoursItem);
  }

  Map<String, dynamic> toMap() {
    return OpeningHoursItemMapper.ensureInitialized()
        .encodeMap<OpeningHoursItem>(this as OpeningHoursItem);
  }

  OpeningHoursItemCopyWith<OpeningHoursItem, OpeningHoursItem, OpeningHoursItem>
  get copyWith =>
      _OpeningHoursItemCopyWithImpl<OpeningHoursItem, OpeningHoursItem>(
        this as OpeningHoursItem,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return OpeningHoursItemMapper.ensureInitialized().stringifyValue(
      this as OpeningHoursItem,
    );
  }

  @override
  bool operator ==(Object other) {
    return OpeningHoursItemMapper.ensureInitialized().equalsValue(
      this as OpeningHoursItem,
      other,
    );
  }

  @override
  int get hashCode {
    return OpeningHoursItemMapper.ensureInitialized().hashValue(
      this as OpeningHoursItem,
    );
  }
}

extension OpeningHoursItemValueCopy<$R, $Out>
    on ObjectCopyWith<$R, OpeningHoursItem, $Out> {
  OpeningHoursItemCopyWith<$R, OpeningHoursItem, $Out>
  get $asOpeningHoursItem =>
      $base.as((v, t, t2) => _OpeningHoursItemCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class OpeningHoursItemCopyWith<$R, $In extends OpeningHoursItem, $Out>
    implements ClassCopyWith<$R, $In, $Out> {
  $R call({
    Season? season,
    DayOfWeek? dayOfWeek,
    int? openingTime,
    int? closingTime,
    String? description,
  });
  OpeningHoursItemCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _OpeningHoursItemCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, OpeningHoursItem, $Out>
    implements OpeningHoursItemCopyWith<$R, OpeningHoursItem, $Out> {
  _OpeningHoursItemCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<OpeningHoursItem> $mapper =
      OpeningHoursItemMapper.ensureInitialized();
  @override
  $R call({
    Object? season = $none,
    DayOfWeek? dayOfWeek,
    int? openingTime,
    int? closingTime,
    Object? description = $none,
  }) => $apply(
    FieldCopyWithData({
      if (season != $none) #season: season,
      if (dayOfWeek != null) #dayOfWeek: dayOfWeek,
      if (openingTime != null) #openingTime: openingTime,
      if (closingTime != null) #closingTime: closingTime,
      if (description != $none) #description: description,
    }),
  );
  @override
  OpeningHoursItem $make(CopyWithData data) => OpeningHoursItem(
    season: data.get(#season, or: $value.season),
    dayOfWeek: data.get(#dayOfWeek, or: $value.dayOfWeek),
    openingTime: data.get(#openingTime, or: $value.openingTime),
    closingTime: data.get(#closingTime, or: $value.closingTime),
    description: data.get(#description, or: $value.description),
  );

  @override
  OpeningHoursItemCopyWith<$R2, OpeningHoursItem, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _OpeningHoursItemCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

