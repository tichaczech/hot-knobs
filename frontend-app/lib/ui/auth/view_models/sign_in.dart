import 'package:flutter/foundation.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';

// import '../../../domain/use_cases/shared/users/profile.dart';
import '../../../utils/auth_provider.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

enum SignOperationResult {
  success,
  cancelledByUser
}

class SignInViewModel extends ChangeNotifier {
  SignInViewModel({required AuthProvider authProvider /*, required ProfileUseCases profileUseCases*/}) {
    _log = Logger('SignInViewModel');
    // _profileUseCases = profileUseCases;
    _authProvider = authProvider;

    signIn = Command0<SignOperationResult>(_signIn);
    signUp = Command0<SignOperationResult>(_signUp);
  }

  late final Logger _log;
  // late final ProfileUseCases _profileUseCases;

  late final AuthProvider _authProvider;
  late final Command0<SignOperationResult> signIn;
  late final Command0<SignOperationResult> signUp;

  Future<Result<SignOperationResult>> _signIn() async {
    try {
      final user = await _authProvider.signIn();

      // return _profileUseCases.createOrUpdate(user);
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
      final user = await _authProvider.signUp();

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
