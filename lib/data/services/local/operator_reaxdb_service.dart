import '../../../domain/models/operator.dart';
import 'reaxdb_service.dart';

class OperatorReaxDBService extends ReaxDBService<Operator> {
  OperatorReaxDBService({required super.db});

  @override
  Operator fromMap(Map<String, dynamic> map) => OperatorMapper.fromMap(map);
}
