namespace thc.HotKnobs.Models;

public abstract record EntityWithName : Entity
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
