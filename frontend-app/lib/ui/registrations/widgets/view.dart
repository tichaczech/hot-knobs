import 'package:flutter/material.dart';

import '../l10n/registrations_localizations.dart';

class RegistrationViewScreen extends StatefulWidget {
  const RegistrationViewScreen({super.key});

  @override
  State<RegistrationViewScreen> createState() => _RegistrationViewScreenState();
}

class _RegistrationViewScreenState extends State<RegistrationViewScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(RegistrationsLocalizations.of(context)!.registrationViewScreenName)),
        body: const Center(child: Text('View of Registration goes here...')),
      ),
    );
  }
}
