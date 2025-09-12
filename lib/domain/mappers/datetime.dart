import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:dart_mappable/dart_mappable.dart';

class DateTimeDecoderOnlyMapper extends SimpleMapper<DateTime> {
  const DateTimeDecoderOnlyMapper();

  @override
  DateTime decode(dynamic value) {
    if (value is String) {
      return DateTime.parse(value);
    } else if (value is Timestamp) {
      return value.toDate();
    } else if (value is num) {
      return DateTime.fromMillisecondsSinceEpoch(value.round());
    } else {
      throw Exception('Invalid DateTime format');
    }
  }

  @override
  dynamic encode(DateTime value) {
    return value;
  }
}
