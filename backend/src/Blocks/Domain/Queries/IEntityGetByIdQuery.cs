using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// Get query for <see cref="Entity"/> by Id.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityGetByIdQuery<out TEntity> : IEntityGetQuery<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Gets the Id of the entity to be retrieved.
	/// </summary>
	string Id { get; init; }
}
