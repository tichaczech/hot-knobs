import 'package:flutter/material.dart' hide NavigationDrawer;

import '../../ui/core/l10n/core_localizations.dart';
import '../../widgets/navigation_drawer.dart';

class RegistrationsHome extends StatefulWidget {
  const RegistrationsHome({super.key});

  @override
  State<RegistrationsHome> createState() => _RegistrationsHomeState();
}

class _RegistrationsHomeState extends State<RegistrationsHome> {

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 3,
      child: Scaffold(
        appBar: AppBar(
          backgroundColor: Theme.of(context).colorScheme.primaryContainer,
          bottom: TabBar(
            tabs: <Widget>[
              Tab(icon: Icon(Icons.check_circle_outline), text: CoreLocalizations.of(context)!.registrationsScreenConfirmedTab,),
              Tab(icon: Icon(Icons.question_mark), text: CoreLocalizations.of(context)!.registrationsScreenWaitingTab,),
              Tab(icon: Icon(Icons.history), text: CoreLocalizations.of(context)!.registrationsScreenPastTab,),
            ],
          ),
          title: Text(CoreLocalizations.of(context)!.registrationsScreenName),
        ),
        body: TabBarView(children: [
          Center(child: Text("It's cloudy here")),
          Center(child: Text("It's rainy here")),
          Center(child: Text("It's sunny here")),
        ]),
        drawer: const NavigationDrawer(),
      ),
    );
  }
}
