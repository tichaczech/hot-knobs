namespace thc.HotKnobs.Model.Entities;

/// <summary>
/// Base class for all entities.
/// </summary>
public abstract class Entity : IModel
{
	/// <summary>
	/// Creation timestamp.
	/// </summary>
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>
	/// Creator's user id.
	/// </summary>
	public required string CreatedBy { get; set; }

	/// <summary>
	/// Id of the entity.
	/// </summary>
	public required string Id { get; set; }

	/// <summary>
	/// Soft delete flag.
	/// </summary>
	public bool IsActive { get; set; } = true;

	/// <summary>
	/// ETag for concurrency control.
	/// </summary>
	public required string ETag { get; set; }

	/// <summary>
	/// Last update timestamp.
	/// </summary>
	public required DateTimeOffset UpdatedAt { get; set; }

	/// <summary>
	/// Last updater's user id.
	/// </summary>
	public required string UpdatedBy { get; set; }
}
