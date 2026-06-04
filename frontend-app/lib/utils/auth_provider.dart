import 'package:logging/logging.dart';
import 'package:msal_auth/msal_auth.dart';

/// Exception thrown by the Hot Knobs Auth/Auth service. See the particular child class of [AuthException] for more specific details.
class AuthException implements Exception {
  final String message;
  final MsalException? msalException;

  const AuthException({required this.message, this.msalException});

  @override
  String toString() {
    return 'AuthException { message: $message, msalException: $msalException }';
  }
}

/// Exception thrown when an authentication operation is cancelled by the user.
class AuthCancelledByUserException extends AuthException {
  const AuthCancelledByUserException({required super.message, super.msalException});
}

/// Exception thrown when an sign in attempt fails.
class AuthSignInException extends AuthException {
  const AuthSignInException({required super.message, super.msalException});
}

/// Exception thrown when an sign out attempt fails.
class AuthSignOutException extends AuthException {
  const AuthSignOutException({required super.message, super.msalException});
}

/// Exception thrown when an sign up attempt fails.
class AuthSignUpException extends AuthException {
  const AuthSignUpException({required super.message, super.msalException});
}

class User {
  final String displayName;
  final String email;
  final String id;

  User({required this.displayName, required this.email, required this.id});

  @override
  toString() => 'User: $displayName (email: $email, id: $id)';
}

final class AuthProvider {
  late final String _androidConfigPath;
  late final String _androidRedirectUri;
  late final String _authority;
  late final Broker _broker;
  late final String _clientId;
  late final Logger _log;

  final AuthorityType _authorityType = AuthorityType.b2c;

  final List<String> _scopes = ['https://graph.microsoft.com/.default']; // Use '.default' to request all the scopes that are configured for the application in the portal. Add specific scopes here if you don't want to request all of them.
  // 'https://graph.microsoft.com/user.read',
  // 'https://graph.microsoft.com/email',
  // 'https://graph.microsoft.com/offline_access',
  // 'https://graph.microsoft.com/openid',
  // 'https://graph.microsoft.com/profile',
  // 'https://graph.microsoft.us/.default'
  // 'email',
  // 'offline_access',
  // 'openid',
  // 'profile',
  // '.default' // Use '.default' to request all the scopes that are configured for the application in the portal.

  SingleAccountPca? _publicClientApplication;

  AuthProvider({required String androidConfigPath, required String androidRedirectUri, required String authority, required String clientId, Broker broker = Broker.webView}) {
    _androidConfigPath = androidConfigPath;
    _androidRedirectUri = androidRedirectUri;
    _authority = authority;
    _broker = broker;
    _clientId = clientId;

    _log = Logger('Utils:AuthProvider');
  }

  /// Gets the access token for the current user. If there is no current user or if acquiring the token fails, an [AuthException] is thrown.
  Future<String> getAccessToken({List<String>? scopes}) async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.acquireTokenSilent(scopes: scopes ?? _scopes);

      return result.accessToken;
    } on MsalException catch (e) {
      _log.severe('Get access token failed => $e');
      throw AuthException(message: 'Get access token failed!', msalException: e);
    }
  }

  /// Gets the current signed in user, or null if there is no signed in user.
  Future<User?> getCurrentUser() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.currentAccount;

      _log.info('Current account => ${result.toJson()}');

      return User(displayName: result.name ?? result.username!, email: result.username!, id: result.id);
    } on MsalException catch (e) {
      _log.severe('Current account failed => $e');
      // We don't throw an exception here because it's possible that there is simply no user currently signed in, which is not necessarily an error case. Instead, we return null to indicate that there is no current user.
      return null;
    }
  }

  Future<User> signIn() async {
    try {
      var user = await getCurrentUser();
      if (user == null) {
        _log.info('No current user, sign in (acquiring token) interactively');

        final client = await _getPublicClientApplication();
        await client.acquireToken(scopes: _scopes);

        user = await getCurrentUser();
      }

      return user!;
    } on MsalException catch (e) {
      if(e is MsalUserCancelException) {
        _log.info('Sign in cancelled by user => $e');
        throw AuthCancelledByUserException(message: 'Sign in cancelled by user!', msalException: e);
      }

      _log.severe('Sign in failed => $e');
      throw AuthSignInException(message: 'Sign in failed!', msalException: e);
    }
  }

  Future<void> signOut() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.signOut();

      if (!result) {
        throw AuthSignOutException(message: 'Sign out failed!');
      }

      _log.info('Sign out => $result');
    } on MsalException catch (e) {
      _log.severe('Sign out failed => $e');
      throw AuthSignOutException(message: 'Sign out failed!', msalException: e);
    }
  }

  Future<Account> signUp() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.acquireToken(scopes: _scopes, prompt: Prompt.create);

      _log.info('Sign up => $result');

      return result.account;
    } on MsalException catch (e) {
      if(e is MsalUserCancelException) {
        _log.info('Sign up cancelled by user => $e');
        throw AuthCancelledByUserException(message: 'Sign up cancelled by user!', msalException: e);
      }

      _log.severe('Sign up failed => $e');
      throw AuthSignUpException(message: 'Sign up failed!', msalException: e);
    }
  }

  Future<SingleAccountPca> _createPublicClientApplication() async {
    try {
      final androidConfig = AndroidConfig(configFilePath: _androidConfigPath, redirectUri: _androidRedirectUri);
      final appleConfig = AppleConfig(authority: _authority, authorityType: _authorityType, broker: _broker);

      return await SingleAccountPca.create(clientId: _clientId, androidConfig: androidConfig, appleConfig: appleConfig);
    } on MsalException catch (e) {
      _log.severe('Create public client application failed => $e');
      throw AuthException(message: 'Create public client application failed!', msalException: e);
    }
  }

  Future<SingleAccountPca> _getPublicClientApplication() async {
    try {
      return (_publicClientApplication ??= await _createPublicClientApplication());
    } on MsalException catch (e) {
      _log.severe('Get public client application failed => $e');
      throw AuthException(message: 'Get public client application failed!', msalException: e);
    }
  }
}
