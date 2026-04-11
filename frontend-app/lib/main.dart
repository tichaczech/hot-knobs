import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';
import 'package:provider/provider.dart';

import 'config/dependencies.dart';
import 'routes/router.dart';
import 'ui/auth/l10n/localizations.dart';
import 'ui/chat/l10n/localizations.dart';
import 'ui/core/l10n/localizations.dart';
import 'ui/dashboard/l10n/localizations.dart';
import 'ui/core/themes/theme.dart';
import 'ui/events/l10n/localizations.dart';
import 'ui/organizers/l10n/localizations.dart';
import 'ui/participants/l10n/localizations.dart';
import 'ui/profile/l10n/localizations.dart';
import 'ui/registrations/l10n/localizations.dart';
import 'ui/sites/l10n/localizations.dart';
import 'utils/auth_provider.dart';
import 'utils/config_provider.dart';

void main() async {
  Logger.root.level = Level.ALL;
  Logger.root.onRecord.listen((record) {
    // ignore: avoid_print
    print('${record.level.name}: ${record.time}: ${record.loggerName}: ${record.message}');
  });

  WidgetsFlutterBinding.ensureInitialized();

  final configProvider = ConfigProvider();
  final authConfig = await configProvider.getConfiguration(name: 'msal');

  log('API configuration: $authConfig');

  final authService = AuthProvider(
    androidConfigPath: 'assets/config/msal.dev.json', // FIXME: This should be dynamic based on the environment, but for simplicity, it's hardcoded here.
    androidRedirectUri: authConfig['redirect_uri'],
    authority: authConfig['authorities'][0]['authority_url'],
    clientId: authConfig['client_id'],
    broker: Broker.values.firstWhere((b) => b.toString().split('.').last.toUpperCase() == authConfig['authorization_user_agent'].toString().toUpperCase(), orElse: () => Broker.webView),
  );

  // final user = await authService.signOut();
  // final user = await authService.getCurrentUser();

  // log('Current user: ${user?.id}, ${user?.displayName}, ${user?.email}');

  // // Initialize storage (call once per app lifetime)
  // // await RestApiClient.initFlutter();

  // // Create the client
  // final client = RestApiClientImpl(options: RestApiClientOptions());
  // await client.init();

  // final result = await client.get('https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m');
  // final body = result.response?.data;

  // Logger.root.info('API response: $body');

  final providers = Providers.get(configProvider: configProvider, authProvider: authService, localServiceType: LocalServiceType.dummy, remoteServiceType: RemoteServiceType.firebase);
  runApp(MultiProvider(providers: providers, child: Application()));
}

class Application extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _Application();
}

class _Application extends State<Application> {
  // // In this example, suppose that all messages contain a data field with the key 'type'.
  // Future<void> setupInteractedMessage() async {
  //   // Get any messages which caused the application to open from a terminated state.
  //   RemoteMessage? initialMessage = await FirebaseMessaging.instance.getInitialMessage();

  //   // If the message also contains a data property with a "type" of "chat", navigate to a chat screen
  //   if (initialMessage != null) {
  //     _handleMessage(initialMessage);
  //   }

  //   // Also handle any interaction when the app is in the background via a Stream listener
  //   FirebaseMessaging.onMessageOpenedApp.listen(_handleMessage);
  // }

  // void _handleMessage(RemoteMessage message) {
  //   Logger.root.info('Handling a message: ${message.messageId}');
  //   Logger.root.info('Message data: ${message.data}');
  // }

  @override
  void initState() {
    super.initState();

    // // Run code required to handle interacted messages in an async function as initState() must not be async
    // setupInteractedMessage();
  }

  // TODO: Improve localization initialization, see https://medium.com/@alexey.yu.popkov/flutter-tip-organize-your-localization-with-multiple-arb-files-per-language-52606e5dbf25
  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      darkTheme: AppTheme.darkTheme,
      localizationsDelegates: [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
        CoreLocalizations.delegate,
        AuthLocalizations.delegate,
        DashboardLocalizations.delegate,
        ChatLocalizations.delegate,
        EventsLocalizations.delegate,
        OrganizersLocalizations.delegate,
        ParticipantsLocalizations.delegate,
        ProfileLocalizations.delegate,
        RegistrationsLocalizations.delegate,
        SitesLocalizations.delegate
      ],
      routerConfig: router,
      supportedLocales: CoreLocalizations.supportedLocales,
      title: 'Hot Knobs',
      theme: AppTheme.lightTheme,
      themeMode: ThemeMode.system,
    );
  }
}
