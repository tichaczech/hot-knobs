using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public abstract record EntityUpdateCommand<TEntity> : ModelUpdateCommand<TEntity>, IEntityUpdateCommand<TEntity>
	where TEntity : Entity
{
	/// <inheritdoc />
	[Required]
	public string Id { get; init; } = default!;

	/// <inheritdoc />
	[Required]
	public string ETag { get; init; } = default!;
}

/// <summary>
/// Update command for <see cref="Entity"/> with a specific update model.
/// </summary>
/// <typeparam name="TUpdateModel">Type of the update model.</typeparam>
/// <typeparam name="TEntity">Type of the resulting entity.</typeparam>
public abstract record EntityUpdateCommand<TUpdateModel, TEntity> : ModelUpdateCommand<TUpdateModel, TEntity>, IEntityUpdateCommand<TUpdateModel, TEntity>
	where TEntity : Entity
	where TUpdateModel : class
{
	/// <inheritdoc />
	[Required]
	public string Id { get; init; } = default!;

	/// <inheritdoc />
	[Required]
	public string ETag { get; init; } = default!;
}
