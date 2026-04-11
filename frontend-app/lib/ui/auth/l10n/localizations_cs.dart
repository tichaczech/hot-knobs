// ignore: unused_import
import 'package:intl/intl.dart' as intl;
import 'localizations.dart';

// ignore_for_file: type=lint

/// The translations for Czech (`cs`).
class AuthLocalizationsCs extends AuthLocalizations {
  AuthLocalizationsCs([String locale = 'cs']) : super(locale);

  @override
  String get authFormSignInButton => 'Přihlásit';

  @override
  String get authFormSignUpButton => 'Registrovat';

  @override
  String get authSignInCancelledByUser => 'Přihlášení zrušeno uživatelem...';

  @override
  String authSignInError(Object error) {
    return 'Chyba při přihlašování: $error';
  }

  @override
  String get authSignInOrSignUpToContinue => 'Než budete moci pokračovat, tak se musíte přihlásit nebo zaregistrovat...';

  @override
  String get authSignUpCancelledByUser => 'Registrace zrušena uživatelem...';

  @override
  String authSignUpError(Object error) {
    return 'Chyba při registraci: $error';
  }

  @override
  String get authScreenName => 'Přihlásit / Registrovat';
}
