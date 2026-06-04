import 'package:flutter/foundation.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';

import '../../../data/repositories/users/profile_repository.dart';
import '../../../domain/models/users/profile.dart';
import '../../../domain/use_cases/auth.dart';
import '../../../utils/auth_provider.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

enum SignOperationResult {
  success,
  cancelledByUser
}

class SignInViewModel extends ChangeNotifier {
  SignInViewModel({required AuthProvider authProvider, required AuthUseCases authUseCases, required ProfileRepository profileRepository}) {
    _authProvider = authProvider;
    _authUseCases = authUseCases;
    _profileRepository = profileRepository;
    _log = Logger('UI:Auth:SignInViewModel');

    signIn = Command0<SignOperationResult>(_signIn);
    signUp = Command0<SignOperationResult>(_signUp);
  }

  late final AuthProvider _authProvider;
  late final AuthUseCases _authUseCases;
  late final Logger _log;
  late final ProfileRepository _profileRepository;

  late final Command0<SignOperationResult> signIn;
  late final Command0<SignOperationResult> signUp;

  Future<Result<SignOperationResult>> _signIn() async {
    try {
      final user = await _authProvider.signIn();
      final result = await _authUseCases.signIn(user);

      switch (result) {
        case Ok():
          _log.info('Profile created/updated successfully for user "${user.email}"');
        case Error():
          _log.severe('Failed to create/update profile for user "${user.email}" => ${result.error}');
          return Result.error(result.error);
      }

      return Result.ok(SignOperationResult.success);
    } on AuthCancelledByUserException catch (e) {
      _log.info('Sign in cancelled by user => $e');
      return Result.ok(SignOperationResult.cancelledByUser);
    } on MsalException catch (e) {
      _log.severe('Sign in failed => $e');
      return Result.error(e);
    }
  }

  Future<Result<SignOperationResult>> _signUp() async {
    try {
      final account = await _authProvider.signUp();

      // return _profileUseCases.createOrUpdate(user);
      return Result.ok(SignOperationResult.success);
    } on AuthCancelledByUserException catch (e) {
      _log.info('Sign up cancelled by user => $e');
      return Result.ok(SignOperationResult.cancelledByUser);
    } on MsalException catch (e) {
      _log.severe('Sign up failed => $e');
      return Result.error(e);
    }
  }
}
