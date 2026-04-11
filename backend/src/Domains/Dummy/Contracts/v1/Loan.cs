using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Domains.Dummy.Contracts.v1;

/// <summary>
/// Represents a loan of a book.
/// </summary>
public abstract class LoanContract
{
	/// <summary>
	/// The book that is loaned.
	/// </summary>
	[Required]
	public string? BookId { get; set; }

	/// <summary>
	/// The date and time when the loan ends.
	/// </summary>
	[Required]
	public DateTimeOffset DueOn { get; set; }

	/// <summary>
	/// The date and time when the book was returned.
	/// </summary>
	public DateTimeOffset? ReturnedOn { get; set; }

	/// <summary>
	/// The patron of this loan.
	/// </summary>
	[Required]
	public string? PatronId { get; set; }

	/// <summary>
	/// The date and time when the book was loaned.
	/// </summary>
	[Required]
	public DateTimeOffset StartedOn { get; set; }
}

/// <summary>
/// Request to create a loan.
/// </summary>
public class LoanCreateRequest : LoanContract, IResourceCreateRequest { }

/// <summary>
/// Represents a response to a loan contract.
/// </summary>
public class LoanResponse : LoanContract, IResourceResponse
{
	/// <summary>
	/// Indicates whether the loan is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Loan ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }

	/// <summary>
	/// Reservation associated with this loan.
	/// </summary>
	public string? ReservationId { get; set; }
}

/// <summary>
/// Request to update a reservation.
/// </summary>
public class LoanUpdateRequest : IResourceUpdateRequest
{
	/// <summary>
	/// The date and time when the loan ends.
	/// </summary>
	[Required]
	public DateTimeOffset ShouldReturnOn { get; set; }

	/// <summary>
	/// The date and time when the book was returned.
	/// </summary>
	public DateTimeOffset? ReturnedOn { get; set; }
}

/// <summary>
/// Provides methods for managing loans.
/// </summary>
public interface ILoanService : IResourceOperationsWithCreateAndUpdate<LoanResponse, LoanCreateRequest, LoanUpdateRequest>
{
	/// <summary>
	/// Creates a new loan for the specified reservation ID.
	/// </summary>
	/// <param name="reservationId">The ID of the reservation for which to create a loan.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A <see cref="LoanResponse"/> object representing the created loan.</returns>
	Task<LoanResponse> CreateAsync(string reservationId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of loans based on the specified criteria.
	/// </summary>
	/// <param name="modifiedSince">Limits the resulting list to only loans that have been modified since.</param>
	/// <param name="bookId">The ID of the book to filter by.</param>
	/// <param name="dueOn"></param>
	/// <param name="patronId">The ID of the patron to filter by.</param>
	/// <param name="reservationId"></param>
	/// <param name="loanInProgressOn">The date on which the loan is/was in progress.</param>
	/// <param name="loanedOn"></param>
	/// <param name="overdueOn"></param>
	/// <param name="onlyActive">Limits the resulting list to only active loans.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of loans that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? bookId = default, DateTimeOffset? dueOn = default, DateTimeOffset? loanInProgressOn = default, DateTimeOffset? loanedOn = default, DateTimeOffset? overdueOn = default, string? patronId = default, string? reservationId = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
