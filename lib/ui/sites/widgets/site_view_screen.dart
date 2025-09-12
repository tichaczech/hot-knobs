import 'package:carousel_slider_plus/carousel_slider_plus.dart';
import 'package:flutter/material.dart';

import '../../core/ui/location_view.dart';
import '../../core/ui/opening_hours_view.dart';
import '../l10n/sites_localizations.dart';
import '../view_models/site_viewmodel.dart';

enum LocationSelection { loc, arv, prk }

class SiteViewScreen extends StatefulWidget {
  const SiteViewScreen({super.key, required this.viewModel});

  final SiteViewModel viewModel;

  @override
  State<SiteViewScreen> createState() => _SiteViewScreenState();
}

class _SiteViewScreenState extends State<SiteViewScreen> {
  LocationSelection locationView = LocationSelection.loc;

  @override
  Widget build(BuildContext context) {
    return ListenableBuilder(
      listenable: widget.viewModel,
      builder: (context, child) {
        return Scaffold(
          appBar: AppBar(
            // actions: [
            //   IconButton(
            //     icon: const Icon(Icons.navigation),
            //     onPressed: () {
            //       // Handle navigation action
            //     },
            //   ),
            //   IconButton(
            //     icon: const Icon(Icons.directions_car),
            //     onPressed: () {
            //       // Handle navigation action
            //     },
            //   ),
            //   IconButton(
            //     icon: const Icon(Icons.directions_boat),
            //     onPressed: () {
            //       // Handle navigation action
            //     },
            //   ),
            // ],
            backgroundColor: Theme.of(context).colorScheme.primaryContainer,
            title: Text(widget.viewModel.nameController.text),
          ),
          body: SafeArea(
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Row(
                      children: [Flexible(child: ImageCarousel(widget: widget))],
                    ),
                    Row(children: [const SizedBox(height: 8)]),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                      children: [
                        InkResponse(
                          focusColor: Theme.of(context).colorScheme.primaryContainer,
                          child: Row(
                            children: [
                              Icon(Icons.favorite_border),
                              const SizedBox(width: 4),
                              Text('13', style: Theme.of(context).textTheme.titleMedium),
                            ],
                          ),
                          onTap: () {
                            print('Favorite tapped');
                            // Handle favorite action
                          },
                        ),
                        InkResponse(
                          child: Row(
                            children: [
                              Icon(Icons.thumb_up_off_alt_outlined),
                              const SizedBox(width: 4),
                              Text('37', style: Theme.of(context).textTheme.titleMedium),
                            ],
                          ),
                          onTap: () {
                            print('Thumb up tapped');
                            // Handle thumb up action
                          },
                        ),
                        InkResponse(
                          child: Row(
                            children: [
                              Icon(Icons.thumb_down_off_alt_outlined),
                              const SizedBox(width: 4),
                              Text('3', style: Theme.of(context).textTheme.titleMedium),
                            ],
                          ),
                          onTap: () {
                            print('Thumb down tapped');
                            // Handle thumb down action
                          },
                        ),
                        InkResponse(
                          child: Row(
                            children: [
                              Icon(Icons.chat_outlined),
                              const SizedBox(width: 4),
                              Text('871', style: Theme.of(context).textTheme.titleMedium),
                            ],
                          ),
                          onTap: () {
                            print('Chat tapped');
                            // Handle chat action
                          },
                        ),
                        InkResponse(
                          child: Row(
                            children: [
                              Icon(Icons.flag_outlined),
                              const SizedBox(width: 4),
                              Text('3', style: Theme.of(context).textTheme.titleMedium),
                            ],
                          ),
                          onTap: () {
                            print('Report tapped');
                            // Handle report action
                          },
                        ),
                      ],
                    ),
                    Row(children: [const SizedBox(height: 8)]),
                    if (widget.viewModel.siteType != null) ...[
                      Row(children: [const SizedBox(height: 8)]),
                      Row(
                        children: [
                          const Icon(Icons.directions_bike),
                          const SizedBox(width: 4),
                          Flexible(child: Text(widget.viewModel.siteType!.displayName, style: Theme.of(context).textTheme.titleMedium)),
                        ],
                      ),
                    ],
                    if (widget.viewModel.skillLevel != null) ...[
                      Row(children: [const SizedBox(height: 8)]),
                      Row(
                        children: [
                          const Icon(Icons.bar_chart),
                          const SizedBox(width: 4),
                          Flexible(child: Text(widget.viewModel.skillLevel!.displayName, style: Theme.of(context).textTheme.titleMedium)),
                        ],
                      ),
                    ],
                    if (widget.viewModel.length != null) ...[
                      Row(children: [const SizedBox(height: 8)]),
                      Row(
                        children: [
                          const Icon(Icons.route),
                          const SizedBox(width: 4),
                          Flexible(child: Text('${widget.viewModel.length!} m', style: Theme.of(context).textTheme.titleMedium)),
                        ],
                      ),
                    ],
                    Row(children: [const SizedBox(height: 8)]),
                    Row(
                      children: [Flexible(child: Text(widget.viewModel.descriptionController.text, style: Theme.of(context).textTheme.bodyLarge))],
                    ),
                    if (widget.viewModel.openingHours != null) ...[
                      Row(children: [const SizedBox(height: 16)]),
                      Row(
                        children: [
                          Flexible(
                            child: Material(
                              elevation: 8,
                              borderRadius: BorderRadius.circular(8),
                              color: Theme.of(context).colorScheme.surfaceContainer,
                              child: Padding(
                                padding: EdgeInsets.all(8),
                                child: Column(
                                  children: [
                                    Row(
                                      mainAxisAlignment: MainAxisAlignment.start,
                                      crossAxisAlignment: CrossAxisAlignment.start,
                                      children: [
                                        const Icon(Icons.access_time),
                                        const SizedBox(width: 4),
                                        Text(SitesLocalizations.of(context)!.siteOpeningHoursTitle, style: Theme.of(context).textTheme.titleMedium),
                                      ],
                                    ),
                                    Padding(
                                      padding: const EdgeInsets.all(8),
                                      child: OpeningHoursView(openingHours: widget.viewModel.openingHours!),
                                    ),
                                  ],
                                ),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ],
                    if (widget.viewModel.location != null) ...[
                      Row(children: [const SizedBox(height: 16)]),
                      Row(
                        children: [
                          Flexible(
                            child: DefaultTabController(
                              length: 3,
                              child: Column(
                                children: [
                                  const TabBar(
                                    tabs: [
                                      Tab(icon: Icon(Icons.location_on)),
                                      Tab(icon: Icon(Icons.directions_car)),
                                      Tab(icon: Icon(Icons.local_parking)),
                                    ],
                                  ),
                                  SizedBox(
                                    // TODO: Find the way to set the height dynamically based on the content size
                                    height: 380,
                                    child: TabBarView(
                                      children: [
                                        if (widget.viewModel.location != null)
                                          Padding(
                                            padding: const EdgeInsets.all(8),
                                            child: LocationView(location: widget.viewModel.location!),
                                          )
                                        else
                                          Container(),
                                        if (widget.viewModel.arrival != null)
                                          Padding(
                                            padding: const EdgeInsets.all(8),
                                            child: LocationView(location: widget.viewModel.arrival!),
                                          )
                                        else
                                          Container(),
                                        if (widget.viewModel.parking != null)
                                          Padding(
                                            padding: const EdgeInsets.all(8),
                                            child: LocationView(location: widget.viewModel.parking!),
                                          )
                                        else
                                          Container(),
                                      ],
                                    ),
                                  ),
                                ],
                              ),
                            ),
                          ),
                        ],
                      ),
                    ],
                    Row(children: [const SizedBox(height: 16)]),
                  ],
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}

class ImageCarousel extends StatelessWidget {
  const ImageCarousel({super.key, required this.widget});

  final SiteViewScreen widget;

  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(8.0),
      child: CarouselSlider(
        options: CarouselOptions(height: 200.0),
        items: [1, 2, 3].map((i) {
          return Builder(
            builder: (BuildContext context) {
              return Center(
                child: Image.network(
                  'https://firebasestorage.googleapis.com/v0/b/hot-knobs-dev.firebasestorage.app/o/sites%2F${widget.viewModel.id}%2Fimages%2F${widget.viewModel.id}-0$i.webp?alt=media&token=a9e47aec-cd38-4f81-bed7-d5d5d813b7fb',
                  fit: BoxFit.cover,
                  width: 512,
                ),
              );
            },
          );
        }).toList(),
      ),
    );
  }
}
