using System.ComponentModel.DataAnnotations;

namespace thc.HotKnobs.Models;

public interface IModel { }

public abstract class ModelWithName : IModel
{
	public string? Description { get; set; }

	[Required]
	public required string Name { get; set; }
}
