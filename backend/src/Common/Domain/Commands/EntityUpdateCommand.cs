using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

public abstract record EntityUpdateCommand<TUpdateModel, TEntity> : ModelUpdateCommand<TUpdateModel, TEntity>, IEntityUpdateCommand<TUpdateModel, TEntity>
	where TEntity : Entity
	where TUpdateModel : class
{
	/// <inheritdoc />
	[Required]
	public required string Id { get; set; }

	/// <inheritdoc />
	[Required]
	public required string ETag { get; set; }
}
