using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TUpdateModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityUpdateCommand<TUpdateModel, out TEntity> : IModelUpdateCommand<TUpdateModel, TEntity>
	where TEntity : Entity
	where TUpdateModel : class
{
	/// <summary>
	/// Gets or sets the Id of the entity to be updated.
	/// </summary>
	string Id { get; set; }

	/// <summary>
	/// Gets or sets the ETag of the entity to be updated.
	/// </summary>
	/// <remarks>
	/// This is used for concurrency control to ensure that the entity has not been modified since it was last retrieved.
	/// </remarks>
	string ETag { get; set; }
}
