import 'dart:developer';
import 'dart:io';

import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';
import 'package:provider/provider.dart';

import './firebase_options.dart';
import 'routes/router.dart';
import 'providers/auth_provider.dart';
import 'providers/config_provider.dart';
import 'ui/core/l10n/core_localizations.dart';
import 'ui/core/themes/theme.dart';
import 'config/dependencies.dart';
import 'ui/sites/l10n/sites_localizations.dart';

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
    broker: Broker.values.firstWhere((b) => b.toString().split('.').last.toUpperCase() == authConfig['authorization_user_agent'].toString().toUpperCase(), orElse: () => Broker.webView)
  );

  // final user = await authService.getCurrentUser();

  // log('Current user: ${user?.id}, ${user?.displayName}, ${user?.email}');

  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);

  // final notificationSettings = await FirebaseMessaging.instance.requestPermission(alert: true, announcement: true, badge: true, carPlay: true, criticalAlert: false, providesAppNotificationSettings: false, provisional: true, sound: true);

  // if (Platform.isIOS) {
  //   var apnsToken = await FirebaseMessaging.instance.getAPNSToken();
  //   if (apnsToken == null) {
  //     await Future<void>.delayed(const Duration(seconds: 3));
  //     apnsToken = await FirebaseMessaging.instance.getAPNSToken();
  //   }
  //   Logger.root.info('APNs token: $apnsToken');
  // }
  // final fcmToken = await FirebaseMessaging.instance.getToken();
  // Logger.root.info('FCM token: $fcmToken');
  // Logger.root.info('Notification settings: $notificationSettings');

  // final publicClientApplication = await SingleAccountPca.create(
  //   clientId: 'e19c3441-3d78-49ae-917a-43b3c32852ad',
  //   androidConfig: AndroidConfig(
  //     configFilePath: 'assets/msal-config.json',
  //     redirectUri: 'msauth://app.hotknobs.v1/ghCPg%2FgSZi0PrA1gFfvCh09Cs6E%3D'
  //   ),
  //   appleConfig: AppleConfig(
  //     authority: 'https://hotknobsdev.ciamlogin.com/099336d3-2e34-4bf2-be23-a507e86cee6e',
  //     // Change authority type to 'b2c' for business to customer flow.
  //     authorityType: AuthorityType.b2c,
  //     // Change broker if you need. Applicable only for iOS platform.
  //     broker: Broker.webView,
  //   ),
  // );

  // final authResult = await publicClientApplication.acquireToken(
  //   scopes: <String>[
  //     'https://graph.microsoft.com/user.read',
  //     // 'https://graph.microsoft.com/email',
  //     // 'https://graph.microsoft.com/offline_access',
  //     // 'https://graph.microsoft.com/openid',
  //     // 'https://graph.microsoft.com/profile',
  //     // 'https://graph.microsoft.us/.default'
  //     // 'email',
  //     // 'offline_access',
  //     // 'openid',
  //     // 'profile',
  //     // '.default' // Use '.default' to request all the scopes that are configured for the application in the portal.
  //     // Add other scopes here if required.
  //   ],
  //   // UI option for authentication, default is [Prompt.whenRequired]
  //   prompt: Prompt.whenRequired,
  //   // Provide 'loginHint' if you have.
  //   // loginHint: '<Email Id / Username / Unique Identifier>',
  //   // Optional: Custom authority URL for B2C or different tenant scenarios
  //   // authority: 'https://hotknobsdev.ciamlogin.com/099336d3-2e34-4bf2-be23-a507e86cee6e',
  // );

  // log('ID token: ${authResult.idToken}');
  // log('Access token: ${authResult.accessToken}');
  // log('Auth result: ${authResult.toJson()}');

  final providers = Providers.get(configProvider: configProvider, authProvider: authService, localServiceType: LocalServiceType.dummy, remoteServiceType: RemoteServiceType.firebase);
  runApp(MultiProvider(providers: providers, child: Application()));
}

class Application extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _Application();
}

class _Application extends State<Application> {
  // In this example, suppose that all messages contain a data field with the key 'type'.
  Future<void> setupInteractedMessage() async {
    // Get any messages which caused the application to open from
    // a terminated state.
    RemoteMessage? initialMessage = await FirebaseMessaging.instance.getInitialMessage();

    // If the message also contains a data property with a "type" of "chat",
    // navigate to a chat screen
    if (initialMessage != null) {
      _handleMessage(initialMessage);
    }

    // Also handle any interaction when the app is in the background via a
    // Stream listener
    FirebaseMessaging.onMessageOpenedApp.listen(_handleMessage);
  }

  void _handleMessage(RemoteMessage message) {
    Logger.root.info('Handling a message: ${message.messageId}');
    Logger.root.info('Message data: ${message.data}');
  }

  @override
  void initState() {
    super.initState();

    // Run code required to handle interacted messages in an async function
    // as initState() must not be async
    setupInteractedMessage();
  }

  // TODO: Improve localization initialization, see https://medium.com/@alexey.yu.popkov/flutter-tip-organize-your-localization-with-multiple-arb-files-per-language-52606e5dbf25
  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      darkTheme: AppTheme.darkTheme,
      localizationsDelegates: [GlobalMaterialLocalizations.delegate, GlobalWidgetsLocalizations.delegate, GlobalCupertinoLocalizations.delegate, CoreLocalizations.delegate, SitesLocalizations.delegate],
      routerConfig: router,
      supportedLocales: CoreLocalizations.supportedLocales,
      title: 'Hot Knobs',
      theme: AppTheme.lightTheme,
      themeMode: ThemeMode.system,
    );
  }
}
