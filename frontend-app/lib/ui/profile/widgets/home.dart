import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../../../utils/auth_provider.dart';
import '../l10n/localizations.dart';

class ProfileHomeScreen extends StatefulWidget {
  const ProfileHomeScreen({super.key, required this.authProvider});

  final AuthProvider authProvider;

  @override
  State<ProfileHomeScreen> createState() => _ProfileHomeScreenState();
}

class _ProfileHomeScreenState extends State<ProfileHomeScreen> {
  late final AuthProvider _authProvider;

  @override
  void initState() {
    super.initState();
    _authProvider = widget.authProvider;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(ProfileLocalizations.of(context)!.profileScreenName)),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              const Center(child: Text('List of Profiles goes here...')),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.participants.name);
                },
                child: const Text('Go to List of Participants'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.profileView.name);
                },
                child: const Text('Go to View Profile'),
              ),
              ElevatedButton(
                onPressed: () {
                  context.pushNamed(Routes.profileEdit.name);
                },
                child: const Text('Go to Edit Profile'),
              ),
              ElevatedButton(
                onPressed: () {
                  _authProvider.signOut();
                  context.replaceNamed(Routes.dashboard.name);
                },
                child: const Text('Sign Out'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
