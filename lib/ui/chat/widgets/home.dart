import 'package:flutter/material.dart';

import '../../chat/l10n/chat_localizations.dart';

class ChatsHomeScreen extends StatefulWidget {
  const ChatsHomeScreen({super.key});

  @override
  State<ChatsHomeScreen> createState() => _ChatsHomeScreenState();
}

class _ChatsHomeScreenState extends State<ChatsHomeScreen> {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(ChatLocalizations.of(context)!.chatsScreenName),
      ),
      body: const Center(child: Text('Chat screen - work in progress')),
    );
  }
}
