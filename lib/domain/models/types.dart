import 'package:dart_mappable/dart_mappable.dart';
import 'package:flutter/widgets.dart';
import 'package:frontend/ui/core/l10n/core_localizations.dart';
import 'package:latlong2/latlong.dart';

part 'types.mapper.dart';

mixin TranslatableEnum {
  static Locale? __locale;

  String get displayName;

  Locale get _locale {
    if (__locale != null) {
      return __locale!;
    }

    final currentLocale = WidgetsBinding.instance.platformDispatcher.locale;
    final supportedLocale = CoreLocalizations.delegate.isSupported(currentLocale) ? currentLocale : const Locale('en');

    return __locale = supportedLocale;
  }
}

@MappableEnum()
enum DayOfWeek with TranslatableEnum {
  mon,
  tue,
  wed,
  thu,
  fri,
  sat,
  sun;

  @override
  String get displayName {
    final t = lookupCoreLocalizations(_locale);

    return switch (this) {
      DayOfWeek.mon => t.dayOfWeekMonday,
      DayOfWeek.tue => t.dayOfWeekTuesday,
      DayOfWeek.wed => t.dayOfWeekWednesday,
      DayOfWeek.thu => t.dayOfWeekThursday,
      DayOfWeek.fri => t.dayOfWeekFriday,
      DayOfWeek.sat => t.dayOfWeekSaturday,
      DayOfWeek.sun => t.dayOfWeekSunday,
    };
  }
}

@MappableClass()
class DeviceRegistration extends DeviceRegistrationMappable {
  final String? apnsToken;
  final String fcmToken;
  final String name;
  final String platform;

  DeviceRegistration({this.apnsToken, required this.fcmToken, required this.name, required this.platform});
}

typedef Location = ({String? description, LatLng? coordinates});

typedef OpeningHours = ({List<OpeningHoursItem>? items, String? description});

typedef OpeningHoursItem = ({Season? season, DayOfWeek dayOfWeek, int openingTime, int closingTime, String? description});

@MappableEnum()
enum Season with TranslatableEnum {
  spr,
  sum,
  fal,
  win;

  @override
  String get displayName {
    final t = lookupCoreLocalizations(_locale);

    return switch (this) {
      Season.spr => t.seasonSpring,
      Season.sum => t.seasonSummer,
      Season.fal => t.seasonAutumn,
      Season.win => t.seasonWinter,
    };
  }
}

@MappableEnum()
enum SiteType with TranslatableEnum {
  enduro,
  motocross;

  @override
  String get displayName {
    final t = lookupCoreLocalizations(_locale);

    return switch (this) {
      SiteType.enduro => t.siteTypeEnduro,
      SiteType.motocross => t.siteTypeMotocross,
    };
  }
}

@MappableEnum()
enum SkillLevel with TranslatableEnum {
  fst,
  beg,
  int,
  adv,
  pro;

  @override
  String get displayName {
    final t = lookupCoreLocalizations(_locale);

    return switch (this) {
      SkillLevel.fst => t.skillLevelFirstTimer,
      SkillLevel.beg => t.skillLevelBeginner,
      SkillLevel.int => t.skillLevelIntermediate,
      SkillLevel.adv => t.skillLevelAdvanced,
      SkillLevel.pro => t.skillLevelProfessional,
    };
  }
}
