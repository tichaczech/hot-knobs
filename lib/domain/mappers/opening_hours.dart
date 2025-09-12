import 'package:dart_mappable/dart_mappable.dart';

import '../models/types.dart';

class OpeningHoursDecoderOnlyMapper extends SimpleMapper<OpeningHours> {
  const OpeningHoursDecoderOnlyMapper();

  @override
  OpeningHours decode(dynamic value) {
    if (value is Map) {
      return (
        description: value['description'],
        items: (value['items'] as List<Map>?)?.map((item) =>
        (
          season: item['season'] != null ? Season.values.firstWhere((e) => e.name == item['season']) : null,
          dayOfWeek: DayOfWeek.values.firstWhere((e) => e.name == item['dayOfWeek']),
          openingTime: item['openingTime'],
          closingTime: item['closingTime'],
          description: item['description'],
        ) as OpeningHoursItem).toList(),
      );
    } else {
      throw Exception('Invalid OpeningHours format');
    }
  }

  @override
  dynamic encode(OpeningHours value) {
    return value;
  }
}
