import 'package:flutter/material.dart';

import '../l10n/sites_localizations.dart';

class SiteViewScreen extends StatefulWidget {
  const SiteViewScreen({super.key});

  @override
  State<SiteViewScreen> createState() => _SiteViewScreenState();
}

class _SiteViewScreenState extends State<SiteViewScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(SitesLocalizations.of(context)!.siteViewScreenName)),
        body: const Center(child: Text('View of Registration goes here...')),
      ),
    );
  }
}
