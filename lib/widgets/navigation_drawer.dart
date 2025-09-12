import 'dart:convert';

import 'package:crypto/crypto.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../routes/routes.dart';
import '../ui/core/l10n/core_localizations.dart';

class NavigationDrawer extends StatelessWidget {
  const NavigationDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    final currentUser = FirebaseAuth.instance.currentUser;
    if (currentUser == null) {
      throw Exception('User not logged in');
    }

    print(currentUser.email.hashCode);

    return Drawer(
      child: SafeArea(
        top: false,
        child: ListView(
          padding: const EdgeInsets.all(0),
          shrinkWrap: true,
          children: <Widget>[
            InkWell(
              child: DrawerHeader(
                decoration: BoxDecoration(color: Theme.of(context).colorScheme.primaryContainer),
                child: Column(
                  children: [
                    CircleAvatar(
                      radius: 40,
                      backgroundImage: NetworkImage(
                        // TODO: Cache photo for performance improvements
                        // TODO: Cache Gravatar URL for performance improvements
                        currentUser.photoURL ?? 'https://www.gravatar.com/avatar/${sha256.convert(utf8.encode(currentUser.email!.toLowerCase().trim()))}',
                      ),
                    ),
                    Text(currentUser.displayName ?? currentUser.email!.split('@').first, style: Theme.of(context).textTheme.headlineMedium),
                    Text(currentUser.email!, style: Theme.of(context).textTheme.bodyMedium),
                  ],
                ),
              ),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.pushNamed(Routes.authProfile.name);
              },
            ),

            ListTile(
              leading: Icon(Icons.home),
              title: Text(CoreLocalizations.of(context)!.navigationDashboard),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                if (GoRouterState.of(context).uri.toString() != Routes.dashboard.path) {
                  context.replaceNamed(Routes.dashboard.name);
                }
              },
            ),
            ListTile(
              leading: Icon(Icons.event),
              title: Text(CoreLocalizations.of(context)!.navigationEvents),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.replaceNamed(Routes.events.name);
              },
            ),
            ListTile(
              leading: Icon(Icons.app_registration),
              title: Text(CoreLocalizations.of(context)!.navigationRegistrations),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.replaceNamed(Routes.registrations.name);
              },
            ),
            ListTile(
              leading: Icon(Icons.directions_bike),
              title: Text(CoreLocalizations.of(context)!.navigationParticipants),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.replaceNamed(Routes.participants.name);
              },
            ),
            ListTile(
              leading: Icon(Icons.workspaces),
              title: Text(CoreLocalizations.of(context)!.navigationOperators),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.replaceNamed(Routes.operators.name);
              },
            ),
            ListTile(
              leading: Icon(Icons.location_on_outlined),
              title: Text(CoreLocalizations.of(context)!.navigationSites),
              onTap: () {
                Scaffold.of(context).closeDrawer();
                context.replaceNamed(Routes.sites.name);
              },
            ),
          ],
        ),
      ),
    );
  }
}
