using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Create command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public interface IEntityCreateCommand<out TEntity> : IModelCreateCommand<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Gets the Id of the entity to be created. If null, a new Id will be generated.
	/// </summary>
	string? Id { get; init; }
}

/// <summary>
/// Create command for <see cref="Entity"/> with a specific create model.
/// </summary>
/// <typeparam name="TCreateModel">Type of the create model.</typeparam>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public interface IEntityCreateCommand<TCreateModel, out TEntity> : IEntityCreateCommand<TEntity>, IModelCreateCommand<TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity;
