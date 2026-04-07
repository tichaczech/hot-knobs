import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../routes/routes.dart';
import '../ui/core/l10n/core_localizations.dart';

class AppShell extends StatelessWidget {
  const AppShell({super.key, required this.child});

  final Widget child;

  static const _routes = [
    Routes.dashboard,
    Routes.events,
    Routes.registrations,
    // Routes.participants,
    // Routes.operators,
    // Routes.sites,
    Routes.chat,
    Routes.authProfile,
  ];

  static const _icons = [
    Icons.home,
    Icons.event,
    Icons.app_registration,
    // Icons.directions_bike,
    // Icons.workspaces,
    // Icons.location_on_outlined,
    Icons.chat,
    Icons.person
  ];

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
    final labels = [
      l10n.navigationDashboard,
      l10n.navigationEvents,
      l10n.navigationRegistrations,
      // l10n.navigationParticipants,
      // l10n.navigationOperators,
      // l10n.navigationSites,
      l10n.navigationChat,
      l10n.navigationProfile,
    ];

    return Scaffold(
      body: child,
      bottomNavigationBar: NavigationBar(
        selectedIndex: _selectedIndex(context),
        labelBehavior: NavigationDestinationLabelBehavior.alwaysShow,
        onDestinationSelected: (i) => context.goNamed(_routes[i].name),
        destinations: [
          for (int i = 0; i < _routes.length; i++)
            NavigationDestination(
              icon: Icon(_icons[i]),
              label: labels[i],
            ),
        ],
      ),
    );
  }
}
