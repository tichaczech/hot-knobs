import 'package:logging/logging.dart';

import '../../../../domain/models/users/device.dart';
import '../restapi_service.dart';

class DeviceRestAPIService extends RestAPIService<Device, DeviceCreateModel, DeviceUpdateModel> {
  DeviceRestAPIService({required super.authProvider, required super.configProvider, super.httpClient}) : super(endpoint: 'devices', log: Logger('Data:Services:Remote:Users:DeviceRestAPIService'), createHttpMethod: HttpMethod.put);

  @override
  Device fromMap(Map<String, dynamic> map) => DeviceMapper.fromMap(map);
}
