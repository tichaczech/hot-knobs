import 'package:dart_mappable/dart_mappable.dart';
import './entity.dart';

part 'operator.mapper.dart';

@MappableClass(discriminatorValue: 'operator')
class Operator extends Entity with OperatorMappable {
  final String description;
  final String? logoUrl;
  final String name;

  Operator({required super.id, required super.active, required super.cachedAt, required super.createdAt, required super.createdBy, required super.etag, required super.updatedAt, required super.updatedBy, required this.description, this.logoUrl, required this.name}) : super() {
    print('Operator created: $id');
  }
}

@MappableClass()
class OperatorCreateModel extends CreateModel with OperatorCreateModelMappable  {
  final String description;
  final String name;

  OperatorCreateModel({required this.description, required this.name});
}

@MappableClass()
class OperatorUpdateModel extends UpdateModel with OperatorUpdateModelMappable {
  final String description;
  final String name;

  OperatorUpdateModel({required this.description, required this.name});
}
