import 'dart:async';

import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:frontend/ui/operators/view_models/operators_viewmodel.dart';
import 'package:frontend/ui/operators/widgets/operators_screen.dart';
import 'package:frontend/ui/sites/view_models/sites_viewmodel.dart';
import 'package:frontend/ui/sites/widgets/sites_screen.dart';
import 'package:frontend/views/auth/profile.dart';
import 'package:frontend/views/auth/sign_in.dart';
import 'package:frontend/views/participants/home.dart';
import 'package:frontend/views/registrations/home.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';

import '../ui/sites/view_models/site_viewmodel.dart';
import '../ui/sites/widgets/site_edit_screen.dart';
import '../ui/sites/widgets/site_view_screen.dart';
import '../views/events/home.dart';
import '../views/dashboard/home.dart';
import 'routes.dart';

final router = GoRouter(
  debugLogDiagnostics: true,
  errorBuilder: (context, state) => Scaffold(body: Center(child: Text('Error: ${state.error}'))),
  initialLocation: Routes.dashboard.path,
  refreshListenable: GoRouterRefreshListenable(FirebaseAuth.instance.authStateChanges()),
  redirect: (BuildContext context, GoRouterState state) {
    final currentUser = FirebaseAuth.instance.currentUser;
    if (currentUser == null) {
      return Routes.authSignIn.path;
    } else {
      return null;
    }
  },
  routes: [
    // Auth
    GoRoute(path: Routes.authProfile.path, name: Routes.authProfile.name, builder: (context, state) => const Profile()),
    GoRoute(name: Routes.authSignIn.name, path: Routes.authSignIn.path, builder: (context, state) => SignIn()),

    // Dashboard
    GoRoute(name: Routes.dashboard.name, path: Routes.dashboard.path, builder: (context, state) => const DashboardHome()),

    // Events
    GoRoute(name: Routes.events.name, path: Routes.events.path, builder: (context, state) => const EventsHome()),

    // Operators
    GoRoute(
      name: Routes.operators.name,
      path: Routes.operators.path,
      builder: (context, state) {
        final viewModel = OperatorsViewModel(operatorRepository: context.read());
        return OperatorsScreen(viewModel: viewModel);
      },
    ),

    // Participants
    GoRoute(name: Routes.participants.name, path: Routes.participants.path, builder: (context, state) => const ParticipantsHome()),

    // Registrations
    GoRoute(name: Routes.registrations.name, path: Routes.registrations.path, builder: (context, state) => const RegistrationsHome()),

    // Sites
    GoRoute(
      name: Routes.sites.name,
      path: Routes.sites.path,
      builder: (context, state) {
        final viewModel = SitesViewModel(siteRepository: context.read());
        return SitesScreen(viewModel: viewModel);
      },
      routes: [
        GoRoute(
          name: Routes.siteView.name,
          path: Routes.siteView.path,
          builder: (context, state) {
            final siteId = state.pathParameters['id'];
            final viewModel = SiteViewModel(siteRepository: context.read(), id: siteId!);
            viewModel.load.execute(false);
            return SiteViewScreen(viewModel: viewModel);
          },
        ),
        GoRoute(
          name: Routes.siteEdit.name,
          path: Routes.siteEdit.path,
          builder: (context, state) {
            final siteId = state.pathParameters['id'];
            final viewModel = SiteViewModel(siteRepository: context.read(), id: siteId!);
            viewModel.load.execute(false);
            return SiteEditScreen(viewModel: viewModel);
          },
        ),
      ],
    ),
  ],
);

class GoRouterRefreshListenable extends ChangeNotifier {
  GoRouterRefreshListenable(Stream stream) {
    notifyListeners();
    _subscription = stream.asBroadcastStream().listen((_) {
      notifyListeners();
    });
  }

  late final StreamSubscription _subscription;

  @override
  void dispose() {
    _subscription.cancel();
    super.dispose();
  }
}
