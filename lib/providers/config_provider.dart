import 'dart:convert';

import 'package:flutter/services.dart';

final class ConfigProvider {
  late final Map<String, Map<String, dynamic>> _cachedConfigs;

  ConfigProvider() {
    _cachedConfigs = {};
  }

  Future<Map<String, dynamic>> getConfiguration({required String name, String environment = 'dev'}) async {
    String key = '$name:$environment';

    if (!_cachedConfigs.containsKey(key)) {
      _cachedConfigs[key] = await _loadConfiguration(name: name, environment: environment);
    }
    
    return _cachedConfigs[key]!;
  }

  Future<Map<String, dynamic>> _loadConfiguration({required String name, String environment = 'dev'}) async {
    String content = await rootBundle.loadString('assets/config/$name.$environment.json');

    return Map<String, dynamic>.from(jsonDecode(content));
  }
}
