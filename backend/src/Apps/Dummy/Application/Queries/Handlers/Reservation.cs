using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries.Handlers;
using thc.HotKnobs.Repositories;


namespace thc.HotKnobs.Domains.Dummy.Queries.Handlers;

public class ReservationGetByIdQueryHandler : EntityGetByIdQueryHandler<ReservationGetByIdQuery, Reservation>
{
	public ReservationGetByIdQueryHandler(ILogger<ReservationGetByIdQueryHandler> logger, IMapper mapper, IRepository<Reservation> repository) : base(logger, mapper, repository) { }
}

public class ReservationListQueryHandler<TRepresentation> : EntityListQueryHandler<ReservationListQuery, Reservation, TRepresentation>
	where TRepresentation : class, IModel
{
	public ReservationListQueryHandler(ILogger<ReservationListQueryHandler<TRepresentation>> logger, IMapper mapper, IRepository<Reservation> repository) : base(logger, mapper, repository) { }
}
