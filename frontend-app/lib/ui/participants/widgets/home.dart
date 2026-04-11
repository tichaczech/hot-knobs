import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../l10n/localizations.dart';

class ParticipantsHomeScreen extends StatefulWidget {
  const ParticipantsHomeScreen({super.key});

  @override
  State<ParticipantsHomeScreen> createState() => _ParticipantsHomeScreenState();
}

class _ParticipantsHomeScreenState extends State<ParticipantsHomeScreen> {
  late Future<String?> _apnsToken;
  late Future<String?> _fcmToken;

  @override
  void initState() {
    super.initState();
    _apnsToken = Future.value('123'); // FirebaseMessaging.instance.getAPNSToken();
    _fcmToken = Future.value('456'); // FirebaseMessaging.instance.getToken();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(ParticipantsLocalizations.of(context)!.participantsScreenName)),
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
    );
  }
}
