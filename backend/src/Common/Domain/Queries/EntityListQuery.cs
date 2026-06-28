using System.Linq.Expressions;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record EntityListQuery<TEntity, TRepresentation> : ModelListQuery<TRepresentation>, IEntityListQuery<TEntity, TRepresentation>
	where TEntity : Entity
	where TRepresentation : class, IModel
{
	/// <inheritdoc/>
	public Expression<Func<TEntity, bool>>? Filter { get; set; }

	/// <inheritdoc/>
	public bool FilteringEnabled { get; protected set; }

	/// <inheritdoc/>
	public bool OnlyActive { get; set; } = true;

	/// <inheritdoc/>
	public string? PagingToken { get; set; }

	/// <inheritdoc/>
	public string? Search { get; set; }

	/// <inheritdoc/>
	public Expression<Func<TEntity, TRepresentation>>? Selector { get; set; }

	/// <inheritdoc/>
	public string? SynchronizationToken { get; set; }
}
