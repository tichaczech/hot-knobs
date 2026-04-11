using System.Linq.Expressions;

using AutoMapper;

using Fand.Runtime;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Model.Entities;
using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Domains.Dummy.UseCases;

public class ReservationUseCases : EntityUseCases<Reservation, ReservationCreateModel, ReservationUpdateModel>, IReservationUseCases
{
	private readonly IBookUseCases _bookUseCases;
	private readonly IPatronUseCases _patronUseCases;

	public ReservationUseCases(ILogger<ReservationUseCases> logger, IMapper mapper, IRepository<Reservation> repository, IBookUseCases bookUseCases, IPatronUseCases patronUseCases) : base(logger, mapper, repository)
	{
		_bookUseCases = bookUseCases;
		_patronUseCases = patronUseCases;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? bookId = null, string? patronId = null, DateTimeOffset? startsOn = null, DateTimeOffset? endsOn = null, DateTimeOffset? reservationInProgressOn = null, DateTimeOffset? modifiedSince = null, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		Expression<Func<Reservation, bool>> where = (reservation => true);
		if (!String.IsNullOrEmpty(bookId))
			where = where.AndAlso(reservation => reservation.BookId == bookId);
		if (!String.IsNullOrEmpty(patronId))
			where = where.AndAlso(reservation => reservation.PatronId == patronId);
		if (startsOn.HasValue)
			where = where.AndAlso(reservation => reservation.StartsOn == startsOn.Value.UtcDateTime);
		if (endsOn.HasValue)
			where = where.AndAlso(reservation => reservation.EndsOn == endsOn.Value.UtcDateTime);
		if (reservationInProgressOn.HasValue)
			where = where.AndAlso(reservation => reservation.StartsOn <= reservationInProgressOn && reservation.EndsOn >= reservationInProgressOn);

		return ListAsync(where, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	/// <inheritdoc />
	protected override async Task ValidateAsync(ReservationCreateModel model, CancellationToken cancellationToken = default)
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

	protected override Task ValidateAsync(ReservationUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}
