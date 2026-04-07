// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: invalid_use_of_protected_member
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'event.dart';

class EventMapper extends SubClassMapperBase<Event> {
  EventMapper._();

  static EventMapper? _instance;
  static EventMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = EventMapper._());
      EntityMapper.ensureInitialized().addSubMapper(_instance!);
    }
    return _instance!;
  }

  @override
  final String id = 'Event';

  static String _$id(Event v) => v.id;
  static const Field<Event, String> _f$id = Field('id', _$id);
  static bool _$active(Event v) => v.active;
  static const Field<Event, bool> _f$active = Field('active', _$active);
  static DateTime _$cachedAt(Event v) => v.cachedAt;
  static const Field<Event, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(Event v) => v.createdAt;
  static const Field<Event, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(Event v) => v.createdBy;
  static const Field<Event, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String _$etag(Event v) => v.etag;
  static const Field<Event, String> _f$etag = Field('etag', _$etag);
  static DateTime _$updatedAt(Event v) => v.updatedAt;
  static const Field<Event, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(Event v) => v.updatedBy;
  static const Field<Event, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );
  static String? _$description(Event v) => v.description;
  static const Field<Event, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static int? _$maxParticipants(Event v) => v.maxParticipants;
  static const Field<Event, int> _f$maxParticipants = Field(
    'maxParticipants',
    _$maxParticipants,
    opt: true,
  );
  static RegistrationType _$registrationType(Event v) => v.registrationType;
  static const Field<Event, RegistrationType> _f$registrationType = Field(
    'registrationType',
    _$registrationType,
  );
  static String _$siteId(Event v) => v.siteId;
  static const Field<Event, String> _f$siteId = Field('siteId', _$siteId);
  static DateTime _$startDate(Event v) => v.startDate;
  static const Field<Event, DateTime> _f$startDate = Field(
    'startDate',
    _$startDate,
  );
  static String _$title(Event v) => v.title;
  static const Field<Event, String> _f$title = Field('title', _$title);
  static EventVisibility _$visibility(Event v) => v.visibility;
  static const Field<Event, EventVisibility> _f$visibility = Field(
    'visibility',
    _$visibility,
  );

  @override
  final MappableFields<Event> fields = const {
    #id: _f$id,
    #active: _f$active,
    #cachedAt: _f$cachedAt,
    #createdAt: _f$createdAt,
    #createdBy: _f$createdBy,
    #etag: _f$etag,
    #updatedAt: _f$updatedAt,
    #updatedBy: _f$updatedBy,
    #description: _f$description,
    #maxParticipants: _f$maxParticipants,
    #registrationType: _f$registrationType,
    #siteId: _f$siteId,
    #startDate: _f$startDate,
    #title: _f$title,
    #visibility: _f$visibility,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'event';
  @override
  late final ClassMapperBase superMapper = EntityMapper.ensureInitialized();

  static Event _instantiate(DecodingData data) {
    return Event(
      id: data.dec(_f$id),
      active: data.dec(_f$active),
      cachedAt: data.dec(_f$cachedAt),
      createdAt: data.dec(_f$createdAt),
      createdBy: data.dec(_f$createdBy),
      etag: data.dec(_f$etag),
      updatedAt: data.dec(_f$updatedAt),
      updatedBy: data.dec(_f$updatedBy),
      description: data.dec(_f$description),
      maxParticipants: data.dec(_f$maxParticipants),
      registrationType: data.dec(_f$registrationType),
      siteId: data.dec(_f$siteId),
      startDate: data.dec(_f$startDate),
      title: data.dec(_f$title),
      visibility: data.dec(_f$visibility),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static Event fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<Event>(map);
  }

  static Event fromJson(String json) {
    return ensureInitialized().decodeJson<Event>(json);
  }
}

mixin EventMappable {
  String toJson() {
    return EventMapper.ensureInitialized().encodeJson<Event>(this as Event);
  }

  Map<String, dynamic> toMap() {
    return EventMapper.ensureInitialized().encodeMap<Event>(this as Event);
  }

  EventCopyWith<Event, Event, Event> get copyWith =>
      _EventCopyWithImpl<Event, Event>(this as Event, $identity, $identity);
  @override
  String toString() {
    return EventMapper.ensureInitialized().stringifyValue(this as Event);
  }

  @override
  bool operator ==(Object other) {
    return EventMapper.ensureInitialized().equalsValue(this as Event, other);
  }

  @override
  int get hashCode {
    return EventMapper.ensureInitialized().hashValue(this as Event);
  }
}

extension EventValueCopy<$R, $Out> on ObjectCopyWith<$R, Event, $Out> {
  EventCopyWith<$R, Event, $Out> get $asEvent =>
      $base.as((v, t, t2) => _EventCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class EventCopyWith<$R, $In extends Event, $Out>
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
    int? maxParticipants,
    RegistrationType? registrationType,
    String? siteId,
    DateTime? startDate,
    String? title,
    EventVisibility? visibility,
  });
  EventCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t);
}

class _EventCopyWithImpl<$R, $Out> extends ClassCopyWithBase<$R, Event, $Out>
    implements EventCopyWith<$R, Event, $Out> {
  _EventCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<Event> $mapper = EventMapper.ensureInitialized();
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
    Object? description = $none,
    Object? maxParticipants = $none,
    RegistrationType? registrationType,
    String? siteId,
    DateTime? startDate,
    String? title,
    EventVisibility? visibility,
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
      if (description != $none) #description: description,
      if (maxParticipants != $none) #maxParticipants: maxParticipants,
      if (registrationType != null) #registrationType: registrationType,
      if (siteId != null) #siteId: siteId,
      if (startDate != null) #startDate: startDate,
      if (title != null) #title: title,
      if (visibility != null) #visibility: visibility,
    }),
  );
  @override
  Event $make(CopyWithData data) => Event(
    id: data.get(#id, or: $value.id),
    active: data.get(#active, or: $value.active),
    cachedAt: data.get(#cachedAt, or: $value.cachedAt),
    createdAt: data.get(#createdAt, or: $value.createdAt),
    createdBy: data.get(#createdBy, or: $value.createdBy),
    etag: data.get(#etag, or: $value.etag),
    updatedAt: data.get(#updatedAt, or: $value.updatedAt),
    updatedBy: data.get(#updatedBy, or: $value.updatedBy),
    description: data.get(#description, or: $value.description),
    maxParticipants: data.get(#maxParticipants, or: $value.maxParticipants),
    registrationType: data.get(#registrationType, or: $value.registrationType),
    siteId: data.get(#siteId, or: $value.siteId),
    startDate: data.get(#startDate, or: $value.startDate),
    title: data.get(#title, or: $value.title),
    visibility: data.get(#visibility, or: $value.visibility),
  );

  @override
  EventCopyWith<$R2, Event, $Out2> $chain<$R2, $Out2>(Then<$Out2, $R2> t) =>
      _EventCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

