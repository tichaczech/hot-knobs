using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of the entity to be updated.</typeparam>
public interface IEntityUpdateCommand<out TEntity> : IModelUpdateCommand<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Gets the Id of the entity to be updated.
	/// </summary>
	string Id { get; init; }

	/// <summary>
	/// Gets the ETag of the entity to be updated.
	/// </summary>
	/// <remarks>
	/// This is used for concurrency control to ensure that the entity has not been modified since it was last retrieved.
	/// </remarks>
	string ETag { get; init; }
}


/// <summary>
/// Update command for <see cref="Entity"/> with a specific update model.
/// </summary>
/// <typeparam name="TUpdateModel">Type of the update model.</typeparam>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public interface IEntityUpdateCommand<TUpdateModel, out TEntity> : IEntityUpdateCommand<TEntity>, IModelUpdateCommand<TUpdateModel, TEntity>
	where TEntity : Entity
	where TUpdateModel : class;
