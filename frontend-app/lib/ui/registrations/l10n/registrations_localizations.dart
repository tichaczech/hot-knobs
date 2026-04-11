import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:intl/intl.dart' as intl;

import 'registrations_localizations_cs.dart';
import 'registrations_localizations_en.dart';

// ignore_for_file: type=lint

/// Callers can lookup localized strings with an instance of RegistrationsLocalizations
/// returned by `RegistrationsLocalizations.of(context)`.
///
/// Applications need to include `RegistrationsLocalizations.delegate()` in their app's
/// `localizationDelegates` list, and the locales they support in the app's
/// `supportedLocales` list. For example:
///
/// ```dart
/// import 'l10n/registrations_localizations.dart';
///
/// return MaterialApp(
///   localizationsDelegates: RegistrationsLocalizations.localizationsDelegates,
///   supportedLocales: RegistrationsLocalizations.supportedLocales,
///   home: MyApplicationHome(),
/// );
/// ```
///
/// ## Update pubspec.yaml
///
/// Please make sure to update your pubspec.yaml to include the following
/// packages:
///
/// ```yaml
/// dependencies:
///   # Internationalization support.
///   flutter_localizations:
///     sdk: flutter
///   intl: any # Use the pinned version from flutter_localizations
///
///   # Rest of dependencies
/// ```
///
/// ## iOS Applications
///
/// iOS applications define key application metadata, including supported
/// locales, in an Info.plist file that is built into the application bundle.
/// To configure the locales supported by your app, you’ll need to edit this
/// file.
///
/// First, open your project’s ios/Runner.xcworkspace Xcode workspace file.
/// Then, in the Project Navigator, open the Info.plist file under the Runner
/// project’s Runner folder.
///
/// Next, select the Information Property List item, select Add Item from the
/// Editor menu, then select Localizations from the pop-up menu.
///
/// Select and expand the newly-created Localizations item then, for each
/// locale your application supports, add a new item and select the locale
/// you wish to add from the pop-up menu in the Value field. This list should
/// be consistent with the languages listed in the RegistrationsLocalizations.supportedLocales
/// property.
abstract class RegistrationsLocalizations {
  RegistrationsLocalizations(String locale) : localeName = intl.Intl.canonicalizedLocale(locale.toString());

  final String localeName;

  static RegistrationsLocalizations? of(BuildContext context) {
    return Localizations.of<RegistrationsLocalizations>(context, RegistrationsLocalizations);
  }

  static const LocalizationsDelegate<RegistrationsLocalizations> delegate = _RegistrationsLocalizationsDelegate();

  /// A list of this localizations delegate along with the default localizations
  /// delegates.
  ///
  /// Returns a list of localizations delegates containing this delegate along with
  /// GlobalMaterialLocalizations.delegate, GlobalCupertinoLocalizations.delegate,
  /// and GlobalWidgetsLocalizations.delegate.
  ///
  /// Additional delegates can be added by appending to this list in
  /// MaterialApp. This list does not have to be used at all if a custom list
  /// of delegates is preferred or required.
  static const List<LocalizationsDelegate<dynamic>> localizationsDelegates = <LocalizationsDelegate<dynamic>>[
    delegate,
    GlobalMaterialLocalizations.delegate,
    GlobalCupertinoLocalizations.delegate,
    GlobalWidgetsLocalizations.delegate,
  ];

  /// A list of this localizations delegate's supported locales.
  static const List<Locale> supportedLocales = <Locale>[
    Locale('cs'),
    Locale('en')
  ];

  /// No description provided for @registrationsScreenName.
  ///
  /// In en, this message translates to:
  /// **'Registrations'**
  String get registrationsScreenName;

  /// No description provided for @registrationViewScreenName.
  ///
  /// In en, this message translates to:
  /// **'View Registration'**
  String get registrationViewScreenName;

  /// No description provided for @registrationEditScreenName.
  ///
  /// In en, this message translates to:
  /// **'Edit Registration'**
  String get registrationEditScreenName;
}

class _RegistrationsLocalizationsDelegate extends LocalizationsDelegate<RegistrationsLocalizations> {
  const _RegistrationsLocalizationsDelegate();

  @override
  Future<RegistrationsLocalizations> load(Locale locale) {
    return SynchronousFuture<RegistrationsLocalizations>(lookupRegistrationsLocalizations(locale));
  }

  @override
  bool isSupported(Locale locale) => <String>['cs', 'en'].contains(locale.languageCode);

  @override
  bool shouldReload(_RegistrationsLocalizationsDelegate old) => false;
}

RegistrationsLocalizations lookupRegistrationsLocalizations(Locale locale) {


  // Lookup logic when only language code is specified.
  switch (locale.languageCode) {
    case 'cs': return RegistrationsLocalizationsCs();
    case 'en': return RegistrationsLocalizationsEn();
  }

  throw FlutterError(
    'RegistrationsLocalizations.delegate failed to load unsupported locale "$locale". This is likely '
    'an issue with the localizations generation tool. Please file an issue '
    'on GitHub with a reproducible sample app and the gen-l10n configuration '
    'that was used.'
  );
}
