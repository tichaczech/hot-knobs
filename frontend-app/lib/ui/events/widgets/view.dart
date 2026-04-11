import 'package:flutter/material.dart';

import '../l10n/localizations.dart';

class EventViewScreen extends StatefulWidget {
  const EventViewScreen({super.key});

  @override
  State<EventViewScreen> createState() => _EventViewScreenState();
}

class _EventViewScreenState extends State<EventViewScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(EventsLocalizations.of(context)!.eventViewScreenName)),
        body: const Center(child: Text('View of Event goes here...')),
      ),
    );
  }
}
