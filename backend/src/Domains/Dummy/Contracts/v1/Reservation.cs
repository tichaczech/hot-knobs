using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Domains.Dummy.Contracts.v1;

/// <summary>
/// Represents a reservation of a book.
/// </summary>
public abstract class ReservationContract
{
	/// <summary>
	/// The book that is reserved.
	/// </summary>
	[Required]
	public string? BookId { get; set; }

	/// <summary>
	/// The date and time when the reservation ends.
	/// </summary>
	[Required]
	public DateTimeOffset EndsOn { get; set; }

	/// <summary>
	/// The patron of this reservation.
	/// </summary>
	[Required]
	public string? PatronId { get; set; }

	/// <summary>
	/// The date and time when the reservation starts.
	/// </summary>
	[Required]
	public DateTimeOffset StartsOn { get; set; }
}

/// <summary>
/// Request to create a reservation.
/// </summary>
public class ReservationCreateRequest : ReservationContract, IResourceCreateRequest { }

/// <summary>
/// Represents a response to a reservation contract.
/// </summary>
public class ReservationResponse : ReservationContract, IResourceResponse
{
	/// <summary>
	/// Indicates whether the reservation is active (or soft-deleted).
	/// </summary>
	[Required]
	public bool IsActive { get; set; }

	/// <summary>
	/// Loan ID.
	/// </summary>
	[Required]
	public required string Id { get; set; }
}

/// <summary>
/// Request to update a reservation.
/// </summary>
public class ReservationUpdateRequest : IResourceUpdateRequest
{
	/// <summary>
	/// The date and time when the reservation ends.
	/// </summary>
	[Required]
	public DateTimeOffset EndsOn { get; set; }

	/// <summary>
	/// The date and time when the reservation starts.
	/// </summary>
	[Required]
	public DateTimeOffset StartsOn { get; set; }
}

public interface IReservationService : IResourceOperationsWithCreateAndUpdate<ReservationResponse, ReservationCreateRequest, ReservationUpdateRequest>
{
	/// <summary>
	/// Retrieves a list of reservations based on the specified criteria.
	/// </summary>
	/// <param name="modifiedSince">Limits the resulting list to only reservations that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active reservations.</param>
	/// <param name="bookId">The ID of the book to filter by.</param>
	/// <param name="patronId">The ID of the patron to filter by.</param>
	/// <param name="startsOn">The date on which the reservation starts.</param>
	/// <param name="endsOn">The date on which the reservation ends.</param>
	/// <param name="reservationInProgressOn">The date on which the reservation is in progress.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of reservations that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? bookId = default, string? patronId = default, DateTimeOffset? startsOn = default, DateTimeOffset? endsOn = default, DateTimeOffset? reservationInProgressOn = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
