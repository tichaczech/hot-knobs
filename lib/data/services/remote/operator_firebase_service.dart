import '../../../domain/models/operator.dart';
import 'firebase_service.dart';

class OperatorFirebaseService extends FirebaseService<Operator, OperatorCreateModel, OperatorUpdateModel> {
  OperatorFirebaseService({required super.firestore}); // : super(collectionName: 'operators');

  @override
  Operator fromMap(Map<String, dynamic> map) => OperatorMapper.fromMap(map);
}
