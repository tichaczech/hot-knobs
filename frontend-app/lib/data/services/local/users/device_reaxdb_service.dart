import '../../../../domain/models/users/device.dart';
import '../reaxdb_service.dart';

class DeviceReaxDBService extends ReaxDBService<Device> {
  DeviceReaxDBService({required super.db});

  @override
  Device fromMap(Map<String, dynamic> map) => DeviceMapper.fromMap(map);
}
