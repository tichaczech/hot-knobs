import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:dart_mappable/dart_mappable.dart';
import 'package:latlong2/latlong.dart';

class LatLngDecoderOnlyMapper extends SimpleMapper<LatLng> {
  const LatLngDecoderOnlyMapper();

  @override
  LatLng decode(dynamic value) {
    if (value is GeoPoint) {
      return LatLng(value.latitude, value.longitude);
    } else {
      throw Exception('Invalid LatLng format');
    }
  }

  @override
  dynamic encode(LatLng value) {
    return value;
  }
}
