import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:latlong2/latlong.dart';

import 'models/types.dart';

enum MapTarget { firestore, reaxdb }

mixin ModelTargetMapping {
  Map<String, dynamic> toMap();

  Map<String, dynamic> toTargetMap(MapTarget target) {
    final map = toMap();

    switch (target) {
      case MapTarget.firestore:
        return map.map((key, value) {
          if (value is DateTime) {
            return MapEntry(key, Timestamp.fromDate(value));
          }
          if((value is LatLng)) {
            return MapEntry(key, GeoPoint(value.latitude, value.longitude));
          }
          if((value is Location)) {
            return MapEntry(key, {
              'description': value.description,
              'coordinates': value.coordinates != null ? GeoPoint(value.coordinates!.latitude, value.coordinates!.longitude) : null
            });
          }
          if(value is OpeningHours) {
            return MapEntry(key, {
              'description': value.description,
              'items': value.items?.map((item) => {
                'season': item.season?.name,
                'dayOfWeek': item.dayOfWeek.name,
                'openingTime': item.openingTime,
                'closingTime': item.closingTime,
                'description': item.description,
              }).toList()
            });
          }

          return MapEntry(key, value);
        });
      case MapTarget.reaxdb:
        return map.map((key, value) {
          if (value is DateTime) {
            return MapEntry(key, value.toIso8601String());
          }
          if((value is LatLng)) {
            return MapEntry(key, { 'latitude': value.latitude, 'longitude': value.longitude });
          }
          if((value is Location)) {
            return MapEntry(key, {
              'description': value.description,
              'coordinates': value.coordinates != null ? {
                'latitude': value.coordinates!.latitude,
                'longitude': value.coordinates!.longitude
              } : null
            });
          }
          if(value is OpeningHours) {
            return MapEntry(key, {
              'description': value.description,
              'items': value.items?.map((item) => {
                'season': item.season?.name,
                'dayOfWeek': item.dayOfWeek.name,
                'openingTime': item.openingTime,
                'closingTime': item.closingTime,
                'description': item.description,
              }).toList()
            });
          }

          return MapEntry(key, value);
        });
    }
  }
}
