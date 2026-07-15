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

public interface IReservationService : IResourceOperationsWithCreateAndUpdate<ReservationResponse, ReservationCreateRequest, ReservationUpdateRequest>;
