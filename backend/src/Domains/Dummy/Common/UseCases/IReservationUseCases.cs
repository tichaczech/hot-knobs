using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

/// <summary>
/// Represents a reservation model with basic information.
/// </summary>
public abstract class ReservationModel
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
/// Represents a model used for creating a reservation.
/// </summary>
public class ReservationCreateModel : ReservationModel, IEntityCreateModel { }

/// <summary>
/// Represents a model used for updating a reservation.
/// </summary>
public class ReservationUpdateModel : ReservationModel, IEntityUpdateModel { }

/// <summary>
/// Represents a service for managing reservations.
/// </summary>
public interface IReservationUseCases : IEntityUseCases<Reservation, ReservationCreateModel, ReservationUpdateModel>
{
	/// <summary>
	/// Retrieves a list of reservations based on the specified criteria.
	/// </summary>
	/// <param name="bookId">Optional. Filters the list to include only reservations of this book.</param>
	/// <param name="patronId">Optional. Filters the list to include only reservations of this patron.</param>
	/// <param name="startsOn">Optional. Filters the list to include only reservations which starts on the specified date and time.</param>
	/// <param name="endsOn">Optional. Filters the list to include only reservations which ends on the specified date and time.</param>
	/// <param name="reservationInProgressOn">Optional. Filters the list to include only reservations which are in progress on the specified date and time.</param>
	/// <param name="modifiedSince">Optional. Filters the list to include only reservations modified since the specified date and time.</param>
	/// <param name="onlyActive">Optional. Indicates whether to include only active reservations in the list.</param>
	/// <param name="cancellationToken">Optional. The cancellation token to cancel operation.</param>
	/// <returns>A collection representing the ids of reservations that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(string? bookId = default, string? patronId = default, DateTimeOffset? startsOn = default, DateTimeOffset? endsOn = default, DateTimeOffset? reservationInProgressOn = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}
