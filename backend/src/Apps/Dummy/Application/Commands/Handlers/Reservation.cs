using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Commands.Handlers;

public class ReservationCreateCommandHandler : EntityCreateCommandHandler<ReservationCreateCommand, ReservationCreateModel, Reservation>
{
	public ReservationCreateCommandHandler(ILogger<ReservationCreateCommandHandler> logger, IMapper mapper, IRepository<Reservation> repository) : base(logger, mapper, repository) { }
}

public class ReservationDeleteCommandHandler : EntityDeleteCommandHandler<ReservationDeleteCommand, Reservation>
{
	public ReservationDeleteCommandHandler(ILogger<ReservationDeleteCommandHandler> logger, IMapper mapper, IRepository<Reservation> repository) : base(logger, mapper, repository) { }
}

public class ReservationUpdateCommandHandler : EntityUpdateCommandHandler<ReservationUpdateCommand, ReservationUpdateModel, Reservation>
{
	public ReservationUpdateCommandHandler(ILogger<ReservationUpdateCommandHandler> logger, IMapper mapper, IRepository<Reservation> repository) : base(logger, mapper, repository) { }
}
