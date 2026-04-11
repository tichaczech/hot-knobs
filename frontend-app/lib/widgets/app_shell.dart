import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../routes/routes.dart';
import '../ui/core/l10n/localizations.dart';

// This is the "shell" of the app, which provides the common Scaffold and NavigationBar for all main routes. The actual content of each page will be provided by the child widget, which is determined by the current route.
class AppShell extends StatelessWidget {
  const AppShell({super.key, required this.child});

  static const _icons = [Icons.home, Icons.event, Icons.app_registration, Icons.chat, Icons.person];

  static const _routes = [Routes.dashboard, Routes.events, Routes.registrations, Routes.chat, Routes.profile];

  final Widget child;

  int _selectedIndex(BuildContext context) {
    final location = GoRouterState.of(context).matchedLocation;
    if (location == Routes.dashboard.path) return 0;
    for (int i = _routes.length - 1; i >= 1; i--) {
      if (location.startsWith(_routes[i].path)) return i;
    }
    return 0;
  }

  @override
  Widget build(BuildContext context) {
    final l10n = CoreLocalizations.of(context)!;
    final labels = [l10n.navigationDashboard, l10n.navigationEvents, l10n.navigationRegistrations, l10n.navigationChat, l10n.navigationProfile];

    return Scaffold(
      body: child,
      bottomNavigationBar: NavigationBar(
        destinations: [for (int i = 0; i < _routes.length; i++) NavigationDestination(icon: Icon(_icons[i], size: 32), label: labels[i])],
        labelBehavior: NavigationDestinationLabelBehavior.alwaysShow,
        onDestinationSelected: (i) => context.goNamed(_routes[i].name),
        selectedIndex: _selectedIndex(context),
      ),
    );
  }
}
