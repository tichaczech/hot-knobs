import 'package:dart_mappable/dart_mappable.dart';

import '../mappers/latlng.dart';
// import '../mappers/location.dart';
// import '../mappers/opening_hours.dart';
import 'entity.dart';
import 'types.dart';

part 'site.mapper.dart';

// @MappableClass(discriminatorValue: 'site', includeCustomMappers: [LatLngDecoderOnlyMapper(), LocationDecoderOnlyMapper(), OpeningHoursDecoderOnlyMapper()])
@MappableClass(discriminatorValue: 'site', includeCustomMappers: [LatLngDecoderOnlyMapper()])
class Site extends Entity with SiteMappable {
  final String? address;
  final Location? arrival;
  final String? description;
  final List<String>? images;
  final int? length; // in meters
  final Location location;
  final String name;
  final OpeningHours? openingHours;
  final Location? parking;
  final SiteType siteType;
  final SkillLevel skillLevel;

  Site({
    required super.id,
    required super.active,
    this.address,
    this.arrival,
    required super.cachedAt,
    required super.createdAt,
    required super.createdBy,
    this.description,
    required super.etag,
    this.images,
    this.length,
    required this.location,
    this.openingHours,
    this.parking,
    required this.siteType,
    required this.skillLevel,
    required super.updatedAt,
    required super.updatedBy,
    required this.name,
  }) : super() {
    print('Site created: $id');
  }
}

@MappableClass()
class SiteCreateModel extends CreateModel with SiteCreateModelMappable {
  final String? address;
  final Location? arrival;
  final String description;
  final int? length;
  final Location? location;
  final String name;
  final OpeningHours? openingHours;
  final Location? parking;
  final SiteType? siteType;
  final SkillLevel? skillLevel;

  SiteCreateModel({this.address, this.arrival, required this.description, this.length, this.location, required this.name, this.openingHours, this.parking, this.siteType, this.skillLevel});
}

@MappableClass()
class SiteUpdateModel extends UpdateModel with SiteUpdateModelMappable {
  final String? address;
  final Location? arrival;
  final String description;
  final int? length;
  final Location? location;
  final String name;
  final OpeningHours? openingHours;
  final Location? parking;
  final SiteType? siteType;
  final SkillLevel? skillLevel;

  SiteUpdateModel({this.address, this.arrival, required this.description, this.length, this.location, required this.name, this.openingHours, this.parking, this.siteType, this.skillLevel});
}
