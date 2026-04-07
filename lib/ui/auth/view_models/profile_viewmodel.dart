import 'package:flutter/material.dart';
import 'package:logging/logging.dart';

import '../../../data/repositories/user_profile_repository.dart';
import '../../../utils/command.dart';

class ProfileViewModel extends ChangeNotifier {
  ProfileViewModel({required UserProfileRepository profileRepository, required this.id}) {
    _profileRepository = profileRepository;

    nameController.text = '';
    descriptionController.text = '';

    isEdit = id != 'new';
    // load = Command1(_load);
    // save = Command0(_save);
  }

  late final String id;
  late final bool isEdit;
  late final Command1<void, bool> load;
  late final Command0<void> save;
  late final UserProfileRepository _profileRepository;

  final TextEditingController descriptionController = TextEditingController();
  final GlobalKey<FormState> formKey = GlobalKey<FormState>();
  final TextEditingController nameController = TextEditingController();

  final _log = Logger('ProfileViewModel');

  // String? _etag;
  // // TODO: implement editing for following fields (with controller etc.)
  // Location? arrival;
  // int? length;
  // Location? location;
  // OpeningHours? openingHours;
  // Location? parking;
  // SiteType? siteType;
  // SkillLevel? skillLevel;

  // Future<Result<void>> _load(bool forceRefresh) async {
  //   if (!isEdit) {
  //     return Result.ok(null);
  //   }

  //   final result = await _siteRepository.get(id, forceRefresh: forceRefresh);
  //   switch (result) {
  //     case Ok(value: final site) when site != null:
  //       _etag = site.etag;
  //       arrival = site.arrival;
  //       nameController.text = site.name;
  //       descriptionController.text = site.description ?? '';
  //       length = site.length;
  //       location = site.location;
  //       openingHours = site.openingHours;
  //       parking = site.parking;
  //       siteType = site.siteType;
  //       skillLevel = site.skillLevel;
  //       notifyListeners();
  //       return Result.ok(null);
  //     case Ok():
  //       _log.warning('Invalid operation branch! This should not happen.');
  //       return Result.error(Exception('Site not found'));
  //     case Error(error: final e) when e is DocumentNotFoundException:
  //       return Result.error(Exception('Site not found'));
  //     case Error(error: final e):
  //       _log.warning('Error fetching Site: $e');
  //       return Result.error(e);
  //   }
  // }

  // Future<Result<void>> _save() async {
  //   if (!formKey.currentState!.validate()) {
  //     return Result.error(Exception('Validation failed'));
  //   }

  //   final name = nameController.text.trim();
  //   final description = descriptionController.text.trim();

  //   if (isEdit) {
  //     final updateModel = SiteUpdateModel(name: name, description: description);
  //     final result = await _siteRepository.update(id, updateModel, _etag!);

  //     return result;
  //   } else {
  //     final createModel = SiteCreateModel(name: name, description: description);
  //     final result = await _siteRepository.create(createModel);

  //     return result;
  //   }
  // }
}
