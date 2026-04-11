using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Represents a loan model with basic information.
/// </summary>
public abstract class LoanModel
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

/// <summary>
/// Represents a model used for creating a loan.
/// </summary>
public class LoanCreateModel : LoanModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating a loan.
/// </summary>
public class LoanUpdateModel : LoanModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing loans.
/// </summary>
public interface ILoanUseCases : IEntityUseCases<Loan, LoanCreateModel, LoanUpdateModel>
{
	/// <summary>
	/// Creates a new loan for the specified reservation.
	/// </summary>
	/// <param name="reservationId">The ID of the reservation.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The created loan.</returns>
	Task<Loan> CreateAsync(string reservationId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of loans based on the specified criteria.
	/// </summary>
	/// <param name="bookId">Optional. Filters the list to include only loans of this book.</param>
	/// <param name="dueOn">Optional. Filters the list to include only loans which should be returned on the specified date.</param>
	/// <param name="loanedOn">Optional. Filters the list to include only loans started on the specified date.</param>
	/// <param name="loanInProgressOn">Optional. Filters the list to include only loans which are in progress on the specified date.</param>
	/// <param name="overdueOn">Optional. Filters the list to include only loans which are on due on the specified date.</param>
	/// <param name="patronId">Optional. Filters the list to include only loans of this patron.</param>
	/// <param name="reservationId">Optional. Filters the list to include only loans of this reservation.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only loans modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active loans in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the IDs of loans that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? bookId = default, DateTimeOffset? dueOn = default, DateTimeOffset? loanInProgressOn = default, DateTimeOffset? loanedOn = default, DateTimeOffset? overdueOn = default, string? patronId = default, string? reservationId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
