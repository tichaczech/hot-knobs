import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../l10n/localizations.dart';

class EventsHomeScreen extends StatefulWidget {
  const EventsHomeScreen({super.key});

  @override
  State<EventsHomeScreen> createState() => _EventsHomeScreenState();
}

class _EventsHomeScreenState extends State<EventsHomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(EventsLocalizations.of(context)!.eventsScreenName)),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const Center(child: Text('List of Organizers goes here...')),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.eventView.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to View Event'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.eventEdit.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to Edit Event'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
