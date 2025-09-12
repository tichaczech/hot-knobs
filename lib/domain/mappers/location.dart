import 'package:dart_mappable/dart_mappable.dart';
import 'package:latlong2/latlong.dart';

import '../models/types.dart';

class LocationDecoderOnlyMapper extends SimpleMapper<Location> {
  const LocationDecoderOnlyMapper();

  @override
  Location decode(dynamic value) {
    if (value is Map) {
      return (description: value['description'], coordinates: LatLng(value['latitude'], value['longitude']));
    } else {
      throw Exception('Invalid LatLng format');
    }
  }

  @override
  dynamic encode(Location value) {
    return value;
  }
}
