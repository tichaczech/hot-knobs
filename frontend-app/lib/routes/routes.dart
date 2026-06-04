import '../domain/models/types.dart';

enum Routes implements TranslatableEnum {
  // Auth
  authSignIn(path: '/auth/sign-in'),
  authSignUp(path: '/auth/sign-up'),
  authSignOut(path: '/auth/sign-out'),

  // Chat
  chat(path: '/chat'),

  // Dashboard
  dashboard(path: '/'),

  // Events
  events(path: '/events'),
  eventEdit(path: '/events/:id/edit'),
  eventView(path: '/events/:id'),

  // Organizers
  organizers(path: '/organizers'),
  organizerEdit(path: '/organizers/:id/edit'),
  organizerView(path: '/organizers/:id'),

  // Participants
  participants(path: '/participants'),
  participantEdit(path: '/participants/:id/edit'),
  participantView(path: '/participants/:id'),

  // Profile
  profile(path: '/profile'),
  profileEdit(path: '/profile/edit'),
  profileView(path: '/profile/view'),

  // Registrations
  registrations(path: '/registrations'),
  registrationEdit(path: '/registrations/:id/edit'),
  registrationView(path: '/registrations/:id'),

  // Sites
  sites(path: '/sites'),
  siteEdit(path: '/sites/:id/edit'),
  siteView(path: '/sites/:id');

  const Routes({required this.path});

  final String path;
  
  @override
  String get displayName => switch (this) {
    Routes.authSignIn => 'Sign In',
    Routes.authSignUp => 'Sign Up',
    Routes.authSignOut => 'Sign Out',
    Routes.chat => 'Chat',
    Routes.dashboard => 'Dashboard',
    Routes.events => 'Events',
    Routes.eventEdit => 'Event Edit',
    Routes.eventView => 'Event View',
    Routes.organizers => 'Organizers',
    Routes.organizerEdit => 'Organizer Edit',
    Routes.organizerView => 'Organizer View',
    Routes.participants => 'Participants',
    Routes.participantEdit => 'Participant Edit',
    Routes.participantView => 'Participant View',
    Routes.profile => 'Profile',
    Routes.profileEdit => 'Profile Edit',
    Routes.profileView => 'Profile View',
    Routes.registrations => 'Registrations',
    Routes.registrationEdit => 'Registration Edit',
    Routes.registrationView => 'Registration View',
    Routes.sites => 'Sites',
    Routes.siteEdit => 'Site Edit',
    Routes.siteView => 'Site View',
  };
}
