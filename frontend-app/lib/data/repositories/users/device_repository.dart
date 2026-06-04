import '../../../domain/models/users/device.dart';
import '../../../utils/result.dart';
import '../repository.dart';

class DeviceRepository extends Repository<Device, DeviceCreateModel, DeviceUpdateModel> {
  DeviceRepository({required super.remoteService, required super.localService}) : super();

  @override
  Future<Result<Device>> create(DeviceCreateModel model) async {
    var x = 7 * 6;
    return super.create(model);
  }
}
