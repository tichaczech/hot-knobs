import 'package:flutter/material.dart';

import '../l10n/sites_localizations.dart';

class SiteEditScreen extends StatefulWidget {
  const SiteEditScreen({super.key});

  @override
  State<SiteEditScreen> createState() => _SiteEditScreenState();
}

class _SiteEditScreenState extends State<SiteEditScreen> {
  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(SitesLocalizations.of(context)!.siteEditScreenName)),
        body: const Center(child: Text('Edit of Site goes here...')),
      ),
    );
  }
}
