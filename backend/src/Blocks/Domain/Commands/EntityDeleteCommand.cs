using System.ComponentModel.DataAnnotations;

namespace thc.HotKnobs.Commands;

public record EntityDeleteCommand : ModelDeleteCommand, IEntityDeleteCommand
{
	/// <inheritdoc />
	[Required]
	public string Id { get; init; } = default!;

	/// <inheritdoc />
	[Required]
	public string ETag { get; init; } = default!;
}
