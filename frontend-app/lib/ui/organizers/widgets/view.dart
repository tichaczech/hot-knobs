import 'package:flutter/material.dart';

import '../l10n/organizers_localizations.dart';

class OrganizerViewScreen extends StatefulWidget {
  const OrganizerViewScreen({super.key});

  @override
  State<OrganizerViewScreen> createState() => _OrganizerViewScreenState();
}

class _OrganizerViewScreenState extends State<OrganizerViewScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(OrganizersLocalizations.of(context)!.organizerViewScreenName)),
        body: const Center(child: Text('View of Organizer goes here...')),
      ),
    );
  }
}
