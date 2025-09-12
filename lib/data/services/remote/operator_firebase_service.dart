import '../../../domain/models/operator.dart';
import '../../../utils/result.dart';
import 'firebase_service.dart';

class OperatorFirebaseService extends FirebaseService<Operator, OperatorCreateModel, OperatorUpdateModel> {
  OperatorFirebaseService({required super.firestore});

  @override
  Result<Operator> fromMap(Map<String, dynamic> map) {
    try {
      return Result.ok(OperatorMapper.fromMap(map));
    } on Exception catch (e) {
      return Result.error(e);
    }
  }
}
