using MapsterMapper;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Commands.Handlers;

public class LoanCreateCommandHandler : EntityCreateCommandHandler<LoanCreateCommand, LoanModel, Loan>
{
	public LoanCreateCommandHandler(ILogger<EntityCreateCommandHandler<LoanCreateCommand, LoanModel, Loan>> logger, IMapper mapper, IRepository<Loan> repository) : base(logger, mapper, repository) { }
}

public class LoanCreateFromReservationCommandHandler : EntityCreateCommandHandler<LoanCreateFromReservationCommand, LoanModel, Loan>
{
	private readonly IRepository<Reservation> _reservationRepository;

	public LoanCreateFromReservationCommandHandler(ILogger<LoanCreateFromReservationCommandHandler> logger, IMapper mapper, IRepository<Loan> repository, IRepository<Reservation> reservationRepository) : base(logger, mapper, repository)
	{
		_reservationRepository = reservationRepository;
	}

	public override async ValueTask<Loan> Handle(LoanCreateFromReservationCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command, nameof(command));
		ArgumentException.ThrowIfNullOrEmpty(command.ReservationId, nameof(command.ReservationId));

		var reservation = await _reservationRepository.FindOne(command.ReservationId, cancellationToken);
		EntityNotFoundException.ThrowIfNull(reservation, command.ReservationId);
		EntityNotActiveException.ThrowIfNotActive(reservation!);

		// Add DueOn and LoanedOn
		var loan = Mapper.Map<Loan>(reservation!);

		_ = await Repository.Create(loan, cancellationToken);

		return loan;
	}
}

public class LoanDeleteCommandHandler : EntityDeleteCommandHandler<LoanDeleteCommand, Loan>
{
	public LoanDeleteCommandHandler(ILogger<LoanDeleteCommandHandler> logger, IMapper mapper, IRepository<Loan> repository) : base(logger, mapper, repository) { }
}

public class LoanUpdateCommandHandler : EntityUpdateCommandHandler<LoanUpdateCommand, LoanModel, Loan>
{
	public LoanUpdateCommandHandler(ILogger<LoanUpdateCommandHandler> logger, IMapper mapper, IRepository<Loan> repository) : base(logger, mapper, repository) { }
}
