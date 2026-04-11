import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../l10n/sites_localizations.dart';

class SitesHomeScreen extends StatefulWidget {
  const SitesHomeScreen({super.key});

  @override
  State<SitesHomeScreen> createState() => _SitesHomeScreenState();
}

class _SitesHomeScreenState extends State<SitesHomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(SitesLocalizations.of(context)!.sitesScreenName)),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const Center(child: Text('List of Sites goes here...')),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.siteView.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to View Site'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.siteEdit.name, pathParameters: {'id': '1'});
                },
                child: const Text('Go to Edit Site'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
