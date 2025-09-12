// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format off
// ignore_for_file: type=lint
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

