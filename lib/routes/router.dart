import 'dart:async';

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:provider/provider.dart';

import '../ui/auth/view_models/sign_in.dart';
import '../ui/auth/widgets/sign_in.dart';
import '../ui/chat/widgets/home.dart';
import '../ui/dashboard/widgets/home.dart';
import '../ui/events/widgets/edit.dart';
import '../ui/events/widgets/home.dart';
import '../ui/events/widgets/view.dart';
import '../ui/organizers/widgets/edit.dart';
import '../ui/organizers/widgets/home.dart';
import '../ui/organizers/widgets/view.dart';
import '../ui/participants/widgets/home.dart';
import '../ui/profile/widgets/edit.dart';
import '../ui/profile/widgets/home.dart';
import '../ui/profile/widgets/view.dart';
import '../ui/registrations/widgets/edit.dart';
import '../ui/registrations/widgets/home.dart';
import '../ui/registrations/widgets/view.dart';
import '../ui/sites/widgets/edit.dart';
import '../ui/sites/widgets/home.dart';
import '../ui/sites/widgets/view.dart';
import '../widgets/app_shell.dart';
import 'routes.dart';
import '../utils/auth_provider.dart';

final _rootNavigatorKey = GlobalKey<NavigatorState>();
final _shellNavigatorKey = GlobalKey<NavigatorState>();

final router = GoRouter(
  debugLogDiagnostics: true,
  errorBuilder: (context, state) => Scaffold(body: Center(child: Text('Error: ${state.error}'))),
  initialLocation: Routes.dashboard.path,
  navigatorKey: _rootNavigatorKey,
  redirect: (BuildContext context, GoRouterState state) async {
    final authProvider = context.read<AuthProvider>();
    final currentUser = await authProvider.getCurrentUser();

    if (currentUser == null) {
      // If the user is not signed in, redirect to the sign-in page.
      // TODO: Improve this logic to allow access to certain routes without authentication (e.g., sign-up, password reset) and to handle redirection back to the originally requested page after successful sign-in.
      // TODO: Handle the case where the user is already on the sign-in page to avoid redirect loops.
      return Routes.authSignIn.path;
    } else {
      return null;
    }
  },
  routes: [
    // Auth
    GoRoute(
      name: Routes.authSignIn.name,
      path: Routes.authSignIn.path,
      builder: (context, state) {
        final viewModel = SignInViewModel(authProvider: context.read() /*, userProfileUseCases: context.read()*/);
        return SignInScreen(viewModel: viewModel);
      },
    ),

    // Main shell with NavigationBar
    ShellRoute(
      builder: (context, state, child) => AppShell(child: child),
      navigatorKey: _shellNavigatorKey,
      routes: [
        // Chat
        GoRoute(name: Routes.chat.name, parentNavigatorKey: _shellNavigatorKey, path: Routes.chat.path, builder: (context, state) => const ChatsHomeScreen()),

        // Dashboard
        GoRoute(
          name: Routes.dashboard.name,
          parentNavigatorKey: _shellNavigatorKey,
          path: Routes.dashboard.path,
          builder: (context, state) => const DashboardHomeScreen(),
          routes: [
            // Organizers
            GoRoute(
              name: Routes.organizers.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.organizers.path,
              builder: (context, state) => const OrganizersHomeScreen(),
              routes: [
                GoRoute(
                  name: Routes.organizerView.name,
                  parentNavigatorKey: _rootNavigatorKey,
                  path: Routes.organizerView.path,
                  builder: (context, state) {
                    final organizerId = state.pathParameters['id'];
                    return OrganizerViewScreen();
                  },
                ),
                GoRoute(
                  name: Routes.organizerEdit.name,
                  parentNavigatorKey: _rootNavigatorKey,
                  path: Routes.organizerEdit.path,
                  builder: (context, state) {
                    final organizerId = state.pathParameters['id'];
                    return OrganizerEditScreen();
                  },
                ),
              ],
            ),
            // Sites
            GoRoute(
              name: Routes.sites.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.sites.path,
              builder: (context, state) => const SitesHomeScreen(),
              routes: [
                GoRoute(
                  name: Routes.siteView.name,
                  parentNavigatorKey: _rootNavigatorKey,
                  path: Routes.siteView.path,
                  builder: (context, state) {
                    final siteId = state.pathParameters['id'];
                    return SiteViewScreen();
                  },
                ),
                GoRoute(
                  name: Routes.siteEdit.name,
                  parentNavigatorKey: _rootNavigatorKey,
                  path: Routes.siteEdit.path,
                  builder: (context, state) {
                    final siteId = state.pathParameters['id'];
                    return SiteEditScreen();
                  },
                ),
              ],
            ),
          ],
        ),

        // Events
        GoRoute(
          name: Routes.events.name,
          parentNavigatorKey: _shellNavigatorKey,
          path: Routes.events.path,
          builder: (context, state) => const EventsHomeScreen(),
          routes: [
            GoRoute(
              name: Routes.eventView.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.eventView.path,
              builder: (context, state) {
                final eventId = state.pathParameters['id'];
                return EventViewScreen();
              },
            ),
            GoRoute(
              name: Routes.eventEdit.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.eventEdit.path,
              builder: (context, state) {
                final eventId = state.pathParameters['id'];
                return EventEditScreen();
              },
            ),
          ],
        ),

        // Participants
        GoRoute(name: Routes.participants.name, path: Routes.participants.path, builder: (context, state) => const ParticipantsHomeScreen()),
        // GoRoute(
        //   name: Routes.participantView.name,
        //   path: Routes.participantView.path,
        //   builder: (context, state) {
        //     final participantId = state.pathParameters['id'];
        //     return ParticipantViewScreen();
        //   },
        // ),
        // GoRoute(
        //   name: Routes.participantEdit.name,
        //   path: Routes.participantEdit.path,
        //   builder: (context, state) {
        //     final participantId = state.pathParameters['id'];
        //     return ParticipantEditScreen();
        //   },
        // ),

        // Profile
        GoRoute(
          name: Routes.profile.name,
          parentNavigatorKey: _shellNavigatorKey,
          path: Routes.profile.path,
          builder: (context, state) => const ProfileHomeScreen(),
          routes: [
            GoRoute(
              name: Routes.profileView.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.profileView.path,
              builder: (context, state) {
                final profileId = state.pathParameters['id'];
                return ProfileViewScreen();
              },
            ),
            GoRoute(
              name: Routes.profileEdit.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.profileEdit.path,
              builder: (context, state) {
                final profileId = state.pathParameters['id'];
                return ProfileEditScreen();
              },
            ),
          ],
        ),

        // Registrations
        GoRoute(
          name: Routes.registrations.name,
          parentNavigatorKey: _shellNavigatorKey,
          path: Routes.registrations.path,
          builder: (context, state) => const RegistrationsHomeScreen(),
          routes: [
            GoRoute(
              name: Routes.registrationView.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.registrationView.path,
              builder: (context, state) {
                final registrationId = state.pathParameters['id'];
                return RegistrationViewScreen();
              },
            ),
            GoRoute(
              name: Routes.registrationEdit.name,
              parentNavigatorKey: _rootNavigatorKey,
              path: Routes.registrationEdit.path,
              builder: (context, state) {
                final registrationId = state.pathParameters['id'];
                return RegistrationEditScreen();
              },
            ),
          ],
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
