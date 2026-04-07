import '../domain/models/types.dart';

enum Routes implements TranslatableEnum {
  // Auth
  authProfile(path: '/auth/profile'),
  authSignIn(path: '/auth/sign-in'),

  // Dashboard
  dashboard(path: '/'),

  // Events
  events(path: '/events'),

  // Operators
  operators(path: '/operators'),

  // Participants
  participants(path: '/participants'),

  // Registrations
  registrations(path: '/registrations'),

  // Sites
  sites(path: '/sites'),
  siteView(path: '/sites/:id'),
  siteEdit(path: '/sites/:id/edit');

  const Routes({required this.path});

  final String path;
  
  @override
  String get displayName => switch (this) {
    Routes.authProfile => 'Profile',
    Routes.authSignIn => 'Sign In',
    Routes.dashboard => 'Dashboard',
    Routes.events => 'Events',
    Routes.operators => 'Operators',
    Routes.participants => 'Participants',
    Routes.registrations => 'Registrations',
    Routes.sites => 'Sites',
    Routes.siteView => 'Site View',
    Routes.siteEdit => 'Site Edit',
  };
}
