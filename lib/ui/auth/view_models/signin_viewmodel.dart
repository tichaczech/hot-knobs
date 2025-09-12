import 'dart:io';

import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:logging/logging.dart';

import '../../../data/repositories/user_profile_repository.dart';
import '../../../domain/models/types.dart';
import '../../../domain/models/user_profile.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

class SignInViewModel {
  SignInViewModel({required this.firebaseAuth, required FirebaseMessaging firebaseMessaging, required UserProfileRepository userProfileRepository}) {
    _firebaseMessaging = firebaseMessaging;
    _log = Logger('SignInViewModel');
    _userProfileRepository = userProfileRepository;

    signedIn = Command1<void, User>(_signedIn);
    userCreated = Command1<void, User>(_userCreated);
  }

  late final FirebaseMessaging _firebaseMessaging;
  late final Logger _log;
  late final UserProfileRepository _userProfileRepository;

  late final FirebaseAuth firebaseAuth;
  late final Command1<void, User> signedIn;
  late final Command1<void, User> userCreated;

  Future<Result<void>> _signedIn(User user) async {
    final result = await _userProfileRepository.get(user.uid, forceRefresh: true);
    switch (result) {
      case Ok(value: final profile) when profile != null:
        _log.info('User profile loaded for user ${user.uid}');
        return Result.ok(null);
      case Ok():
        _log.info('User profile not found for user ${user.uid}, needs to be created');
        return Result.ok(null);
      case Err(exception: final e):
        _log.severe('Failed to load user profile for user ${user.uid}: $e');
        return Result.error(e);
    }
  }

  Future<Result<void>> _userCreated(User user) async {
    try {
      final apnsToken = Platform.isIOS ? await _firebaseMessaging.getAPNSToken() : null;
      if (Platform.isIOS && apnsToken == null) {
        final message = 'Failed to get APNS token for user ${user.uid} on iOS!';
        _log.warning(message);
        throw Exception(message);
      }

      final fcmToken = await _firebaseMessaging.getToken();
      if (fcmToken == null) {
        final message = 'Failed to get FCM token for user ${user.uid}!';
        _log.warning(message);
        throw Exception(message);
      }

      final createModel = UserProfileCreateModel(
        deviceRegistration: DeviceRegistration(apnsToken: apnsToken, fcmToken: fcmToken, name: Platform.localHostname, platform: Platform.operatingSystem),
        displayName: user.displayName,
        email: user.email!,
        phoneNumber: user.phoneNumber,
        photoUrl: user.photoURL,
        uid: user.uid,
      );
      final result = await _userProfileRepository.create(createModel);
      switch (result) {
        case Ok():
          _log.info('Profile created for user ${user.uid}');
          return result;
        case Error():
          _log.severe('Failed to create UserProfile! User UID: ${user.uid}, email: ${user.email}. Error details: ${result.error}');
          throw result.error;
      }
    } on Exception catch (e) {
      _log.severe('Failed to create profile for user ${user.uid} (${user.email}): $e');
      return Result.error(e);
    }
  }
}
