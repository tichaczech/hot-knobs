// ignore: unused_import
import 'package:intl/intl.dart' as intl;
import 'localizations.dart';

// ignore_for_file: type=lint

/// The translations for English (`en`).
class AuthLocalizationsEn extends AuthLocalizations {
  AuthLocalizationsEn([String locale = 'en']) : super(locale);

  @override
  String get authFormSignInButton => 'Sign In';

  @override
  String get authFormSignUpButton => 'Sign Up';

  @override
  String get authSignInCancelledByUser => 'Sign in cancelled by user...';

  @override
  String authSignInError(Object error) {
    return 'Error signing in: $error';
  }

  @override
  String get authSignInOrSignUpToContinue => 'You need to sign in or sign up before you can continue...';

  @override
  String get authSignUpCancelledByUser => 'Sign up cancelled by user...';

  @override
  String authSignUpError(Object error) {
    return 'Error signing up: $error';
  }

  @override
  String get authScreenName => 'Sign In / Sign Up';
}
