import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../l10n/localizations.dart';

class RegistrationsHomeScreen extends StatefulWidget {
  const RegistrationsHomeScreen({super.key});

  @override
  State<RegistrationsHomeScreen> createState() => _RegistrationsHomeScreenState();
}

class _RegistrationsHomeScreenState extends State<RegistrationsHomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(RegistrationsLocalizations.of(context)!.registrationsScreenName)),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const Center(child: Text('List of Registrations goes here...')),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.registrationView.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to View Registration'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.registrationEdit.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to Edit Registration'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
