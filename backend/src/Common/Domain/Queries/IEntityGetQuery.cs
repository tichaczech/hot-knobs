using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// Get query for <see cref="Entity"/> by Id.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityGetQuery<out TEntity> : IModelGetQuery<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Gets or sets a value indicating whether to retrieve the entity only when it is active.
	/// </summary>
	/// <remarks>
	/// When set to true, the query will only retrieve the entity if it is active; otherwise, the query will return HTTP 410 (Gone).
	/// </remarks>
	bool OnlyActive { get; set; }
}
