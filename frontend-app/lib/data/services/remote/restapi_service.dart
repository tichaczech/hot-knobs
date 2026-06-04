import 'dart:async' show TimeoutException;
import 'dart:convert';
import 'dart:math';

import 'package:http/http.dart' as http;
import 'package:logging/logging.dart';
import 'package:path/path.dart';

import '../../../domain/models/entity.dart';
import '../../../domain/models/target_mapping.dart';
import '../../../utils/auth_provider.dart';
import '../../../utils/config_provider.dart';
import 'remote_service.dart';

/// Thrown for non-2xx, non-retryable HTTP responses.
class ApiException implements Exception {
  final int statusCode;
  final String message;
  final String? responseBody;

  const ApiException({required this.statusCode, required this.message, this.responseBody});

  @override
  String toString() => 'ApiException($statusCode): $message';
}

enum HttpMethod {
  get,
  patch,
  post,
  put,
  delete
}

/// Internal signal that the last HTTP call returned a transient server error and should be retried. Never escapes [RestAPIService].
class _TransientFailureException implements Exception {
  final String reason;

  _TransientFailureException(this.reason);
}

abstract class RestAPIService<TEntity extends Entity, TCreateModel extends CreateModel, TUpdateModel extends UpdateModel> implements RemoteService<TEntity, TCreateModel, TUpdateModel> {
  static const int _maxRetries = 3;
  static const Duration _requestTimeout = Duration(seconds: 30);
  static const Duration _retryBaseDelay = Duration(milliseconds: 500);

  late final AuthProvider _authProvider;
  late final ConfigProvider _configProvider;
  late final HttpMethod _createHttpMethod;
  late final String _endpoint;
  late final http.Client _httpClient;
  late final Logger _log;
  late final List<String>? _scopes;

  String? _cachedBaseUrl;

  RestAPIService({required String endpoint, required AuthProvider authProvider, required ConfigProvider configProvider, required Logger log, HttpMethod? createHttpMethod, http.Client? httpClient, List<String>? scopes}) {
    _endpoint = endpoint;
    _authProvider = authProvider;
    _configProvider = configProvider;
    _log = log;

    _createHttpMethod = createHttpMethod ?? HttpMethod.post;
    _httpClient = httpClient ?? http.Client();
    _scopes = scopes;
  }

  TEntity fromMap(Map<String, dynamic> map);

  @override
  Future<TEntity> create(TCreateModel createModel) async {
    final body = jsonEncode(createModel.toTargetMap(MapTarget.reaxdb));

    final request = await _buildRequest(_createHttpMethod, body: body);
    request.body = body;

    return await _sendRequest(request, (responseBody) => fromMap(jsonDecode(responseBody) as Map<String, dynamic>));
  }

  @override
  Future<void> delete(String id, String etag) async {
    final request = await _buildRequest(HttpMethod.delete, path: id, etag: etag);

    await _sendRequest(request, (responseBody) => null);
  }

  @override
  Future<TEntity?> get(String id, {bool onlyActive = true, String? etag}) async {
    final request = await _buildRequest(HttpMethod.get, path: id, etag: etag);

    return await _sendRequest(request, (responseBody) => fromMap(jsonDecode(responseBody) as Map<String, dynamic>));
  }

  @override
  Future<List<String>> list({String? query, bool onlyActive = true}) async {
    final request = await _buildRequest(HttpMethod.get, path: query != null ? '?q=$query' : null);

    return await _sendRequest(request, (responseBody) {
      final List<String> decoded = jsonDecode(responseBody) as List<String>;
      return decoded;
    });
  }

  @override
  Future<TEntity> update(String id, TUpdateModel updateModel, String etag) async {
    final body = jsonEncode(updateModel.toTargetMap(MapTarget.reaxdb));

    final request = await _buildRequest(HttpMethod.put, path: id, body: body, etag: etag);

    return await _sendRequest(request, (responseBody) => fromMap(jsonDecode(responseBody) as Map<String, dynamic>));
  }

  /// Acquires a fresh Bearer token before each attempt so that a token expiring mid-session is handled transparently by MSAL's silent refresh.
  Future<Map<String, String>> _authHeaders() async {
    final token = await _authProvider.getAccessToken(scopes: _scopes);
    return {
      'Authorization': 'Bearer $token',
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    };
  }

  Future<http.Request> _buildRequest(HttpMethod method, {String? path, String? body, String? etag}) async {
    final baseUrl = await _getBaseUrl();
    final url = Uri.parse(join(baseUrl, _endpoint, path ?? ''));

    final request = http.Request(method.name.toUpperCase(), url);
    request.headers.addAll(await _authHeaders());

    if (etag != null) {
      final headerName = method == HttpMethod.get ? 'If-None-Match' : 'If-Match';

      request.headers[headerName] = etag;
    }

    if (body != null) {
      request.body = body;
    }

    return request;
  }

  Future<String> _getBaseUrl() async {
    _cachedBaseUrl ??= (await _configProvider.getConfiguration(name: 'api'))['base_url'] as String;
    return _cachedBaseUrl!;
  }

  Future<T> _sendRequest<T>(http.Request request, T Function(String responseBody) onSuccess) async {
    try {
      return await _withRetry(() async {
        final response = await _httpClient.send(request).timeout(_requestTimeout);
        final responseBody = await response.stream.bytesToString();
        if ([200, 201, 204].contains(response.statusCode)) {
          return onSuccess(responseBody);
        }

        switch (response.statusCode) {
          case 401:
            throw AuthException(message: 'Unauthorized (401): access token rejected by server');
          case 403:
            throw AuthException(message: 'Forbidden (403): access token does not have permission to perform this action');
          case 404:
            throw DocumentNotFoundException('Not Found (404): resource with specified ID does not exist');
          case 409:
            throw ETagMismatchException('Conflict (409): resource was modified by another request');
          case 410:
            throw DocumentDeletedException('Gone (410): resource was deleted');
          case 412:
            throw PreconditionFailedException('Precondition Failed (412): missing or invalid ETag');
          case >= 500:
            // Transient — eligible for retry.
            throw _TransientFailureException('Server error ${response.statusCode}: ${responseBody.isNotEmpty ? responseBody : 'No response body'}');
          default:
            // 4xx and anything else is a permanent client error.
            throw ApiException(statusCode: response.statusCode, message: 'Unexpected response from server', responseBody: responseBody);
        }
      });
    } on _TransientFailureException catch (e) {
      // All retries exhausted — surface as a public ApiException.
      throw ApiException(statusCode: 503, message: 'Service unavailable after $_maxRetries retries', responseBody: e.reason);
    }
  }

  /// Polly-style retry with exponential back-off + jitter.
  ///
  /// Retries on:
  /// - [_TransientFailureException] (5xx responses)
  /// - [http.ClientException] (connection failures)
  /// - [TimeoutException] (request timeout)
  ///
  /// All other exceptions propagate immediately (4xx, auth, business errors).
  Future<T> _withRetry<T>(Future<T> Function() attempt) async {
    final rng = Random();
    for (int i = 1; i <= _maxRetries; i++) {
      try {
        return await attempt();
      } on _TransientFailureException {
        if (i == _maxRetries) rethrow;
      } on http.ClientException {
        if (i == _maxRetries) rethrow;
      } on TimeoutException {
        if (i == _maxRetries) rethrow;
      }
      final jitter = rng.nextInt(200);
      await Future<void>.delayed(_retryBaseDelay * pow(2, i - 1) + Duration(milliseconds: jitter));
    }

    // Unreachable: the loop either returns or rethrows on the last attempt.
    throw StateError('_withRetry: unreachable');
  }
}
