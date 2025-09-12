import 'package:flutter/material.dart';

import '../../../data/repositories/site_repository.dart';
import '../../../domain/models/site.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

class SitesViewModel extends ChangeNotifier {
  SitesViewModel({required SiteRepository siteRepository}) {
    _siteRepository = siteRepository;
    load = Command1(_load)..execute(false);
  }

  late final Command1<void, bool> load;
  late final SiteRepository _siteRepository;

  List<Site> _sites = <Site>[];
  List<Site> get sites => _sites;

  Future<Result<void>> _load(bool forceRefresh) async {
    final result = await _siteRepository.list(forceRefresh: forceRefresh);
    switch (result) {
      case Ok():
        if (result.value == null || result.value!.isEmpty) {
          _sites = [];
          notifyListeners();
          return Result.ok(null);
        }
        final tasks = result.value!.map((id) => _siteRepository.get(id, forceRefresh: forceRefresh));
        final results = await Future.wait<Result<Site>>(tasks);
        if (results.whereType<Error>().isNotEmpty) {
          _sites = [];
          notifyListeners();
          return Result.error(Exception('Failed to load some sites: ${results.whereType<Error>().toList()}'));
        }
        if (results.whereType<Ok<Site>>().where((x) => x.value == null).isNotEmpty) {
          _sites = [];
          notifyListeners();
          return Result.error(Exception('Failed to load some sites: ${results.whereType<Error>().toList()}'));
        }
        _sites = results.whereType<Ok<Site>>().map((e) => e.value!).toList();
        notifyListeners();
        return Result.ok(null);
      case Error():
        _sites = [];
        notifyListeners();
        return result;
    }
  }
}
