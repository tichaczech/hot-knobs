import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart' hide NavigationDrawer;
import 'package:flutter/services.dart';

import '../../ui/core/l10n/core_localizations.dart';
import '../../widgets/navigation_drawer.dart';

class ParticipantsHome extends StatefulWidget {
  const ParticipantsHome({super.key});

  @override
  State<ParticipantsHome> createState() => _ParticipantsHomeState();
}

class _ParticipantsHomeState extends State<ParticipantsHome> {
  late Future<String?> _apnsToken;
  late Future<String?> _fcmToken;

  @override
  void initState() {
    super.initState();
    _apnsToken = FirebaseMessaging.instance.getAPNSToken();
    _fcmToken = FirebaseMessaging.instance.getToken();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(CoreLocalizations.of(context)!.participantsScreenName)),
      body: Column(
        children: [
          Text("It's cloudy here"),
          const SizedBox(height: 32),
          FutureBuilder(
            future: _apnsToken,
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return CircularProgressIndicator();
              } else if (snapshot.hasError) {
                return Text('Error: ${snapshot.error}');
              } else {
                return InkWell(
                  onTap: () => Clipboard.setData(ClipboardData(text: snapshot.data ?? '#value-not-set')),
                  child: Text('APNs Token: ${snapshot.data}'),
                );
              }
            },
          ),
          const SizedBox(height: 32),
          FutureBuilder(
            future: _fcmToken,
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return CircularProgressIndicator();
              } else if (snapshot.hasError) {
                return Text('Error: ${snapshot.error}');
              } else {
                return InkWell(
                  onTap: () => Clipboard.setData(ClipboardData(text: snapshot.data ?? '#value-not-set')),
                  child: Text('FCM Token: ${snapshot.data}'),
                );
              }
            },
          ),
        ],
      ),
      drawer: const NavigationDrawer(),
    );
  }
}
