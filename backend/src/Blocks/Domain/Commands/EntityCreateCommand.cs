using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Create command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public abstract record EntityCreateCommand<TEntity> : ModelCreateCommand<TEntity>, IEntityCreateCommand<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc />
	public string? Id { get; init; }
}

/// <summary>
/// Create command for <see cref="Entity"/> with a specific create model.
/// </summary>
/// <typeparam name="TCreateModel">Type of the create model.</typeparam>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public abstract record EntityCreateCommand<TCreateModel, TEntity> : ModelCreateCommand<TCreateModel, TEntity>, IEntityCreateCommand<TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity
{
	/// <inheritdoc />
	public string? Id { get; init; }
}
