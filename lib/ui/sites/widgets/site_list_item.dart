import 'package:flutter/material.dart';
import '../../../domain/models/site.dart';

class SiteListItem extends StatelessWidget {
  const SiteListItem({super.key, required this.site, this.onTap, this.onEdit, this.onDelete});

  final Site site;
  final VoidCallback? onTap;
  final VoidCallback? onEdit;
  final VoidCallback? onDelete;

  @override
  Widget build(BuildContext context) {
    final colorScheme = Theme.of(context).colorScheme;

    return Material(
      color: Colors.transparent,
      child: InkWell(
        borderRadius: BorderRadius.circular(16),
        onTap: onTap,
        child: Ink(
          decoration: BoxDecoration(
            color: colorScheme.surface,
            borderRadius: BorderRadius.circular(16),
            border: Border.all(color: colorScheme.outlineVariant.withOpacity(.5)),
            boxShadow: [BoxShadow(color: Colors.black.withOpacity(.05), blurRadius: 4, offset: const Offset(0, 2))],
          ),
          padding: const EdgeInsets.all(16),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              _Avatar(site: site),
              const SizedBox(width: 16),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Row(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Expanded(
                          child: Text(
                            site.name,
                            style: Theme.of(context).textTheme.titleMedium?.copyWith(fontWeight: FontWeight.w600),
                            maxLines: 1,
                            overflow: TextOverflow.ellipsis,
                          ),
                        ),
                        const SizedBox(width: 8),
                        _StatusChip(label: site.siteType.displayName),
                        // const SizedBox(width: 4),
                        // _Menu(onEdit: onEdit, onDelete: onDelete),
                      ],
                    ),
                    const SizedBox(height: 8),
                    if (site.description != null && site.description!.isNotEmpty)
                      Text(
                        site.description!,
                        style: Theme.of(context).textTheme.bodyMedium?.copyWith(color: colorScheme.onSurfaceVariant),
                        maxLines: 3,
                        overflow: TextOverflow.ellipsis,
                      ),
                    const SizedBox(height: 12),
                    // _MetaRow(site: site),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class _Avatar extends StatelessWidget {
  const _Avatar({required this.site});
  final Site site;

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: 28,
      backgroundColor: Theme.of(context).colorScheme.secondaryContainer.withOpacity(.5),
      child: Text(site.name.isNotEmpty ? site.name.substring(0, 1).toUpperCase() : '?', style: Theme.of(context).textTheme.titleMedium?.copyWith(fontWeight: FontWeight.bold)),
    );
  }
}

class _StatusChip extends StatelessWidget {
  const _StatusChip({required this.label});
  final String label;
  @override
  Widget build(BuildContext context) {
    final colorScheme = Theme.of(context).colorScheme;
    // final (bg, fg, label) = isActive
    //     ? (colorScheme.primaryContainer, colorScheme.onPrimaryContainer, 'Active')
    //     : (colorScheme.surfaceContainerHighest, colorScheme.onSurfaceVariant, 'Inactive');
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      decoration: BoxDecoration(
        color: colorScheme.primaryContainer,
        borderRadius: BorderRadius.circular(8),
        border: Border.all(color: colorScheme.outlineVariant.withOpacity(.4)),
      ),
      child: Text(
        label,
        style: Theme.of(context).textTheme.labelSmall?.copyWith(color: colorScheme.onPrimaryContainer, fontWeight: FontWeight.w600),
      ),
    );
  }
}

// class _Menu extends StatelessWidget {
//   const _Menu({this.onEdit, this.onDelete});
//   final VoidCallback? onEdit;
//   final VoidCallback? onDelete;
//   @override
//   Widget build(BuildContext context) {
//     return PopupMenuButton<String>(
//       tooltip: 'Actions',
//       onSelected: (value) {
//         switch (value) {
//           case 'edit':
//             onEdit?.call();
//           case 'delete':
//             onDelete?.call();
//         }
//       },
//       itemBuilder: (context) => [
//         const PopupMenuItem(
//           value: 'edit',
//           child: ListTile(leading: Icon(Icons.edit_outlined), title: Text('Edit')),
//         ),
//         const PopupMenuItem(
//           value: 'delete',
//           child: ListTile(leading: Icon(Icons.delete_outline), title: Text('Delete')),
//         ),
//       ],
//       child: const Padding(padding: EdgeInsets.all(4.0), child: Icon(Icons.more_vert)),
//     );
//   }
// }

// class _MetaRow extends StatelessWidget {
//   const _MetaRow({required this.site});
//   final Site site;
//   @override
//   Widget build(BuildContext context) {
//     final style = Theme.of(context).textTheme.labelSmall;
//     return Wrap(
//       spacing: 12,
//       runSpacing: 4,
//       children: [
//         _MetaIconText(icon: Icons.badge_outlined, text: site.id, style: style),
//         _MetaIconText(icon: Icons.schedule_outlined, text: _formatDate(site.createdAt), style: style),
//         if (site.updatedAt.isAfter(site.createdAt)) _MetaIconText(icon: Icons.update, text: _formatDate(site.updatedAt), style: style),
//       ],
//     );
//   }

//   String _formatDate(DateTime dt) => '${dt.year.toString().padLeft(4, '0')}-${dt.month.toString().padLeft(2, '0')}-${dt.day.toString().padLeft(2, '0')}';
// }

// class _MetaIconText extends StatelessWidget {
//   const _MetaIconText({required this.icon, required this.text, this.style});
//   final IconData icon;
//   final String text;
//   final TextStyle? style;
//   @override
//   Widget build(BuildContext context) {
//     return ConstrainedBox(
//       constraints: const BoxConstraints(maxWidth: 160),
//       child: Row(
//         mainAxisSize: MainAxisSize.min,
//         children: [
//           Icon(icon, size: 14, color: style?.color ?? Theme.of(context).colorScheme.onSurfaceVariant),
//           const SizedBox(width: 4),
//             Expanded(
//               child: Text(
//                 text,
//                 style: style?.copyWith(overflow: TextOverflow.ellipsis) ?? Theme.of(context).textTheme.labelSmall,
//                 maxLines: 1,
//                 overflow: TextOverflow.ellipsis,
//               ),
//             ),
//         ],
//       ),
//     );
//   }
// }
