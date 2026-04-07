import 'dart:developer';

import 'package:msal_auth/msal_auth.dart';
import 'package:flutter/material.dart' hide NavigationDrawer;

import '../../ui/core/l10n/core_localizations.dart';
import '../../widgets/navigation_drawer.dart';

class DashboardHome extends StatefulWidget {
  const DashboardHome({super.key});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  @override
  State<DashboardHome> createState() => _DashboardHomeState();
}

class _DashboardHomeState extends State<DashboardHome> {
  int _counter = 0;

  Future<void> _incrementCounter() async {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      _counter++;
    });

    final msalAuth = await MultipleAccountPca.create(
      clientId: 'e19c3441-3d78-49ae-917a-43b3c32852ad',
      androidConfig: AndroidConfig(
        configFilePath: 'assets/msal_config.json',
        redirectUri: 'msauth://app.hotknobs.v1/ghCPg%2FgSZi0PrA1gFfvCh09Cs6E%3D'
      ),
      appleConfig: AppleConfig(
        authority: 'https://hotknobsdev.ciamlogin.com/099336d3-2e34-4bf2-be23-a507e86cee6e',
        // Change authority type to 'b2c' for business to customer flow.
        authorityType: AuthorityType.b2c,
        // Change broker if you need. Applicable only for iOS platform.
        broker: Broker.webView,
      ),
    );

    final authResult = await msalAuth.acquireToken(
      scopes: <String>[
        // 'https://graph.microsoft.com/email',
        // 'https://graph.microsoft.com/offline_access',
        // 'https://graph.microsoft.com/openid',
        // 'https://graph.microsoft.com/profile',
        // 'https://graph.microsoft.us/.default'
        // 'email',
        // 'offline_access',
        // 'openid',
        // 'profile',
        '.default' // Use '.default' to request all the scopes that are configured for the application in the portal.
        // Add other scopes here if required.
      ],
      // UI option for authentication, default is [Prompt.whenRequired]
      prompt: Prompt.whenRequired,
      // Provide 'loginHint' if you have.
      loginHint: '<Email Id / Username / Unique Identifier>',
      // Optional: Custom authority URL for B2C or different tenant scenarios
      authority: 'https://hotknobsdev.ciamlogin.com/099336d3-2e34-4bf2-be23-a507e86cee6e',
    );

    log('ID token: ${authResult.idToken}');
    log('Access token: ${authResult.accessToken}');
    log('Auth result: ${authResult.toJson()}');
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // TRY THIS: Try changing the color here to a specific color (to
        // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
        // change color while the other colors stay the same.
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(CoreLocalizations.of(context)!.dashboardScreenName),
      ),
      body: Center(
        // Center is a layout widget. It takes a single child and positions it
        // in the middle of the parent.
        child: Column(
          // Column is also a layout widget. It takes a list of children and
          // arranges them vertically. By default, it sizes itself to fit its
          // children horizontally, and tries to be as tall as its parent.
          //
          // Column has various properties to control how it sizes itself and
          // how it positions its children. Here we use mainAxisAlignment to
          // center the children vertically; the main axis here is the vertical
          // axis because Columns are vertical (the cross axis would be
          // horizontal).
          //
          // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
          // action in the IDE, or press "p" in the console), to see the
          // wireframe for each widget.
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text('You have pushed the button this many times:'),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headlineMedium,
            ),
          ],
        ),
      ),
      drawer: const NavigationDrawer(),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          await _incrementCounter();
        },
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
