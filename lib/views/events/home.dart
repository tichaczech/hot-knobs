import 'package:flutter/material.dart' hide NavigationDrawer;

import '../../ui/core/l10n/core_localizations.dart';
import '../../widgets/navigation_drawer.dart';

class EventsHome extends StatefulWidget {
  const EventsHome({super.key});

  @override
  State<EventsHome> createState() => _EventsHomeState();
}

class _EventsHomeState extends State<EventsHome> {

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
              Tab(icon: Icon(Icons.calendar_month), text: CoreLocalizations.of(context)!.eventsScreenUpcomingTab),
              Tab(icon: Icon(Icons.near_me), text: CoreLocalizations.of(context)!.eventsScreenNearbyTab),
              Tab(icon: Icon(Icons.history), text: CoreLocalizations.of(context)!.eventsScreenPastTab),
            ],
          ),
          title: Text(CoreLocalizations.of(context)!.eventsScreenName),
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
