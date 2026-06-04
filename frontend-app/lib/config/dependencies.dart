import 'package:device_info_plus/device_info_plus.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:frontend/data/services/remote/users/profile_restapi_service.dart';
import 'package:path/path.dart' as path;
import 'package:path_provider/path_provider.dart';
import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';
import 'package:reaxdb_dart/reaxdb_dart.dart';

import '../data/repositories/users/device_repository.dart';
import '../data/repositories/users/profile_repository.dart';
import '../data/services/local/local_service.dart';
import '../data/services/local/users/device_dummy_service.dart';
import '../data/services/local/users/device_reaxdb_service.dart';
import '../data/services/local/users/profile_dummy_service.dart';
import '../data/services/local/users/profile_reaxdb_service.dart';
import '../data/services/remote/remote_service.dart';
import '../data/services/remote/users/device_restapi_service.dart';
import '../domain/models/users/device.dart';
import '../domain/models/users/profile.dart';
import '../domain/use_cases/auth.dart';
import '../utils/auth_provider.dart';
import '../utils/config_provider.dart';

enum LocalServiceType { dummy, reaxdb, isar }

enum RemoteServiceType { firebase, restApi }

class Providers {
  static List<SingleChildWidget> get({
    required AuthProvider authProvider,
    required ConfigProvider configProvider,
    required FirebaseMessaging firebaseMessaging,
    LocalServiceType localServiceType = LocalServiceType.reaxdb,
    RemoteServiceType remoteServiceType = RemoteServiceType.firebase,
  }) {
    final list = List<SingleChildWidget>.empty(growable: true);

    list.add(Provider<AuthProvider>.value(value: authProvider));
    list.add(Provider<ConfigProvider>.value(value: configProvider));
    list.add(Provider<DeviceInfoPlugin>(create: (_) => DeviceInfoPlugin()));
    list.add(Provider<FirebaseMessaging>.value(value: firebaseMessaging));

    // Services
    list.addAll(_localServices[localServiceType] ?? []);
    list.addAll(_remoteServices[remoteServiceType] ?? []);

    // Repositories
    list.add(
      Provider<DeviceRepository>(
        create: (context) => DeviceRepository(localService: context.read(), remoteService: context.read()),
      ),
    );
    list.add(
      Provider<ProfileRepository>(
        create: (context) => ProfileRepository(remoteService: context.read(), localService: context.read()),
      ),
    );

    // UseCases
    list.add(
      Provider<AuthUseCases>(
        create: (context) => AuthUseCases(deviceInfoPlugin: context.read(), deviceRepository: context.read(), firebaseMessaging: context.read(), profileRepository: context.read()),
      ),
    );

    return list;
  }

  static Map<LocalServiceType, List<SingleChildWidget>> _localServices = {
    LocalServiceType.dummy: [
      // Dummy services
      Provider<LocalService<Device>>(create: (context) => DeviceDummyService()),
      Provider<LocalService<Profile>>(create: (context) => ProfileDummyService()),
    ],
    LocalServiceType.reaxdb: [
      // ReaxDB services
      FutureProvider<SimpleReaxDB?>(
        create: (context) async {
          final cacheDir = await getApplicationCacheDirectory();
          final dbPath = path.join(cacheDir.path, 'db', 'v1');

          return await ReaxDB.simple(dbPath);
        },
        initialData: null,
      ),
      // Local Services
      Provider<LocalService<Device>>(create: (context) => DeviceReaxDBService(db: context.read<SimpleReaxDB?>()!)),
      Provider<LocalService<Profile>>(create: (context) => ProfileReaxDBService(db: context.read<SimpleReaxDB?>()!)),
    ],
  };

  static Map<RemoteServiceType, List<SingleChildWidget>> _remoteServices = {
    RemoteServiceType.firebase: [
      // Firebase services
      // Provider<RemoteService<Device>>(create: (context) => DeviceFirebaseService(authProvider: context.read(), configProvider: context.read(), log: Logger('Data:Services:Remote:DeviceFirebaseService'))),
      // Provider<RemoteService<Profile>>(create: (context) => ProfileFirebaseService(authProvider: context.read(), configProvider: context.read(), log: Logger('Data:Services:Remote:ProfileFirebaseService'))),
    ],
    RemoteServiceType.restApi: [
      // REST API Services
      Provider<RemoteService<Device, DeviceCreateModel, DeviceUpdateModel>>(
        create: (context) => DeviceRestAPIService(authProvider: context.read(), configProvider: context.read()),
      ),
      Provider<RemoteService<Profile, ProfileCreateModel, ProfileUpdateModel>>(
        create: (context) => ProfileRestAPIService(authProvider: context.read(), configProvider: context.read()),
      ),
    ],
  };
}
