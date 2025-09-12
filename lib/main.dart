import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:firebase_ui_localizations/firebase_ui_localizations.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:logging/logging.dart';
import 'package:provider/provider.dart';

import './firebase_options.dart';
import 'routes/router.dart';
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
  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);

  final notificationSettings = await FirebaseMessaging.instance.requestPermission(alert: true, announcement: true, badge: true, carPlay: true, criticalAlert: false, providesAppNotificationSettings: false, provisional: true, sound: true);
  final apnsToken = await FirebaseMessaging.instance.getAPNSToken();
  final fcmToken = await FirebaseMessaging.instance.getToken();
  Logger.root.info('APNs token: $apnsToken');
  Logger.root.info('FCM token: $fcmToken');
  Logger.root.info('Notification settings: $notificationSettings');

  final providers = Providers.get(localServiceType: LocalServiceType.dummy, remoteServiceType: RemoteServiceType.firebase);
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
      localizationsDelegates: [GlobalMaterialLocalizations.delegate, GlobalWidgetsLocalizations.delegate, GlobalCupertinoLocalizations.delegate, CoreLocalizations.delegate, SitesLocalizations.delegate, FirebaseUILocalizations.delegate],
      routerConfig: router,
      supportedLocales: CoreLocalizations.supportedLocales,
      title: 'Hot Knobs',
      theme: AppTheme.lightTheme,
      themeMode: ThemeMode.system,
    );
  }
}
