using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy.Commands;

/// <summary>
/// Represents a loan model with basic information.
/// </summary>
public abstract record LoanModel
{
	/// <summary>
	/// The book that is loaned.
	/// </summary>
	[Required]
	public string? BookId { get; set; }

	/// <summary>
	/// Date and time when the loan ends.
	/// </summary>
	[Required]
	public DateTimeOffset DueOn { get; set; }

	/// <summary>
	/// Date and time when the book was loaned.
	/// </summary>
	[Required]
	public DateTimeOffset LoanedOn { get; set; }

	/// <summary>
	/// The patron of this loan.
	/// </summary>
	[Required]
	public string? PatronId { get; set; }

	/// <summary>
	/// Date and time when the book was returned.
	/// </summary>
	public DateTimeOffset? ReturnedOn { get; set; }
}

public record LoanCreateModel : LoanModel;

public record LoanUpdateModel : LoanModel;

public record LoanCreateCommand : EntityCreateCommand<LoanCreateModel, Loan>;

public record LoanCreateFromReservationCommand : EntityCreateCommand<Loan>
{
	/// <summary>
	/// The ID of the reservation for which the Loan is created.
	/// </summary>
	[Required]
	public string ReservationId { get; init; } = default!;
}

public record LoanDeleteCommand : EntityDeleteCommand;

public record LoanUpdateCommand : EntityUpdateCommand<LoanUpdateModel, Loan>;
