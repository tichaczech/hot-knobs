using System.ComponentModel.DataAnnotations;

namespace thc.HotKnobs.Commands;

public record EntityDeleteCommand : ModelDeleteCommand, IEntityDeleteCommand
{
	/// <inheritdoc />
	[Required]
	public required string Id { get; set; }

	/// <inheritdoc />
	[Required]
	public required string ETag { get; set; }
}
