import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../l10n/profile_localizations.dart';

class ProfileHomeScreen extends StatefulWidget {
  const ProfileHomeScreen({super.key});

  @override
  State<ProfileHomeScreen> createState() => _ProfileHomeScreenState();
}

class _ProfileHomeScreenState extends State<ProfileHomeScreen> {
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
            ],
          ),
        ),
      ),
    );
  }
}
