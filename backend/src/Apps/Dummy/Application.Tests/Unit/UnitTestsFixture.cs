using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries.Handlers;
using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit;

public class UnitTestsFixture : BaseTestsFixture
{
	public UnitTestsFixture()
	{
		AuthorRepository = new InMemoryRepository<Author>(Authors);
		PatronRepository = new InMemoryRepository<Patron>(Patrons);
		BookRepository = new InMemoryRepository<Book>(Books);
		ReservationRepository = new InMemoryRepository<Reservation>(Reservations);
		LoanRepository = new InMemoryRepository<Loan>(Loans);

		AuthorCreateCommandHandler = new AuthorCreateCommandHandler(CreateLogger<ILogger<AuthorCreateCommandHandler>>(), EntityMapper, AuthorRepository);
		AuthorDeleteCommandHandler = new AuthorDeleteCommandHandler(CreateLogger<ILogger<AuthorDeleteCommandHandler>>(), EntityMapper, AuthorRepository);
		AuthorUpdateCommandHandler = new AuthorUpdateCommandHandler(CreateLogger<ILogger<AuthorUpdateCommandHandler>>(), EntityMapper, AuthorRepository);
		AuthorGetByIdQueryHandler = new AuthorGetByIdQueryHandler(CreateLogger<ILogger<AuthorGetByIdQueryHandler>>(), EntityMapper, AuthorRepository);
		AuthorGetByExternalIdQueryHandler = new AuthorGetByExternalIdQueryHandler(CreateLogger<ILogger<AuthorGetByExternalIdQueryHandler>>(), EntityMapper, AuthorRepository);
		AuthorListQueryHandler = new AuthorListQueryHandler<Author>(CreateLogger<ILogger<AuthorListQueryHandler<Author>>>(), EntityMapper, AuthorRepository);

		PatronCreateCommandHandler = new PatronCreateCommandHandler(CreateLogger<ILogger<PatronCreateCommandHandler>>(), EntityMapper, PatronRepository);
		PatronDeleteCommandHandler = new PatronDeleteCommandHandler(CreateLogger<ILogger<PatronDeleteCommandHandler>>(), EntityMapper, PatronRepository);
		PatronUpdateCommandHandler = new PatronUpdateCommandHandler(CreateLogger<ILogger<PatronUpdateCommandHandler>>(), EntityMapper, PatronRepository);
		PatronGetByIdQueryHandler = new PatronGetByIdQueryHandler(CreateLogger<ILogger<PatronGetByIdQueryHandler>>(), EntityMapper, PatronRepository);
		PatronListQueryHandler = new PatronListQueryHandler<Patron>(CreateLogger<ILogger<PatronListQueryHandler<Patron>>>(), EntityMapper, PatronRepository);

		BookCreateCommandHandler = new BookCreateCommandHandler(CreateLogger<ILogger<BookCreateCommandHandler>>(), EntityMapper, BookRepository);
		BookDeleteCommandHandler = new BookDeleteCommandHandler(CreateLogger<ILogger<BookDeleteCommandHandler>>(), EntityMapper, BookRepository);
		BookUpdateCommandHandler = new BookUpdateCommandHandler(CreateLogger<ILogger<BookUpdateCommandHandler>>(), EntityMapper, BookRepository);
		BookGetByIdQueryHandler = new BookGetByIdQueryHandler(CreateLogger<ILogger<BookGetByIdQueryHandler>>(), EntityMapper, BookRepository);
		BookGetByExternalIdQueryHandler = new BookGetByExternalIdQueryHandler(CreateLogger<ILogger<BookGetByExternalIdQueryHandler>>(), EntityMapper, BookRepository);
		BookListQueryHandler = new BookListQueryHandler<Book>(CreateLogger<ILogger<BookListQueryHandler<Book>>>(), EntityMapper, BookRepository);

		ReservationCreateCommandHandler = new ReservationCreateCommandHandler(CreateLogger<ILogger<ReservationCreateCommandHandler>>(), EntityMapper, ReservationRepository);
		ReservationDeleteCommandHandler = new ReservationDeleteCommandHandler(CreateLogger<ILogger<ReservationDeleteCommandHandler>>(), EntityMapper, ReservationRepository);
		ReservationUpdateCommandHandler = new ReservationUpdateCommandHandler(CreateLogger<ILogger<ReservationUpdateCommandHandler>>(), EntityMapper, ReservationRepository);
		ReservationGetByIdQueryHandler = new ReservationGetByIdQueryHandler(CreateLogger<ILogger<ReservationGetByIdQueryHandler>>(), EntityMapper, ReservationRepository);
		ReservationListQueryHandler = new ReservationListQueryHandler<Reservation>(CreateLogger<ILogger<ReservationListQueryHandler<Reservation>>>(), EntityMapper, ReservationRepository);

		LoanCreateCommandHandler = new LoanCreateCommandHandler(CreateLogger<ILogger<LoanCreateCommandHandler>>(), EntityMapper, LoanRepository);
		LoanCreateFromReservationCommandHandler = new LoanCreateFromReservationCommandHandler(CreateLogger<ILogger<LoanCreateFromReservationCommandHandler>>(), EntityMapper, LoanRepository, ReservationRepository);
		LoanDeleteCommandHandler = new LoanDeleteCommandHandler(CreateLogger<ILogger<LoanDeleteCommandHandler>>(), EntityMapper, LoanRepository);
		LoanUpdateCommandHandler = new LoanUpdateCommandHandler(CreateLogger<ILogger<LoanUpdateCommandHandler>>(), EntityMapper, LoanRepository);
		LoanGetByIdQueryHandler = new LoanGetByIdQueryHandler(CreateLogger<ILogger<LoanGetByIdQueryHandler>>(), EntityMapper, LoanRepository);
		LoanListQueryHandler = new LoanListQueryHandler<Loan>(CreateLogger<ILogger<LoanListQueryHandler<Loan>>>(), EntityMapper, LoanRepository);
	}

	private sealed class InMemoryRepository<T>(ICollection<T> storage) : IRepository<T> where T : Entity
	{
		public ValueTask<T> Create(T entity, CancellationToken cancellationToken = default)
		{
			storage.Add(entity);
			return ValueTask.FromResult(entity);
		}

		public ValueTask Delete(T entity, CancellationToken cancellationToken = default)
		{
			_ = storage.Remove(entity);
			return ValueTask.CompletedTask;
		}

		public void Dispose()
		{
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
		}

		public IAsyncEnumerable<TRepresentation> FindAll<TRepresentation>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TRepresentation>> selector, string? pagingToken = default, string? syncToken = default, CancellationToken cancellationToken = default) where TRepresentation : class
		{
			return storage.Where(predicate.Compile()).Select(selector.Compile()).ToAsyncEnumerable();
		}

		public ValueTask<T?> FindOne(string id, CancellationToken cancellationToken = default)
		{
			return ValueTask.FromResult(storage.FirstOrDefault(x => x.Id == id));
		}

		public ValueTask<T?> FindOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return ValueTask.FromResult(storage.SingleOrDefault(predicate.Compile()));
		}

		public ValueTask<T> Update(T entity, CancellationToken cancellationToken = default)
		{
			var entityToUpdate = storage.FirstOrDefault(x => x.Id == entity.Id);
			if (entityToUpdate is not null)
			{
				_ = storage.Remove(entityToUpdate);
				storage.Add(entity);
			}

			return ValueTask.FromResult(entity);
		}
	}
}
