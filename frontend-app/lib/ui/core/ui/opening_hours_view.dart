import 'package:flutter/material.dart';

import '../../../domain/models/types.dart';
import '../l10n/localizations.dart';

class OpeningHoursView extends StatelessWidget {
  OpeningHoursView({super.key, required OpeningHours openingHours}) {
    _openingHours = openingHours;
  }

  late final OpeningHours _openingHours;

  String _formatTime(int time) {
    final hours = time ~/ 60;
    final minutes = time % 60;
    return '${hours.toString().padLeft(2, '0')}:${minutes.toString().padLeft(2, '0')}';
  }

  String _seasonLabel(BuildContext context, Season? season) {
    if (season == null) {
      return CoreLocalizations.of(context)!.seasonAllYear;
    }

    return season.displayName;
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              if (_openingHours.description != null && _openingHours.description!.isNotEmpty) ...[Text(_openingHours.description!, style: Theme.of(context).textTheme.bodyLarge), const SizedBox(height: 8)],
              if (_openingHours.items != null && _openingHours.items!.isNotEmpty) ...[..._buildGroupedItems(context)],
            ],
          ),
        ),
      ],
    );
  }

  List<Widget> _buildGroupedItems(BuildContext context) {
    final items = _openingHours.items!;

    // Build per-season data structures
    final Map<Season?, List<dynamic>> itemsBySeason = {};
    for (final it in items) {
      itemsBySeason.putIfAbsent(it.season, () => []).add(it);
    }

    // For each season, compute normalized day schedule keys and a season-level key
    final Map<Season?, Map<int, List<dynamic>>> seasonByDayIndex = {};
    final Map<Season?, Map<int, String>> seasonDayKey = {};
    final Map<Season?, String> seasonScheduleKey = {};

    for (final season in itemsBySeason.keys) {
      final seasonItems = itemsBySeason[season]!;
      final byDayIndex = <int, List<dynamic>>{};
      for (final it in seasonItems) {
        final idx = it.dayOfWeek.index;
        byDayIndex.putIfAbsent(idx, () => []).add(it);
      }
      // sort ranges in each day and build a stable key
      final dayKey = <int, String>{};
      final sortedDayIndices = byDayIndex.keys.toList()..sort();
      for (final idx in sortedDayIndices) {
        final dayItems = byDayIndex[idx]!..sort((a, b) => a.openingTime.compareTo(b.openingTime));
        final key = dayItems.map((it) => '${it.openingTime}-${it.closingTime}-${it.description ?? ''}').join('|');
        dayKey[idx] = key;
      }
      final seasonKey = sortedDayIndices.map((i) => '$i=${dayKey[i]}').join('||');
      seasonByDayIndex[season] = byDayIndex;
      seasonDayKey[season] = dayKey;
      seasonScheduleKey[season] = seasonKey;
    }

    // Group seasons by identical schedules
    final Map<String, List<Season?>> seasonsBySchedule = {};
    for (final entry in seasonScheduleKey.entries) {
      seasonsBySchedule.putIfAbsent(entry.value, () => []).add(entry.key);
    }

    // Order groups by first season (null first, then enum order)
    int seasonOrder(Season? s) => s == null ? -1 : s.index;
    final scheduleKeys = seasonsBySchedule.keys.toList()
      ..sort((a, b) => seasonOrder((seasonsBySchedule[a]!..sort((x, y) => seasonOrder(x).compareTo(seasonOrder(y)))).first).compareTo(seasonOrder((seasonsBySchedule[b]!..sort((x, y) => seasonOrder(x).compareTo(seasonOrder(y)))).first)));

    final List<Widget> widgets = [];

    for (final sk in scheduleKeys) {
      final seasons = seasonsBySchedule[sk]!..sort((a, b) => seasonOrder(a).compareTo(seasonOrder(b)));

      // Build season header label: if includes All year (null), prefer that only
      String seasonHeaderLabel;
      if (seasons.contains(null)) {
        seasonHeaderLabel = _seasonLabel(context, null);
      } else {
        // Collapse contiguous season indices into ranges
        final indices = seasons.map((s) => s!.index).toList()..sort();
        final segments = <String>[];
        int iSeg = 0;
        while (iSeg < indices.length) {
          int start = indices[iSeg];
          int end = start;
          int j = iSeg;
          while (j + 1 < indices.length && indices[j + 1] == indices[j] + 1) {
            end = indices[j + 1];
            j++;
          }
          final startLabel = _seasonLabel(context, Season.values[start]);
          final endLabel = _seasonLabel(context, Season.values[end]);
          segments.add(start == end ? startLabel : '$startLabel–$endLabel');
          iSeg = j + 1;
        }
        seasonHeaderLabel = segments.join(', ');
      }

      // Header
      widgets.add(
        Padding(
          padding: EdgeInsets.only(top: widgets.isEmpty ? 0 : 8.0, bottom: 4.0),
          child: Text(seasonHeaderLabel, style: Theme.of(context).textTheme.titleMedium),
        ),
      );

      // Use the first season in this group as representative for day schedule rendering
      final representative = seasons.first;
      final byDayIndex = seasonByDayIndex[representative]!;

      final dayIndices = byDayIndex.keys.toList()..sort();
      final Map<String, List<int>> daysBySchedule = {};
      final Map<String, String> scheduleDisplayText = {};

      for (final idx in dayIndices) {
        final dayItems = byDayIndex[idx]!; // already sorted
        final key = seasonDayKey[representative]![idx] ?? '';
        final display = dayItems
            .map((it) {
              final range = '${_formatTime(it.openingTime)} - ${_formatTime(it.closingTime)}';
              final desc = (it.description != null && it.description!.isNotEmpty) ? ' (${it.description})' : '';
              return '$range$desc';
            })
            .join('; ');
        daysBySchedule.putIfAbsent(key, () => []).add(idx);
        scheduleDisplayText[key] = display;
      }

      final dayScheduleKeys = daysBySchedule.keys.toList()..sort((a, b) => daysBySchedule[a]!.first.compareTo(daysBySchedule[b]!.first));

      for (final key in dayScheduleKeys) {
        final indices = daysBySchedule[key]!..sort();
        final List<String> segments = [];
        int s = 0;
        while (s < indices.length) {
          int start = indices[s];
          int end = start;
          int t = s;
          while (t + 1 < indices.length && indices[t + 1] == indices[t] + 1) {
            end = indices[t + 1];
            t++;
          }
          final startLabel = DayOfWeek.values[start].displayName;
          final endLabel = DayOfWeek.values[end].displayName;
          segments.add(start == end ? startLabel : '$startLabel–$endLabel');
          s = t + 1;
        }

        final dayGroupLabel = segments.join(', ');
        final rangesText = scheduleDisplayText[key]!;

        widgets.add(
          Padding(
            padding: const EdgeInsets.only(bottom: 4.0),
            child: Text('$dayGroupLabel: $rangesText', style: Theme.of(context).textTheme.bodyMedium),
          ),
        );
      }
    }

    return widgets;
  }
}
