using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class ReservationServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateAsyncTest()
	{
		var patronResponse = await unitTestsFixture.PatronUseCases.CreateAsync(default, unitTestsFixture.PatronCreateModel);
		var testedPatronId = patronResponse.Id;

		var authorResponse = await unitTestsFixture.AuthorUseCases.CreateAsync(default, unitTestsFixture.AuthorCreateModel);
		var testedAuthorId = authorResponse.Id;

		unitTestsFixture.BookCreateModel.AuthorId = testedAuthorId;
		var bookResponse = await unitTestsFixture.BookUseCases.CreateAsync(default, unitTestsFixture.BookCreateModel);
		var testedBookId = bookResponse.Id;

		unitTestsFixture.ReservationCreateModel.BookId = testedBookId;
		unitTestsFixture.ReservationCreateModel.PatronId = testedPatronId;
		var reservationResponse = await unitTestsFixture.ReservationUseCases.CreateAsync(default, unitTestsFixture.ReservationCreateModel);

		Assert.NotNull(reservationResponse.Id);
	}

	[Fact]
	public async Task GetAsyncTest()
	{
		var response = await unitTestsFixture.ReservationUseCases.GetAsync(BaseTestsFixture.GetRandomReservation().Id);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task UpdateAsyncTest()
	{
		var reservationId = BaseTestsFixture.GetRandomReservation().Id;
		_ = await unitTestsFixture.ReservationUseCases.GetAsync(reservationId);

		var response = await unitTestsFixture.ReservationUseCases.UpdateAsync(reservationId, unitTestsFixture.ReservationUpdateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var reservationId = BaseTestsFixture.GetRandomReservation().Id;
		await unitTestsFixture.ReservationUseCases.DeleteAsync(reservationId);

		var response = await unitTestsFixture.ReservationUseCases.GetAsync(reservationId, false);

		Assert.NotNull(response);
	}

	[Fact]
	public void ListAsyncTest()
	{
		var listOfModified = unitTestsFixture.ReservationUseCases.ListAsync(bookId: BaseTestsFixture.GetRandomBook().Id, patronId: BaseTestsFixture.GetRandomPatron().Id);

		Assert.NotNull(listOfModified);
	}
}
