import 'package:flutter/material.dart';

import '../l10n/localizations.dart';

class OrganizerEditScreen extends StatefulWidget {
  const OrganizerEditScreen({super.key});

  @override
  State<OrganizerEditScreen> createState() => _OrganizerEditScreenState();
}

class _OrganizerEditScreenState extends State<OrganizerEditScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(OrganizersLocalizations.of(context)!.organizerEditScreenName)),
        body: const Center(child: Text('Edit of Organizer goes here...')),
      ),
    );
  }
}
