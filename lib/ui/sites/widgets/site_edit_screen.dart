import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_svg/svg.dart';
import 'package:go_router/go_router.dart';
import 'package:latlong2/latlong.dart';
import 'package:map_launcher/map_launcher.dart';

import '../l10n/sites_localizations.dart';
import '../view_models/site_viewmodel.dart';
import '../../../utils/result.dart';

class SiteEditScreen extends StatefulWidget {
  const SiteEditScreen({super.key, required this.viewModel});
  final SiteViewModel viewModel;

  @override
  State<SiteEditScreen> createState() => _SiteEditScreenState();
}

class _SiteEditScreenState extends State<SiteEditScreen> {
  @override
  Widget build(BuildContext context) {
    final l10n = SitesLocalizations.of(context)!;
    final vm = widget.viewModel;
    return Scaffold(
      appBar: AppBar(
        actions: [
          TextButton(
            onPressed: vm.save.running
                ? null
                : () async {
                    await vm.save.execute();
                    final r = vm.save.result;
                    if (r == null) return;
                    switch (r) {
                      case Ok():
                        if (mounted) context.pop(true);
                      case Error():
                        if (!mounted) break;
                        ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(r.error.toString())));
                    }
                  },
            child: Text('Save', style: TextStyle(fontSize: 20)), // final confirmed = await showDialog<bool>(
            //   context: context,
            //   builder: (context) {
            //     return AlertDialog(
            //       title: Text(l10n.sitesFormDeleteConfirmationTitle),
            //       content: Text(l10n.sitesFormDeleteConfirmationMessage),
            //       actions: [
            //         TextButton(
            //           onPressed: () => Navigator.of(context).pop(false),
            //           child: Text(l10n.cancel),
            //         ),
            //         TextButton(
            //           onPressed: () => Navigator.of(context).pop(true),
            //           child: Text(l10n.delete),
            //         ),
            //       ],
            //     );
            //   },
            // );
            // if (confirmed == true) {
            //   // Call delete function
            // }
          ),
        ],
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(vm.isEdit ? l10n.sitesFormTitleEdit : l10n.sitesFormTitleCreate),
      ),
      body: SafeArea(
        child: Stack(
          children: [
            Form(
              key: vm.formKey,
              child: ListView(
                padding: const EdgeInsets.all(16),
                children: [
                  TextFormField(
                    controller: vm.nameController,
                    decoration: InputDecoration(labelText: l10n.sitesFormFieldNameLabel, hintText: l10n.sitesFormFieldNameHint),
                    validator: (value) => (value == null || value.trim().isEmpty) ? l10n.sitesFormValidationNameRequired : null,
                    textInputAction: TextInputAction.next,
                  ),
                  const SizedBox(height: 16),
                  TextFormField(
                    controller: vm.descriptionController,
                    decoration: InputDecoration(labelText: l10n.sitesFormFieldDescriptionLabel, hintText: l10n.sitesFormFieldDescriptionHint),
                    maxLines: 4,
                  ),
                  const SizedBox(height: 16),
                  ClipRRect(
                    borderRadius: BorderRadius.circular(12),
                    child: SizedBox(
                      height: 300,
                      child: FlutterMap(
                        options: MapOptions(
                          initialCenter: LatLng(49.738471, 14.304696),
                          initialZoom: 13.5,
                          interactionOptions: InteractionOptions(flags: InteractiveFlag.none),
                          onTap: (tapPosition, point) async {
                            try {
                              final coords = Coords(49.738471, 14.304696);
                              final title = vm.nameController.text.isEmpty ? 'Site Location' : vm.nameController.text;
                              final availableMaps = await MapLauncher.installedMaps;

                              showModalBottomSheet(
                                context: context, // TODO: Solve this hint
                                builder: (BuildContext context) {
                                  return SafeArea(
                                    child: Column(
                                      children: [
                                        SizedBox(height: 16),
                                        Text('Vyberte aplikaci', style: TextStyle(fontSize: 18)),
                                        SizedBox(height: 8),
                                        // Divider(),
                                        SingleChildScrollView(
                                          child: Wrap(
                                            children: <Widget>[
                                              for (var map in availableMaps)
                                                ListTile(
                                                  onTap: () {
                                                    Navigator.of(context).pop();
                                                    map.showMarker(coords: coords, title: title, description: vm.descriptionController.text.isEmpty ? null : vm.descriptionController.text, zoom: 13);
                                                  },
                                                  title: Text(map.mapName),
                                                  leading: SvgPicture.asset(map.icon, height: 30.0, width: 30.0),
                                                ),
                                            ],
                                          ),
                                        ),
                                      ],
                                    ),
                                  );
                                },
                              );
                            } catch (e) {
                              print(e);
                            }
                          },
                        ),
                        children: [
                          TileLayer(urlTemplate: 'https://tile.openstreetmap.org/{z}/{x}/{y}.png', userAgentPackageName: 'app.hotknobs'),
                          MarkerLayer(
                            markers: [
                              Marker(
                                point: LatLng(49.738471, 14.304696),
                                width: 30,
                                height: 30,
                                child: Icon(Icons.location_on, color: Colors.red),
                              ),
                            ],
                          ),
                          // RichAttributionWidget(attributions: [TextSourceAttribution('OpenStreetMap contributors', onTap: () => launchUrl(Uri.parse('https://openstreetmap.org/copyright')))]),
                          SimpleAttributionWidget(source: Text('OpenStreetMap contributors')),
                        ],
                      ),
                    ),
                  ),
                  const SizedBox(height: 32),
                ],
              ),
            ),
          ],
        ),
      ),
      // floatingActionButton: FloatingActionButton(
      //   onPressed: null,
      //   tooltip: 'Save',
      //   child: const Icon(Icons.save),
      // ),
    );
  }
}
