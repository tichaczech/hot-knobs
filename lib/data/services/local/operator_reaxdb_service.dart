
import '../../../domain/models/operator.dart';
import '../../../utils/result.dart';
import 'reaxdb_service.dart';

class OperatorReaxDBService extends ReaxDBService<Operator> {
  OperatorReaxDBService({required super.db});

  @override
  Result<Operator> fromMap(Map<String, dynamic> map) {
    try {
      return Result.ok(OperatorMapper.fromMap(map));
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
