// Copyright 2024 The Flutter team. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

import 'dart:io';

import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';

import '../../../data/repositories/user_profile_repository.dart';
import '../../../utils/result.dart';
import '../../data/services/remote/firebase_service.dart';
import '../models/types.dart';
import '../models/user_profile.dart';

/// UseCases for managing [UserProfile] objects.
class UserProfileUseCases {
  UserProfileUseCases({required FirebaseMessaging firebaseMessaging, required UserProfileRepository userProfileRepository}) {
    _firebaseMessaging = firebaseMessaging;
    _userProfileRepository = userProfileRepository;
  }

  late final FirebaseMessaging _firebaseMessaging;
  late final UserProfileRepository _userProfileRepository;

  final _log = Logger('UserProfileUseCases');

  /// Create or update [UserProfile] from a [Account]
  Future<Result<UserProfile>> createOrUpdate(Account user) async {
    // return Result.ok(null);
    final profile = await _userProfileRepository.get(user.id, forceRefresh: true);
    switch (profile) {
      case Ok(value: final value) when value != null:
        _log.fine('UserProfile: ${value.id} already exists, updating...');
        return _update(value);
      case Ok():
        _log.warning('Invalid operation branch! This should not happen.');
        _log.warning('UserProfile: ${user.id} not found, creating...');
        return _create(user);
      case Error() when profile.error is DocumentNotFoundException:
        _log.warning('UserProfile: ${user.id} not found, creating...');
        return _create(user);
      case Error():
        _log.warning('Error fetching UserProfile: ${profile.error}');
        return Result.error(profile.error);
    }
  }

  /// Create [UserProfile] from a current [Account]
  Future<Result<UserProfile>> _create(Account user) async {
    final deviceRegistration = await _getCurrentDeviceRegistration();
    final createModel = UserProfileCreateModel(deviceRegistration: deviceRegistration, displayName: user.name ?? '', email: user.username!, uid: user.id);

    return await _userProfileRepository.create(createModel);
  }

  /// Update [UserProfile] from a current [UserProfile]
  Future<Result<UserProfile>> _update(UserProfile userProfile) async {
    final deviceRegistration = await _getCurrentDeviceRegistration();
    final updateModel = UserProfileUpdateModel(deviceRegistration: deviceRegistration, displayName: userProfile.displayName, email: userProfile.email, phoneNumber: userProfile.phoneNumber, photoURL: userProfile.photoURL);

    return await _userProfileRepository.update(userProfile.id, updateModel, userProfile.etag);
  }

  /// Get the current [DeviceRegistration]
  Future<DeviceRegistration> _getCurrentDeviceRegistration() async {
    String? apnsToken;
    if (Platform.isIOS) {
      apnsToken = await _firebaseMessaging.getAPNSToken();
      if (apnsToken == null) {
        final message = 'Failed to get APNS token on iOS!';
        _log.warning(message);
        throw Exception(message);
      }
    }

    final fcmToken = await _firebaseMessaging.getToken();
    if (fcmToken == null) {
      final message = 'Failed to get FCM token!';
      _log.warning(message);
      throw Exception(message);
    }

    return DeviceRegistration(apnsToken: apnsToken, fcmToken: fcmToken, name: Platform.localHostname, platform: Platform.operatingSystem);
  }
}
