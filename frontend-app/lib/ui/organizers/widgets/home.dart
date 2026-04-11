import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../l10n/localizations.dart';

class OrganizersHomeScreen extends StatefulWidget {
  const OrganizersHomeScreen({super.key});

  @override
  State<OrganizersHomeScreen> createState() => _OrganizersHomeScreenState();
}

class _OrganizersHomeScreenState extends State<OrganizersHomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(OrganizersLocalizations.of(context)!.organizersScreenName),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const Center(child: Text('List of Organizers goes here...')),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.organizerView.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to View Organizer'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.organizerEdit.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to Edit Organizer'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
