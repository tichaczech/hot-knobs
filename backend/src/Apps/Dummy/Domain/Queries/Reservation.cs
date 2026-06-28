using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record ReservationGetByIdQuery : EntityGetByIdQuery<Reservation>;

public record ReservationListQuery<TRepresentation> : EntityListQuery<Reservation, TRepresentation>
	where TRepresentation : class, IModel
{
	public ReservationListQuery()
	{
		FilteringEnabled = false;
	}
}
