import '../../../domain/models/operator.dart';
import './repository.dart';

class OperatorRepository extends Repository<Operator, OperatorCreateModel, OperatorUpdateModel> {
  OperatorRepository({required super.remoteService, required super.localService}) : super();
}
