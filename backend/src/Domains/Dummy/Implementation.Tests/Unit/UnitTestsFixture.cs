using FakeItEasy;

using thc.HotKnobs.Domains.Dummy.UseCases;
using thc.HotKnobs.Model.Entities;
using thc.HotKnobs.Model.Repository;

using MockQueryable.FakeItEasy;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit;

public class UnitTestsFixture : BaseTestsFixture
{
	public UnitTestsFixture()
	{
		InitiateFakedDatabase();
		InitiateServices();
	}

	private void InitiateServices()
	{
		AuthorUseCases = new AuthorUseCases(AuthorServiceLogger, EntityMapper, AuthorRepository);
		PatronUseCases = new PatronUseCases(PatronServiceLogger, EntityMapper, PatronRepository);
		BookUseCases = new BookUseCases(BookServiceLogger, EntityMapper, BookRepository, AuthorUseCases);
		ReservationUseCases = new ReservationUseCases(ReservationServiceLogger, EntityMapper, ReservationRepository, BookUseCases, PatronUseCases);
		LoanUseCases = new LoanUseCases(LoanServiceLogger, EntityMapper, LoanRepository, ReservationUseCases, BookUseCases, PatronUseCases);
	}

	private void InitiateFakedDatabase()
	{
		InitiateFakedDatabase(AuthorRepository, Authors);
		InitiateFakedDatabase(PatronRepository, Patrons);
		InitiateFakedDatabase(BookRepository, Books);
		InitiateFakedDatabase(ReservationRepository, Reservations);
		InitiateFakedDatabase(LoanRepository, Loans);
	}

	private static void InitiateFakedDatabase<T>(IRepository<T> repository, ICollection<T> storage) where T : Entity
	{
		_ = A.CallTo(() => repository.AsQueryable()).Returns(storage.BuildMockDbSet());
		_ = A.CallTo(() => repository.CreateAsync(A<T>._, A<CancellationToken>._)).ReturnsLazily((T entity, CancellationToken c) =>
		{
			storage.Add(entity);
			return entity;
		});
		_ = A.CallTo(() => repository.FindAsync(A<string>._, A<CancellationToken>._)).ReturnsLazily((string id, CancellationToken c) =>
			storage.FirstOrDefault(x => x.Id == id));
		_ = A.CallTo(() => repository.UpdateAsync(A<T>._, A<CancellationToken>._)).ReturnsLazily((T entity, CancellationToken c) =>
		{
			var entityToUpdate = storage.FirstOrDefault(x => x.Id == entity.Id);
			if (entityToUpdate != null)
			{
				_ = storage.Remove(entityToUpdate);
				storage.Add(entity);
			}
			return entity;
		});
		_ = A.CallTo(() => repository.DeleteAsync(A<T>._, A<CancellationToken>._)).Invokes((T entity, CancellationToken c) =>
		{
			var entityToDelete = storage.FirstOrDefault(x => x.Id == entity.Id);
			if (entityToDelete != null)
			{
				if (entityToDelete is Entity)
				{
					entityToDelete.IsActive = false;
					return;
				}

				_ = storage.Remove(entityToDelete);
			}
		});
	}
}
