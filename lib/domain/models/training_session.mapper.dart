// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
// ignore_for_file: unused_element, unnecessary_cast, override_on_non_overriding_member
// ignore_for_file: strict_raw_type, inference_failure_on_untyped_parameter

part of 'training_session.dart';

class TrainingSessionMapper extends SubClassMapperBase<TrainingSession> {
  TrainingSessionMapper._();

  static TrainingSessionMapper? _instance;
  static TrainingSessionMapper ensureInitialized() {
    if (_instance == null) {
      MapperContainer.globals.use(_instance = TrainingSessionMapper._());
      EventMapper.ensureInitialized().addSubMapper(_instance!);
    }
    return _instance!;
  }

  @override
  final String id = 'TrainingSession';

  static String _$id(TrainingSession v) => v.id;
  static const Field<TrainingSession, String> _f$id = Field('id', _$id);
  static bool _$active(TrainingSession v) => v.active;
  static const Field<TrainingSession, bool> _f$active = Field(
    'active',
    _$active,
  );
  static DateTime _$cachedAt(TrainingSession v) => v.cachedAt;
  static const Field<TrainingSession, DateTime> _f$cachedAt = Field(
    'cachedAt',
    _$cachedAt,
  );
  static DateTime _$createdAt(TrainingSession v) => v.createdAt;
  static const Field<TrainingSession, DateTime> _f$createdAt = Field(
    'createdAt',
    _$createdAt,
  );
  static String _$createdBy(TrainingSession v) => v.createdBy;
  static const Field<TrainingSession, String> _f$createdBy = Field(
    'createdBy',
    _$createdBy,
  );
  static String _$etag(TrainingSession v) => v.etag;
  static const Field<TrainingSession, String> _f$etag = Field('etag', _$etag);
  static DateTime _$updatedAt(TrainingSession v) => v.updatedAt;
  static const Field<TrainingSession, DateTime> _f$updatedAt = Field(
    'updatedAt',
    _$updatedAt,
  );
  static String _$updatedBy(TrainingSession v) => v.updatedBy;
  static const Field<TrainingSession, String> _f$updatedBy = Field(
    'updatedBy',
    _$updatedBy,
  );
  static String? _$description(TrainingSession v) => v.description;
  static const Field<TrainingSession, String> _f$description = Field(
    'description',
    _$description,
    opt: true,
  );
  static int? _$maxParticipants(TrainingSession v) => v.maxParticipants;
  static const Field<TrainingSession, int> _f$maxParticipants = Field(
    'maxParticipants',
    _$maxParticipants,
    opt: true,
  );
  static RegistrationType _$registrationType(TrainingSession v) =>
      v.registrationType;
  static const Field<TrainingSession, RegistrationType> _f$registrationType =
      Field('registrationType', _$registrationType);
  static String _$siteId(TrainingSession v) => v.siteId;
  static const Field<TrainingSession, String> _f$siteId = Field(
    'siteId',
    _$siteId,
  );
  static DateTime _$startDate(TrainingSession v) => v.startDate;
  static const Field<TrainingSession, DateTime> _f$startDate = Field(
    'startDate',
    _$startDate,
  );
  static String _$title(TrainingSession v) => v.title;
  static const Field<TrainingSession, String> _f$title = Field(
    'title',
    _$title,
  );
  static EventVisibility _$visibility(TrainingSession v) => v.visibility;
  static const Field<TrainingSession, EventVisibility> _f$visibility = Field(
    'visibility',
    _$visibility,
  );
  static List<String> _$categories(TrainingSession v) => v.categories;
  static const Field<TrainingSession, List<String>> _f$categories = Field(
    'categories',
    _$categories,
  );
  static String _$leadTrainer(TrainingSession v) => v.leadTrainer;
  static const Field<TrainingSession, String> _f$leadTrainer = Field(
    'leadTrainer',
    _$leadTrainer,
  );
  static List<String> _$skillLevels(TrainingSession v) => v.skillLevels;
  static const Field<TrainingSession, List<String>> _f$skillLevels = Field(
    'skillLevels',
    _$skillLevels,
  );
  static List<String> _$trainers(TrainingSession v) => v.trainers;
  static const Field<TrainingSession, List<String>> _f$trainers = Field(
    'trainers',
    _$trainers,
    opt: true,
    def: const [],
  );

  @override
  final MappableFields<TrainingSession> fields = const {
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
    #categories: _f$categories,
    #leadTrainer: _f$leadTrainer,
    #skillLevels: _f$skillLevels,
    #trainers: _f$trainers,
  };

  @override
  final String discriminatorKey = '__discriminator';
  @override
  final dynamic discriminatorValue = 'event';
  @override
  late final ClassMapperBase superMapper = EventMapper.ensureInitialized();

  static TrainingSession _instantiate(DecodingData data) {
    return TrainingSession(
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
      categories: data.dec(_f$categories),
      leadTrainer: data.dec(_f$leadTrainer),
      skillLevels: data.dec(_f$skillLevels),
      trainers: data.dec(_f$trainers),
    );
  }

  @override
  final Function instantiate = _instantiate;

  static TrainingSession fromMap(Map<String, dynamic> map) {
    return ensureInitialized().decodeMap<TrainingSession>(map);
  }

  static TrainingSession fromJson(String json) {
    return ensureInitialized().decodeJson<TrainingSession>(json);
  }
}

mixin TrainingSessionMappable {
  String toJson() {
    return TrainingSessionMapper.ensureInitialized()
        .encodeJson<TrainingSession>(this as TrainingSession);
  }

  Map<String, dynamic> toMap() {
    return TrainingSessionMapper.ensureInitialized().encodeMap<TrainingSession>(
      this as TrainingSession,
    );
  }

  TrainingSessionCopyWith<TrainingSession, TrainingSession, TrainingSession>
  get copyWith =>
      _TrainingSessionCopyWithImpl<TrainingSession, TrainingSession>(
        this as TrainingSession,
        $identity,
        $identity,
      );
  @override
  String toString() {
    return TrainingSessionMapper.ensureInitialized().stringifyValue(
      this as TrainingSession,
    );
  }

  @override
  bool operator ==(Object other) {
    return TrainingSessionMapper.ensureInitialized().equalsValue(
      this as TrainingSession,
      other,
    );
  }

  @override
  int get hashCode {
    return TrainingSessionMapper.ensureInitialized().hashValue(
      this as TrainingSession,
    );
  }
}

extension TrainingSessionValueCopy<$R, $Out>
    on ObjectCopyWith<$R, TrainingSession, $Out> {
  TrainingSessionCopyWith<$R, TrainingSession, $Out> get $asTrainingSession =>
      $base.as((v, t, t2) => _TrainingSessionCopyWithImpl<$R, $Out>(v, t, t2));
}

abstract class TrainingSessionCopyWith<$R, $In extends TrainingSession, $Out>
    implements EventCopyWith<$R, $In, $Out> {
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>> get categories;
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>> get skillLevels;
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>> get trainers;
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
    List<String>? categories,
    String? leadTrainer,
    List<String>? skillLevels,
    List<String>? trainers,
  });
  TrainingSessionCopyWith<$R2, $In, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  );
}

class _TrainingSessionCopyWithImpl<$R, $Out>
    extends ClassCopyWithBase<$R, TrainingSession, $Out>
    implements TrainingSessionCopyWith<$R, TrainingSession, $Out> {
  _TrainingSessionCopyWithImpl(super.value, super.then, super.then2);

  @override
  late final ClassMapperBase<TrainingSession> $mapper =
      TrainingSessionMapper.ensureInitialized();
  @override
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>> get categories =>
      ListCopyWith(
        $value.categories,
        (v, t) => ObjectCopyWith(v, $identity, t),
        (v) => call(categories: v),
      );
  @override
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>>
  get skillLevels => ListCopyWith(
    $value.skillLevels,
    (v, t) => ObjectCopyWith(v, $identity, t),
    (v) => call(skillLevels: v),
  );
  @override
  ListCopyWith<$R, String, ObjectCopyWith<$R, String, String>> get trainers =>
      ListCopyWith(
        $value.trainers,
        (v, t) => ObjectCopyWith(v, $identity, t),
        (v) => call(trainers: v),
      );
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
    List<String>? categories,
    String? leadTrainer,
    List<String>? skillLevels,
    List<String>? trainers,
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
      if (categories != null) #categories: categories,
      if (leadTrainer != null) #leadTrainer: leadTrainer,
      if (skillLevels != null) #skillLevels: skillLevels,
      if (trainers != null) #trainers: trainers,
    }),
  );
  @override
  TrainingSession $make(CopyWithData data) => TrainingSession(
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
    categories: data.get(#categories, or: $value.categories),
    leadTrainer: data.get(#leadTrainer, or: $value.leadTrainer),
    skillLevels: data.get(#skillLevels, or: $value.skillLevels),
    trainers: data.get(#trainers, or: $value.trainers),
  );

  @override
  TrainingSessionCopyWith<$R2, TrainingSession, $Out2> $chain<$R2, $Out2>(
    Then<$Out2, $R2> t,
  ) => _TrainingSessionCopyWithImpl<$R2, $Out2>($value, $cast, t);
}

