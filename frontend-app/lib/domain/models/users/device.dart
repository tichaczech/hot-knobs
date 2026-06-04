import 'package:dart_mappable/dart_mappable.dart';

import '../entity.dart';
import '../types.dart';

part 'device.mapper.dart';

@MappableClass(discriminatorValue: 'device')
class Device extends Entity with DeviceMappable {
  final String? description;
  final String deviceId;
  final DateTime lastActiveAt;
  final String modelName;
  final String name;
  final Platform platform;
  final String profileId;
  final String systemName;
  final String token;

  Device({
    required super.active,
    required super.id,
    required super.cachedAt,
    this.description,
    required this.deviceId,
    required super.etag,
    required this.lastActiveAt,
    required this.modelName,
    required this.name,
    required this.platform,
    required this.profileId,
    required this.systemName,
    required this.token,
  }) : super();

  @override
  toString() => 'Device: $name (model: $modelName, platform: $platform, id: $id)';
}

@MappableClass()
class DeviceCreateModel extends CreateModel with DeviceCreateModelMappable {
  final String deviceId;
  final String modelName;
  final Platform platform;
  final String systemName;
  final String name;
  final String token;

  DeviceCreateModel({required this.deviceId, required this.modelName, required this.name, required this.platform, required this.systemName, required this.token}) : super();

  @override
  toString() => 'DeviceCreateModel: $name (model: $modelName, platform: $platform)';
}

@MappableClass()
class DeviceUpdateModel extends UpdateModel with DeviceUpdateModelMappable {
  final String? description;
  final String name;

  DeviceUpdateModel({this.description, required this.name}) : super();

  @override
  toString() => 'DeviceUpdateModel: $name (description: $description)';
}
