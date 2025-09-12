import 'package:flutter/material.dart';

import '../../../data/repositories/site_repository.dart';
import '../../../domain/models/site.dart';
import '../../../domain/models/types.dart';
import '../../../utils/command.dart';
import '../../../utils/result.dart';

class SiteViewModel extends ChangeNotifier {
  SiteViewModel({required SiteRepository siteRepository, required this.id}) {
    _siteRepository = siteRepository;

    nameController.text = '';
    descriptionController.text = '';

    isEdit = id != 'new';
    load = Command1(_load);
    save = Command0(_save);
  }

  late final String id;
  late final bool isEdit;
  late final Command1<void, bool> load;
  late final Command0<void> save;

  final TextEditingController descriptionController = TextEditingController();
  final GlobalKey<FormState> formKey = GlobalKey<FormState>();
  final TextEditingController nameController = TextEditingController();

  late final SiteRepository _siteRepository;

  String? _etag;
  // TODO: implement editing for following fields (with controller etc.)
  Location? arrival;
  int? length;
  Location? location;
  OpeningHours? openingHours;
  Location? parking;
  SiteType? siteType;
  SkillLevel? skillLevel;

  Future<Result<void>> _load(bool forceRefresh) async {
    if (!isEdit) {
      return Result.ok(null);
    }

    final result = await _siteRepository.get(id, forceRefresh: forceRefresh);
    switch (result) {
      case Ok(value: final site) when site != null:
        _etag = site.etag;
        arrival = site.arrival;
        nameController.text = site.name;
        descriptionController.text = site.description ?? '';
        length = site.length;
        location = site.location;
        openingHours = site.openingHours;
        parking = site.parking;
        siteType = site.siteType;
        skillLevel = site.skillLevel;
        notifyListeners();
        return Result.ok(null);
      case Ok():
        return Result.error(Exception('Site not found'));
      case Error(error: final e):
        return Result.error(e);
    }
  }

  Future<Result<void>> _save() async {
    if (!formKey.currentState!.validate()) {
      return Result.error(Exception('Validation failed'));
    }

    final name = nameController.text.trim();
    final description = descriptionController.text.trim();

    if (isEdit) {
      final updateModel = SiteUpdateModel(name: name, description: description);
      final result = await _siteRepository.update(id, updateModel, _etag!);

      return result;
    } else {
      final createModel = SiteCreateModel(name: name, description: description);
      final result = await _siteRepository.create(createModel);

      return result;
    }
  }
}
