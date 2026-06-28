using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record EntityGetQuery<TEntity> : ModelGetQuery<TEntity>, IEntityGetQuery<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Gets or sets a value indicating whether to retrieve the entity only when it is active.
	/// </summary>
	/// <remarks>
	/// When set to true, the query will only retrieve the entity if it is active; otherwise, the query will return HTTP 410 (Gone).
	/// </remarks>
	public bool OnlyActive { get; set; }
}
