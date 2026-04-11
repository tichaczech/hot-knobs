import 'package:flutter/material.dart';

import '../l10n/localizations.dart';

class EventEditScreen extends StatefulWidget {
  const EventEditScreen({super.key});

  @override
  State<EventEditScreen> createState() => _EventEditScreenState();
}

class _EventEditScreenState extends State<EventEditScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(EventsLocalizations.of(context)!.eventEditScreenName)),
        body: const Center(child: Text('Edit of Event goes here...')),
      ),
    );
  }
}
