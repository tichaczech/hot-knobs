import 'package:flutter/material.dart';

import '../l10n/localizations.dart';

class RegistrationEditScreen extends StatefulWidget {
  const RegistrationEditScreen({super.key});

  @override
  State<RegistrationEditScreen> createState() => _RegistrationEditScreenState();
}

class _RegistrationEditScreenState extends State<RegistrationEditScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(RegistrationsLocalizations.of(context)!.registrationEditScreenName)),
        body: const Center(child: Text('Edit of Registration goes here...')),
      ),
    );
  }
}
