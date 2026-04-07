import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../../core/l10n/core_localizations.dart';
import '../../core/ui/error_indicator.dart';
import '../l10n/sites_localizations.dart';
import '../view_models/sites_viewmodel.dart';
import 'site_list_item.dart';

class SitesScreen extends StatefulWidget {
  const SitesScreen({super.key, required this.viewModel});
  final SitesViewModel viewModel;

  @override
  State<SitesScreen> createState() => _SitesScreenState();
}

class _SitesScreenState extends State<SitesScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        title: Text(SitesLocalizations.of(context)!.sitesScreenName),
        actions: [
          // IconButton(
          //   onPressed: () => context.pushNamed(Routes.siteEdit.name, pathParameters: {'id': 'new'}),
          //   icon: const Icon(Icons.add),
          //   tooltip: 'Add Site', // AppLocalizations.of(context)!.refresh,
          // ),
        ],
      ),
      body: ListenableBuilder(
        listenable: widget.viewModel.load,
        builder: (context, child) {
          if (widget.viewModel.load.completed) {
            return child!;
          }
          return Column(
            children: [
              if (widget.viewModel.load.running) const Expanded(child: Center(child: CircularProgressIndicator())),
              if (widget.viewModel.load.error)
                Expanded(
                  child: Center(
                    child: ErrorIndicator(title: CoreLocalizations.of(context)!.errorWhileLoadingData, label: CoreLocalizations.of(context)!.tryAgain, onPressed: () => widget.viewModel.load.execute(false)),
                  ),
                ),
            ],
          );
        },
        child: ListenableBuilder(
          listenable: widget.viewModel,
          builder: (context, child) {
            final sites = widget.viewModel.sites;
            return SafeArea(
              child: RefreshIndicator(
                onRefresh: () async => widget.viewModel.load.execute(true),
                child: sites.isEmpty
                    ? ListView(
                        padding: const EdgeInsets.symmetric(horizontal: 24.0, vertical: 48.0),
                        children: [
                          Center(
                            child: Column(
                              mainAxisSize: MainAxisSize.min,
                              children: [
                                Icon(Icons.location_on_outlined, size: 72, color: Theme.of(context).colorScheme.primary.withOpacity(.35)),
                                const SizedBox(height: 16),
                                Text(
                                  SitesLocalizations.of(context)!.sitesScreenName,
                                  style: Theme.of(context).textTheme.titleLarge?.copyWith(fontWeight: FontWeight.bold),
                                  textAlign: TextAlign.center,
                                ),
                                const SizedBox(height: 8),
                                Text(
                                  'No sites yet', // TODO: localize
                                  style: Theme.of(context).textTheme.bodyMedium?.copyWith(color: Theme.of(context).colorScheme.onSurfaceVariant),
                                  textAlign: TextAlign.center,
                                ),
                                const SizedBox(height: 24),
                                Builder(
                                  builder: (context) {
                                    return FilledButton.icon(
                                      onPressed: () {
                                        context.pushNamed(Routes.siteEdit.name, pathParameters: {'id': 'new'});
                                        // if (created == true) widget.viewModel.load.execute(true);
                                      },
                                      icon: const Icon(Icons.add),
                                      label: Text(SitesLocalizations.of(context)!.sitesFormCreateButton),
                                    );
                                  },
                                ),
                              ],
                            ),
                          ),
                        ],
                      )
                    : ListView.separated(
                        physics: const AlwaysScrollableScrollPhysics(),
                        itemCount: sites.length,
                        separatorBuilder: (_, __) => SizedBox(height: 8),
                        itemBuilder: (context, index) {
                          final site = sites[index];
                          return SiteListItem(
                            site: site,
                            onEdit: () => context.pushNamed(Routes.siteEdit.name, pathParameters: {'id': site.id}),
                            onTap: () => context.pushNamed(Routes.siteView.name, pathParameters: {'id': site.id}),
                          );
                        },
                      ),
              ),
            );
          },
        ),
      ),
    );
  }
}
