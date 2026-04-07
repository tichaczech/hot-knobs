import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:frontend/providers/auth_provider.dart';
import 'package:frontend/providers/config_provider.dart';
import 'package:path/path.dart' as path;
import 'package:path_provider/path_provider.dart';
import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';
import 'package:reaxdb_dart/reaxdb_dart.dart';

import '../data/repositories/operator_repository.dart';
import '../data/repositories/site_repository.dart';
import '../data/repositories/user_profile_repository.dart';
import '../data/services/local/local_service.dart';
import '../data/services/local/operator_dummy_service.dart';
import '../data/services/local/operator_reaxdb_service.dart';
import '../data/services/local/site_dummy_service.dart';
import '../data/services/local/user_profile_dummy_service.dart';
import '../data/services/remote/operator_firebase_service.dart';
import '../data/services/remote/site_firebase_service.dart';
import '../data/services/remote/remote_service.dart';
import '../data/services/remote/user_profile_firebase_service.dart';
import '../domain/models/operator.dart';
import '../domain/models/site.dart';
import '../domain/models/user_profile.dart';
import '../domain/use_cases/user_profile.dart';

enum LocalServiceType { dummy, reaxdb }

enum RemoteServiceType { firebase }

class Providers {
  static List<SingleChildWidget> get({required ConfigProvider configProvider, required AuthProvider authProvider, LocalServiceType localServiceType = LocalServiceType.reaxdb, RemoteServiceType remoteServiceType = RemoteServiceType.firebase}) {
    final localProviders = _localProviders[localServiceType];
    if (localProviders == null) {
      throw Exception('Local providers not found');
    }

    final remoteProviders = _remoteProviders[remoteServiceType];
    if (remoteProviders == null) {
      throw Exception('Remote providers not found');
    }

    final cs = Provider<ConfigProvider>.value(value: configProvider);
    final as = Provider<AuthProvider>.value(value: authProvider);

    return [...localProviders, ...remoteProviders, ..._sharedProviders, cs, as];
  }
}

/// Shared providers for all configurations.
List<SingleChildWidget> _sharedProviders = [
  // Firebase
  Provider<FirebaseMessaging>.value(value: FirebaseMessaging.instance),
  // Repositories
  Provider<OperatorRepository>(
    create: (context) => OperatorRepository(localService: context.read(), remoteService: context.read()),
  ),
  Provider<SiteRepository>(
    create: (context) => SiteRepository(localService: context.read(), remoteService: context.read()),
  ),
  Provider<UserProfileRepository>(
    create: (context) => UserProfileRepository(localService: context.read(), remoteService: context.read()),
  ),
  // UseCases
  Provider<UserProfileUseCases>(
    create: (context) => UserProfileUseCases(firebaseMessaging: FirebaseMessaging.instance, userProfileRepository: context.read()),
  ),
];

Map<LocalServiceType, List<SingleChildWidget>> _localProviders = {
  LocalServiceType.dummy: [
    // Dummy
    Provider<LocalService<Operator>>(create: (context) => OperatorDummyService()),
    Provider<LocalService<Site>>(create: (context) => SiteDummyService()),
    Provider<LocalService<UserProfile>>(create: (context) => UserProfileDummyService()),
  ],
  LocalServiceType.reaxdb: [
    // ReaxDB
    FutureProvider<SimpleReaxDB?>(
      create: (context) async {
        final cacheDir = await getApplicationCacheDirectory();
        final dbPath = path.join(cacheDir.path, 'db', 'v1');

        return await ReaxDB.simple(dbPath);
      },
      initialData: null,
    ),
    // Local Services
    Provider<LocalService<Operator>>(create: (context) => OperatorReaxDBService(db: context.read<SimpleReaxDB?>()!)),
    // Provider<LocalService<Site>>(create: (context) => SiteReaxDBService(db: context.read<SimpleReaxDB?>()!)),
    // Provider<LocalService<UserProfile>>(create: (context) => UserProfileReaxDBService(db: context.read<SimpleReaxDB?>()!)),
  ],
};

Map<RemoteServiceType, List<SingleChildWidget>> _remoteProviders = {
  RemoteServiceType.firebase: [
    // Firebase Firestore
    Provider<FirebaseFirestore>.value(value: FirebaseFirestore.instance),
    // Remote Services
    Provider<RemoteService<Operator, OperatorCreateModel, OperatorUpdateModel>>(create: (context) => OperatorFirebaseService(firestore: context.read())),
    Provider<RemoteService<Site, SiteCreateModel, SiteUpdateModel>>(create: (context) => SiteFirebaseService(firestore: context.read())),
    Provider<RemoteService<UserProfile, UserProfileCreateModel, UserProfileUpdateModel>>(create: (context) => UserProfileFirebaseService(firestore: context.read())),
  ],
};
