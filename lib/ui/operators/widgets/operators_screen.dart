import 'package:flutter/material.dart';
import '../../core/l10n/core_localizations.dart';
import '../../core/ui/error_indicator.dart';
import '../view_models/operators_viewmodel.dart';
import 'operator_list_item.dart';

class OperatorsScreen extends StatefulWidget {
  const OperatorsScreen({super.key, required this.viewModel});

  final OperatorsViewModel viewModel;

  @override
  State<OperatorsScreen> createState() => _OperatorsScreenState();
}

class _OperatorsScreenState extends State<OperatorsScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(CoreLocalizations.of(context)!.operatorsScreenName)),
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
                    child: ErrorIndicator(
                      title: 'Error while loading data',
                      label: 'Try Again',
                      onPressed: () {
                        widget.viewModel.load.execute(false);
                      },
                    ),
                    // child: ErrorIndicator(title: AppLocalizations.of(context).errorWhileLoadingData, label: AppLocalizations.of(context).tryAgain, onPressed: ), //widget.viewModel.load.execute),
                  ),
                ),
            ],
          );
        },
        child: ListenableBuilder(
          listenable: widget.viewModel,
          builder: (context, child) {
            final operators = widget.viewModel.operators;
            return SafeArea(
              child: RefreshIndicator(
                onRefresh: () async => widget.viewModel.load.execute(true),
                child: operators.isEmpty
                    ? ListView(
                        padding: const EdgeInsets.symmetric(horizontal: 24.0, vertical: 48.0),
                        children: [
                          Center(
                            child: Column(
                              mainAxisSize: MainAxisSize.min,
                              children: [
                                Icon(Icons.groups_outlined, size: 72, color: Theme.of(context).colorScheme.primary.withOpacity(.35)),
                                const SizedBox(height: 16),
                                Text(
                                  CoreLocalizations.of(context)!.operatorsScreenName,
                                  style: Theme.of(context).textTheme.titleLarge?.copyWith(fontWeight: FontWeight.bold),
                                  textAlign: TextAlign.center,
                                ),
                                const SizedBox(height: 8),
                                Text(
                                  'No operators yet', // TODO: localize
                                  style: Theme.of(context).textTheme.bodyMedium?.copyWith(color: Theme.of(context).colorScheme.onSurfaceVariant),
                                  textAlign: TextAlign.center,
                                ),
                                const SizedBox(height: 24),
                                FilledButton.icon(
                                  onPressed: () {/* TODO: navigate to create */},
                                  icon: const Icon(Icons.add),
                                  label: const Text('Create Operator'), // TODO: localize
                                ),
                              ],
                            ),
                          )
                        ],
                      )
                    : ListView.separated(
                        physics: const AlwaysScrollableScrollPhysics(),
                        padding: const EdgeInsets.fromLTRB(12, 12, 12, 24),
                        itemCount: operators.length,
                        separatorBuilder: (_, __) => const SizedBox(height: 12),
                        itemBuilder: (context, index) {
                          final operator = operators[index];
                          return OperatorListItem(
                            operator: operator,
                            onEdit: () => debugPrint('Edit operator ${operator.id}'),
                            onTap: () => debugPrint('Open operator ${operator.id}'),
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
