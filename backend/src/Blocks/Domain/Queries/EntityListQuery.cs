using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record EntityListQuery<TEntity> : ModelListQuery<TEntity>, IEntityListQuery<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc/>
	public Expression<Func<TEntity, bool>>? Filter { get; init; }

	// /// <inheritdoc/>
	// public bool FilteringEnabled { get; init; }

	/// <inheritdoc/>
	public bool OnlyActive { get; init; } = true;

	public IEnumerable<Tuple<Expression<Func<TEntity, object>>, OrderDirection>>? OrderBy { get; init; }

	/// <inheritdoc/>
	public string? PaginationToken { get; init; }

	/// <inheritdoc/>
	public string? Search { get; init; }

	/// <inheritdoc/>
	[Required]
	public Expression<Func<TEntity, dynamic>> Selector { get; init; } = default!;

	/// <inheritdoc/>
	public string? SynchronizationToken { get; init; }
}
