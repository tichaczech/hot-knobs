using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy.Commands;

/// <summary>
/// Represents a patron model with basic information.
/// </summary>
public abstract record PatronModel
{
	/// <summary>
	/// The first name of the patron.
	/// </summary>
	[Required]
	public string? FirstName { get; set; }

	/// <summary>
	/// The last name of the patron.
	/// </summary>
	[Required]
	public string? LastName { get; set; }

	/// <summary>
	/// Date of birth of the patron.
	/// </summary>
	[Required]
	public DateTimeOffset DateOfBirth { get; set; }
}

public record PatronCreateModel : PatronModel;

public record PatronUpdateModel : PatronModel;

public record PatronCreateCommand : EntityCreateCommand<PatronCreateModel, Patron>;

public record PatronDeleteCommand : EntityDeleteCommand;

public record PatronUpdateCommand : EntityUpdateCommand<PatronUpdateModel, Patron>;
