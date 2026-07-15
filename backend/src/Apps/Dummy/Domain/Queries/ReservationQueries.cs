using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record ReservationGetByIdQuery : EntityGetByIdQuery<Reservation>;

public record ReservationListQuery : EntityListQuery<Reservation>;
