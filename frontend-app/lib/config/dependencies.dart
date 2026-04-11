import 'package:provider/provider.dart';
import 'package:provider/single_child_widget.dart';

import '../utils/auth_provider.dart';
import '../utils/config_provider.dart';

enum LocalServiceType { dummy, reaxdb, isar }

enum RemoteServiceType { firebase, restApi }

class Providers {
  static List<SingleChildWidget> get({required ConfigProvider configProvider, required AuthProvider authProvider, LocalServiceType localServiceType = LocalServiceType.reaxdb, RemoteServiceType remoteServiceType = RemoteServiceType.firebase}) {
    final cs = Provider<ConfigProvider>.value(value: configProvider);
    final as = Provider<AuthProvider>.value(value: authProvider);

    return [cs, as];
  }
}
