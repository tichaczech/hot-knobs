import 'package:frontend/providers/auth_provider.dart';
import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';

import '../../../domain/use_cases/user_profile.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

class SignInViewModel {
  SignInViewModel({required AuthProvider authProvider, required UserProfileUseCases userProfileUseCases}) {
    _log = Logger('SignInViewModel');
    _userProfileUseCases = userProfileUseCases;
    _authProvider = authProvider;

    signIn = Command0<void>(_signIn);
  }

  late final Logger _log;
  late final UserProfileUseCases _userProfileUseCases;

  late final AuthProvider _authProvider;
  late final Command0<void> signIn;

  Future<Result<void>> _signIn() async {
    try {
      final user = await _authProvider.signIn();

      return _userProfileUseCases.createOrUpdate(user);
    } on MsalException catch (e) {
      _log.severe('Sign in failed => $e');

      return Result.error(e);
    }
  }
}
