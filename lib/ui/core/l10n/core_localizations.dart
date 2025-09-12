import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:intl/intl.dart' as intl;

import 'core_localizations_cs.dart';
import 'core_localizations_en.dart';

// ignore_for_file: type=lint

/// Callers can lookup localized strings with an instance of CoreLocalizations
/// returned by `CoreLocalizations.of(context)`.
///
/// Applications need to include `CoreLocalizations.delegate()` in their app's
/// `localizationDelegates` list, and the locales they support in the app's
/// `supportedLocales` list. For example:
///
/// ```dart
/// import 'l10n/core_localizations.dart';
///
/// return MaterialApp(
///   localizationsDelegates: CoreLocalizations.localizationsDelegates,
///   supportedLocales: CoreLocalizations.supportedLocales,
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
/// be consistent with the languages listed in the CoreLocalizations.supportedLocales
/// property.
abstract class CoreLocalizations {
  CoreLocalizations(String locale) : localeName = intl.Intl.canonicalizedLocale(locale.toString());

  final String localeName;

  static CoreLocalizations? of(BuildContext context) {
    return Localizations.of<CoreLocalizations>(context, CoreLocalizations);
  }

  static const LocalizationsDelegate<CoreLocalizations> delegate = _CoreLocalizationsDelegate();

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

  /// No description provided for @dashboardScreenName.
  ///
  /// In en, this message translates to:
  /// **'Dashboard'**
  String get dashboardScreenName;

  /// No description provided for @dayOfWeekFriday.
  ///
  /// In en, this message translates to:
  /// **'Friday'**
  String get dayOfWeekFriday;

  /// No description provided for @dayOfWeekMonday.
  ///
  /// In en, this message translates to:
  /// **'Monday'**
  String get dayOfWeekMonday;

  /// No description provided for @dayOfWeekSaturday.
  ///
  /// In en, this message translates to:
  /// **'Saturday'**
  String get dayOfWeekSaturday;

  /// No description provided for @dayOfWeekSunday.
  ///
  /// In en, this message translates to:
  /// **'Sunday'**
  String get dayOfWeekSunday;

  /// No description provided for @dayOfWeekThursday.
  ///
  /// In en, this message translates to:
  /// **'Thursday'**
  String get dayOfWeekThursday;

  /// No description provided for @dayOfWeekTuesday.
  ///
  /// In en, this message translates to:
  /// **'Tuesday'**
  String get dayOfWeekTuesday;

  /// No description provided for @dayOfWeekWednesday.
  ///
  /// In en, this message translates to:
  /// **'Wednesday'**
  String get dayOfWeekWednesday;

  /// No description provided for @errorWhileLoadingData.
  ///
  /// In en, this message translates to:
  /// **'Error while loading data'**
  String get errorWhileLoadingData;

  /// No description provided for @eventsScreenName.
  ///
  /// In en, this message translates to:
  /// **'Events'**
  String get eventsScreenName;

  /// No description provided for @eventsScreenNearbyTab.
  ///
  /// In en, this message translates to:
  /// **'Nearby'**
  String get eventsScreenNearbyTab;

  /// No description provided for @eventsScreenPastTab.
  ///
  /// In en, this message translates to:
  /// **'Past'**
  String get eventsScreenPastTab;

  /// No description provided for @eventsScreenUpcomingTab.
  ///
  /// In en, this message translates to:
  /// **'Upcoming'**
  String get eventsScreenUpcomingTab;

  /// No description provided for @navigationDashboard.
  ///
  /// In en, this message translates to:
  /// **'Dashboard'**
  String get navigationDashboard;

  /// No description provided for @navigationEvents.
  ///
  /// In en, this message translates to:
  /// **'Events'**
  String get navigationEvents;

  /// No description provided for @navigationOperators.
  ///
  /// In en, this message translates to:
  /// **'Operators'**
  String get navigationOperators;

  /// No description provided for @navigationParticipants.
  ///
  /// In en, this message translates to:
  /// **'Participants'**
  String get navigationParticipants;

  /// No description provided for @navigationRegistrations.
  ///
  /// In en, this message translates to:
  /// **'Registrations'**
  String get navigationRegistrations;

  /// No description provided for @navigationSites.
  ///
  /// In en, this message translates to:
  /// **'Sites'**
  String get navigationSites;

  /// No description provided for @operatorsScreenName.
  ///
  /// In en, this message translates to:
  /// **'Operators'**
  String get operatorsScreenName;

  /// No description provided for @participantsScreenName.
  ///
  /// In en, this message translates to:
  /// **'Participants'**
  String get participantsScreenName;

  /// No description provided for @registrationsScreenConfirmedTab.
  ///
  /// In en, this message translates to:
  /// **'Confirmed'**
  String get registrationsScreenConfirmedTab;

  /// No description provided for @registrationsScreenName.
  ///
  /// In en, this message translates to:
  /// **'Registrations'**
  String get registrationsScreenName;

  /// No description provided for @registrationsScreenPastTab.
  ///
  /// In en, this message translates to:
  /// **'Past'**
  String get registrationsScreenPastTab;

  /// No description provided for @registrationsScreenWaitingTab.
  ///
  /// In en, this message translates to:
  /// **'Pending'**
  String get registrationsScreenWaitingTab;

  /// No description provided for @seasonAllYear.
  ///
  /// In en, this message translates to:
  /// **'All year'**
  String get seasonAllYear;

  /// No description provided for @seasonAutumn.
  ///
  /// In en, this message translates to:
  /// **'Autumn'**
  String get seasonAutumn;

  /// No description provided for @seasonSpring.
  ///
  /// In en, this message translates to:
  /// **'Spring'**
  String get seasonSpring;

  /// No description provided for @seasonSummer.
  ///
  /// In en, this message translates to:
  /// **'Summer'**
  String get seasonSummer;

  /// No description provided for @seasonWinter.
  ///
  /// In en, this message translates to:
  /// **'Winter'**
  String get seasonWinter;

  /// No description provided for @siteTypeEnduro.
  ///
  /// In en, this message translates to:
  /// **'Enduro'**
  String get siteTypeEnduro;

  /// No description provided for @siteTypeMotocross.
  ///
  /// In en, this message translates to:
  /// **'Motocross'**
  String get siteTypeMotocross;

  /// No description provided for @skillLevelFirstTimer.
  ///
  /// In en, this message translates to:
  /// **'First-timer'**
  String get skillLevelFirstTimer;

  /// No description provided for @skillLevelBeginner.
  ///
  /// In en, this message translates to:
  /// **'Beginner'**
  String get skillLevelBeginner;

  /// No description provided for @skillLevelIntermediate.
  ///
  /// In en, this message translates to:
  /// **'Intermediate'**
  String get skillLevelIntermediate;

  /// No description provided for @skillLevelAdvanced.
  ///
  /// In en, this message translates to:
  /// **'Advanced'**
  String get skillLevelAdvanced;

  /// No description provided for @skillLevelProfessional.
  ///
  /// In en, this message translates to:
  /// **'Professional'**
  String get skillLevelProfessional;

  /// No description provided for @tryAgain.
  ///
  /// In en, this message translates to:
  /// **'Try Again'**
  String get tryAgain;
}

class _CoreLocalizationsDelegate extends LocalizationsDelegate<CoreLocalizations> {
  const _CoreLocalizationsDelegate();

  @override
  Future<CoreLocalizations> load(Locale locale) {
    return SynchronousFuture<CoreLocalizations>(lookupCoreLocalizations(locale));
  }

  @override
  bool isSupported(Locale locale) => <String>['cs', 'en'].contains(locale.languageCode);

  @override
  bool shouldReload(_CoreLocalizationsDelegate old) => false;
}

CoreLocalizations lookupCoreLocalizations(Locale locale) {


  // Lookup logic when only language code is specified.
  switch (locale.languageCode) {
    case 'cs': return CoreLocalizationsCs();
    case 'en': return CoreLocalizationsEn();
  }

  throw FlutterError(
    'CoreLocalizations.delegate failed to load unsupported locale "$locale". This is likely '
    'an issue with the localizations generation tool. Please file an issue '
    'on GitHub with a reproducible sample app and the gen-l10n configuration '
    'that was used.'
  );
}
