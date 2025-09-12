import 'package:flutter/material.dart';
import 'package:frontend/data/repositories/operator_repository.dart';

import '../../../domain/models/operator.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

class OperatorsViewModel extends ChangeNotifier {
  OperatorsViewModel({required OperatorRepository operatorRepository}) {
    _operatorRepository = operatorRepository;

    load = Command1(_load)..execute(false);
  }

  late final Command1<void, bool> load;
  late final OperatorRepository _operatorRepository;

  List<Operator> _operators = <Operator>[];

  List<Operator> get operators => _operators;

  Future<Result<void>> _load(bool forceRefresh) async {
    final result = await _operatorRepository.list(forceRefresh: forceRefresh);
    switch (result) {
      case Ok():
        if (result.value == null || result.value!.isEmpty) {
          _operators = [];
          notifyListeners();
          return Result.ok(null);
        }

        // _log.fine('Loading operators...');
        final tasks = result.value!.map((id) => _operatorRepository.get(id, forceRefresh: forceRefresh));
        final results = await Future.wait<Result<Operator>>(tasks);
        if (results.whereType<Error>().isNotEmpty) {
          _operators = [];
          notifyListeners();
          // _log.warning('Failed to load some operators: $errors');
          return Result.error(Exception('Failed to load some operators: ${results.whereType<Error>().toList()}'));
        }

        if (results.whereType<Ok<Operator>>().where((x) => x.value == null).isNotEmpty) {
          _operators = [];
          notifyListeners();
          // _log.warning('Failed to load some operators: $errors');
          return Result.error(Exception('Failed to load some operators: ${results.whereType<Error>().toList()}'));
        }

        _operators = results.whereType<Ok<Operator>>().map((e) => e.value!).toList();
        notifyListeners();
      case Error():
        // _log.warning('Failed to load operators');
        _operators = [];
        notifyListeners();
        return result;
    }

    return Result.ok(null);
  }
}
