using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

public abstract record EntityGetByIdQuery<TEntity> : EntityGetQuery<TEntity>, IEntityGetByIdQuery<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc />
	[Required]
	public string Id { get; init; } = default!;
}
