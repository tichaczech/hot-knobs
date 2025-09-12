import 'package:flutter/material.dart';
import 'package:frontend/ui/core/ui/map_view.dart';

import '../../../domain/models/types.dart';

class LocationView extends StatelessWidget {
  LocationView({super.key, required Location location}) {
    _location = location;
  }

  late final Location _location;

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        if(_location.description != null && _location.description!.isNotEmpty) ...[
          Text(_location.description!, style: Theme.of(context).textTheme.bodyLarge),
          const SizedBox(height: 8),
        ],
        if(_location.coordinates != null) ...[
          SizedBox(height: 300, child: MapView(initialPosition:  _location.coordinates!, markerLocation: _location.coordinates!)),
          const SizedBox(height: 8),
        ],
      ],
    );
  }
}
