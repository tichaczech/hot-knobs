namespace thc.HotKnobs.Model.Entities;

public abstract class EntityWithName : Entity
{
	/// <summary>
	/// A description of the entity.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// The name of the entity.
	/// </summary>
	public required string Name { get; set; }
}
