import 'dart:developer';

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

final class AuthProvider {
  late final String _androidConfigPath;
  late final String _androidRedirectUri;
  late final String _authority;
  late final Broker _broker;
  late final String _clientId;

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
  }

  Future<AuthenticationResult> acquireToken({bool allowInteractive = false}) async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.acquireTokenSilent(scopes: _scopes);

      log('Acquire token silent => ${result.toJson()}');

      return result;
    } on MsalException catch (e) {
      log('Acquire token silent failed => $e');

      // If it is a UI required exception, try to acquire token interactively.
      if (e is MsalUiRequiredException && allowInteractive) {
        return _acquireToken();
      }

      throw AuthException(message: 'Acquire token silent failed', msalException: e);
    }
  }

  Future<Account?> getCurrentUser() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.currentAccount;

      log('Current account => ${result.toJson()}');

      return result;
    } on MsalException catch (e) {
      log('Current account failed => $e');
      // We don't throw an exception here because it's possible that there is simply no user currently signed in, which is not necessarily an error case. Instead, we return null to indicate that there is no current user.
      // throw AuthException(message: 'Get current user failed', msalException: e);
      return null;
    }
  }

  Future<Account> signIn() async {
    try {
      Account? user = await getCurrentUser();
      if (user == null) {
        log('No current user, acquiring token (sign in) interactively');

        await _acquireToken();
        user = await getCurrentUser();
      }

      return user!;
    } on MsalException catch (e) {
      log('Sign in failed => $e');
      throw AuthException(message: 'Sign in failed', msalException: e);
    }
  }

  Future<void> signOut() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.signOut();

      if (!result) {
        throw AuthException(message: 'Sign out returned \'false\' in MSAL SDK');
      }

      log('Sign out => $result');
    } on MsalException catch (e) {
      log('Sign out failed => $e');
      throw AuthException(message: 'Sign out failed', msalException: e);
    }
  }

  Future<AuthenticationResult> _acquireToken() async {
    try {
      final client = await _getPublicClientApplication();
      final result = await client.acquireToken(scopes: _scopes);

      log('Acquire token => ${result.toJson()}');

      return result;
    } on MsalException catch (e) {
      log('Acquire token failed => $e');
      throw AuthException(message: 'Acquire token failed', msalException: e);
    }
  }

  Future<SingleAccountPca> _createPublicClientApplication() async {
    try {
      final androidConfig = AndroidConfig(configFilePath: _androidConfigPath, redirectUri: _androidRedirectUri);
      final appleConfig = AppleConfig(authority: _authority, authorityType: _authorityType, broker: _broker);

      return await SingleAccountPca.create(clientId: _clientId, androidConfig: androidConfig, appleConfig: appleConfig);
    } on MsalException catch (e) {
      log('Create public client application failed => $e');
      throw AuthException(message: 'Create public client application failed', msalException: e);
    }
  }

  Future<SingleAccountPca> _getPublicClientApplication() async {
    try {
      return (_publicClientApplication ??= await _createPublicClientApplication());
    } on MsalException catch (e) {
      log('Get public client application failed => $e');
      throw AuthException(message: 'Get public client application failed', msalException: e);
    }
  }
}
