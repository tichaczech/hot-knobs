// Copyright 2024 The Flutter team. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

import 'dart:io' as io;

import 'package:device_info_plus/device_info_plus.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/foundation.dart';
import 'package:logging/logging.dart';
import 'package:unique_device_identifier/unique_device_identifier.dart';

import '../../../../utils/result.dart';
import '../../../data/repositories/users/profile_repository.dart';
import '../../../data/services/remote/remote_service.dart';
import '../../../utils/auth_provider.dart';
import '../../data/repositories/users/device_repository.dart';
import '../models/types.dart';
import '../models/users/device.dart';
import '../models/users/profile.dart';

/// UseCases for managing [UserProfile] objects.
class AuthUseCases {
  late final DeviceInfoPlugin _deviceInfoPlugin;
  late final DeviceRepository _deviceRepository;
  late final FirebaseMessaging _firebaseMessaging;
  late final ProfileRepository _profileRepository;
  final _log = Logger('Domain:UseCases:Users:AuthUseCases');

  AuthUseCases({required DeviceInfoPlugin deviceInfoPlugin, required DeviceRepository deviceRepository, required FirebaseMessaging firebaseMessaging, required ProfileRepository profileRepository}) {
    _deviceInfoPlugin = deviceInfoPlugin;
    _deviceRepository = deviceRepository;
    _firebaseMessaging = firebaseMessaging;
    _profileRepository = profileRepository;
  }

  /// Create or update [Profile] from a [User]
  Future<Result<Profile>> signIn(User user) async {
    var profileResult = await _profileRepository.getMy(forceRefresh: true);
    switch (profileResult) {
      case Ok(value: final value) when value != null: // Profile already exists, doing nothing for now (in the future we might want to update some fields like displayName or photoURL)
        _log.fine('Profile for $user already exists...');
      case Ok(): // This case should not happen as the repository should return Error(DocumentNotFoundException) if profile is not found, but we handle it just in case
        _log.severe('Invalid operation branch (this should not happen)! Anyway, profile for $user not found, creating...');
        profileResult = await _createProfile(user);
      case Error() when profileResult.error is DocumentNotFoundException: // Profile not found, creating a new one
        _log.info('Profile for $user not found, creating...');
        profileResult = await _createProfile(user);
      case Error(): // Some other error happened during fetching the profile, we cannot proceed with sign in
        _log.shout('Error fetching Profile: ${profileResult.error}');
        profileResult = Result<Profile>.error(profileResult.error);
    }

    var deviceCreateModel = await _getCreateModelForCurrentDevice();
    var deviceResult = await _deviceRepository.create(deviceCreateModel);
    switch (deviceResult) {
      case Ok(value: final value) when value != null:
        _log.info('$value registered successfully for $user');
      case Ok():
        _log.severe('Invalid operation branch (this should not happen)! Anyway, failed to register $deviceCreateModel for $user');
      case Error():
        _log.shout('Failed to register $deviceCreateModel for $user => ${deviceResult.error}');
        return Result.error(deviceResult.error);
    }

    return profileResult;
  }

  /// Create [Profile] from a current [User]
  Future<Result<Profile>> _createProfile(User user) async {
    final profileCreateModel = ProfileCreateModel(displayName: user.displayName, email: user.email, userId: user.id);

    return await _profileRepository.create(profileCreateModel);
  }

  // /// Update [Profile] from a current [Profile]
  // Future<Result<Profile>> _updateProfile(Profile profile) async {
  //   final deviceRegistration = await _getCurrentDeviceRegistration();
  //   final updateModel = ProfileUpdateModel(deviceRegistration: deviceRegistration, displayName: profile.displayName, email: profile.email, phoneNumber: profile.phoneNumber, photoURL: profile.photoURL);

  //   return await _profileRepository.update(profile.id, updateModel, profile.etag);
  // }

  /// Get the [DeviceCreateModel] for the current device, which is used for device registration during sign in.
  Future<DeviceCreateModel> _getCreateModelForCurrentDevice() async {
    var deviceId = await UniqueDeviceIdentifier.getUniqueIdentifier();
    if (deviceId == null) {
      throw Exception('Failed to get unique device identifier!');
    }

    return switch (defaultTargetPlatform) {
      TargetPlatform.android => _getAndroidDeviceCreateModel(deviceId),
      TargetPlatform.iOS => _getIOSDeviceCreateModel(deviceId),
      TargetPlatform.fuchsia => throw UnimplementedError(),
      TargetPlatform.linux => throw UnimplementedError(),
      TargetPlatform.macOS => throw UnimplementedError(),
      TargetPlatform.windows => throw UnimplementedError(),
    };
  }

  Future<DeviceCreateModel> _getAndroidDeviceCreateModel(String deviceId) async {
    final androidInfo = await _deviceInfoPlugin.androidInfo;
    final token = await _firebaseMessaging.getToken();
    if (token == null) {
      final message = 'Failed to get FCM token on Android!';
      _log.shout(message);
      throw Exception(message);
    }

    return DeviceCreateModel(deviceId: deviceId, modelName: androidInfo.model, name: androidInfo.name, platform: Platform.android, systemName: androidInfo.version.release, token: token);
  }

  Future<DeviceCreateModel> _getIOSDeviceCreateModel(String deviceId) async {
    final iosInfo = await _deviceInfoPlugin.iosInfo;
    final token = await _firebaseMessaging.getAPNSToken();
    if (token == null) {
      final message = 'Failed to get APNS token on iOS!';
      _log.shout(message);
      throw Exception(message);
    }

    return DeviceCreateModel(deviceId: deviceId, modelName: iosInfo.modelName, name: iosInfo.name, platform: Platform.ios, systemName: iosInfo.systemName, token: token);
  }
}
