using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy.Commands;

/// <summary>
/// Represents a author model with basic information.
/// </summary>
public abstract record AuthorModel
{
	/// <summary>
	/// The external id of the author.
	/// </summary>
	public string? ExternalId { get; set; }

	/// <summary>
	/// The first name of the author.
	/// </summary>
	[Required]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The last name of the author.
	/// </summary>
	[Required]
	public string LastName { get; set; } = default!;
}

public record AuthorCreateModel : AuthorModel;

public record AuthorUpdateModel : AuthorModel;

public record AuthorCreateCommand : EntityCreateCommand<AuthorCreateModel, Author>;

public record AuthorDeleteCommand : EntityDeleteCommand;

public record AuthorUpdateCommand : EntityUpdateCommand<AuthorUpdateModel, Author>;
