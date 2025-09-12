import 'package:flutter/material.dart' hide NavigationDrawer;

import '../../ui/core/l10n/core_localizations.dart';
import '../../widgets/navigation_drawer.dart';

class OperatorsHome extends StatefulWidget {
  const OperatorsHome({super.key});

  @override
  State<OperatorsHome> createState() => _OperatorsHomeState();
}

class _OperatorsHomeState extends State<OperatorsHome> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(CoreLocalizations.of(context)!.operatorsScreenName),
      ),
      body: Center(child: Text("It's rainy here")),
      drawer: const NavigationDrawer(),
    );
  }
}
