using System.Linq.Expressions;

using AutoMapper;

using Fand.Runtime;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

public class LoanUseCases : EntityUseCases<Loan, LoanCreateModel, LoanUpdateModel>, ILoanUseCases
{
	private readonly IMapper _mapper;
	private readonly IRepository<Loan> _repository;
	private readonly IReservationUseCases _reservationUseCases;
	private readonly IBookUseCases _bookUseCases;
	private readonly IPatronUseCases _patronUseCases;

	public LoanUseCases(ILogger<LoanUseCases> logger, IMapper mapper, IRepository<Loan> repository, IReservationUseCases reservationUseCases, IBookUseCases bookUseCases, IPatronUseCases patronUseCases) : base(logger, mapper, repository)
	{
		_mapper = mapper;
		_repository = repository;
		_reservationUseCases = reservationUseCases;
		_bookUseCases = bookUseCases;
		_patronUseCases = patronUseCases;
	}

	/// <inheritdoc />
	public async Task<Loan> CreateAsync(string reservationId, CancellationToken cancellationToken = default)
	{
		var reservation = await _reservationUseCases.GetAsync(reservationId, true, cancellationToken);

		// Add DueOn and LoanedOn
		var loan = _mapper.Map<Loan>(reservation);

		_ = await _repository.CreateAsync(loan, cancellationToken);
		_ = await _repository.SaveChangesAsync(cancellationToken);

		return loan;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? bookId = default, DateTimeOffset? dueOn = default, DateTimeOffset? loanInProgressOn = default, DateTimeOffset? loanedOn = default, DateTimeOffset? overdueOn = default, string? patronId = default, string? reservationId = default, DateTimeOffset? modifiedSince = null, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		Expression<Func<Loan, bool>> where = (loan => true);
		if (!String.IsNullOrEmpty(bookId))
			where = where.AndAlso(loan => loan.BookId == bookId);
		if (dueOn.HasValue)
			where = where.AndAlso(loan => loan.DueOn == dueOn.Value.Date);
		if (loanInProgressOn.HasValue)
			where = where.AndAlso(loan => loan.LoanedOn <= loanInProgressOn.Value.Date && loan.DueOn >= loanInProgressOn.Value.Date);
		if (loanedOn.HasValue)
			where = where.AndAlso(loan => loan.LoanedOn == loanedOn.Value.Date);
		if (overdueOn.HasValue)
			where = where.AndAlso(loan => loan.DueOn < overdueOn.Value.Date);
		if (!String.IsNullOrEmpty(patronId))
			where = where.AndAlso(loan => loan.PatronId == patronId);
		if (!String.IsNullOrEmpty(reservationId))
			where = where.AndAlso(loan => loan.ReservationId == reservationId);

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	/// <inheritdoc />
	protected override async Task ValidateAsync(LoanCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		try
		{
			_ = await _bookUseCases.GetAsync(model?.BookId!, true, cancellationToken);

		}
		catch (EntityException ex) when (ex is EntityNotFoundException or EntityNotActiveException)
		{
			EntityNotFoundOrNotActiveException.Throw<Book>(null, model?.BookId!);
		}

		try
		{
			_ = await _patronUseCases.GetAsync(model?.PatronId!, true, cancellationToken);
		}
		catch (EntityException ex) when (ex is EntityNotFoundException or EntityNotActiveException)
		{
			EntityNotFoundOrNotActiveException.Throw<Patron>(null, model?.PatronId!);
		}
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(LoanUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}
