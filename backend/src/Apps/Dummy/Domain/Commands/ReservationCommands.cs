using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Domains.Dummy.Models;

namespace thc.HotKnobs.Domains.Dummy.Commands;

/// <summary>
/// Represents a reservation model with basic information.
/// </summary>
public abstract record ReservationModel
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

public record ReservationCreateModel : ReservationModel;

public record ReservationUpdateModel : ReservationModel;

public record ReservationCreateCommand : EntityCreateCommand<ReservationCreateModel, Reservation>;

public record ReservationDeleteCommand : EntityDeleteCommand;

public record ReservationUpdateCommand : EntityUpdateCommand<ReservationUpdateModel, Reservation>;
