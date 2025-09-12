import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:flutter_svg/svg.dart';
import 'package:latlong2/latlong.dart';
import 'package:map_launcher/map_launcher.dart';

class MapView extends StatelessWidget {
  MapView({super.key, required LatLng initialPosition, LatLng? markerLocation}) {
    _initialPosition = initialPosition;
    _markerLocation = markerLocation;
  }

  late final LatLng _initialPosition;
  late final LatLng? _markerLocation;

  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(8.0),
      child: FlutterMap(
        options: MapOptions(
          initialCenter: _initialPosition,
          initialZoom: 17,
          interactionOptions: InteractionOptions(flags: InteractiveFlag.none),
          onTap: (tapPosition, point) async {
            try {
              final coords = Coords(_initialPosition.latitude, _initialPosition.longitude);
              // final title = vm.nameController.text.isEmpty ? 'Site Location' : vm.nameController.text;
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
                                    map.showMarker(coords: coords, title: '', description: '', zoom: 13);
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
          if (_markerLocation != null)
            MarkerLayer(
              markers: [
                Marker(
                  point: _markerLocation,
                  width: 50,
                  height: 50,
                  child: Icon(Icons.location_on, color: Colors.red),
                ),
              ],
            ),
          SimpleAttributionWidget(source: Text('OpenStreetMap contributors')),
        ],
      ),
    );
  }
}
