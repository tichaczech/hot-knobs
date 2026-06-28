using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class ReservationServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateCommand_CreatesReservation()
	{
		var patron = await unitTestsFixture.PatronCreateCommandHandler.Handle(unitTestsFixture.CreatePatronCommand(), default);
		var author = await unitTestsFixture.AuthorCreateCommandHandler.Handle(unitTestsFixture.CreateAuthorCommand(), default);
		var book = await unitTestsFixture.BookCreateCommandHandler.Handle(unitTestsFixture.CreateBookCommand(author.Id), default);
		var reservationResponse = await unitTestsFixture.ReservationCreateCommandHandler.Handle(unitTestsFixture.CreateReservationCommand(book.Id, patron.Id), default);

		Assert.NotNull(reservationResponse.Id);
	}

	[Fact]
	public async Task GetByIdQuery_ReturnsReservation()
	{
		var reservation = unitTestsFixture.GetRandomReservation();
		var response = await unitTestsFixture.ReservationGetByIdQueryHandler.Handle(new ReservationGetByIdQuery { Id = reservation.Id }, default);

		Assert.NotNull(response);
		Assert.Equal(reservation.Id, response.Id);
	}

	[Fact]
	public async Task UpdateCommand_UpdatesReservation()
	{
		var reservation = unitTestsFixture.GetRandomReservation();
		var response = await unitTestsFixture.ReservationUpdateCommandHandler.Handle(unitTestsFixture.CreateReservationUpdateCommand(reservation), default);

		Assert.NotNull(response);
		Assert.True(response.StartsOn > reservation.StartsOn);
	}

	[Fact]
	public async Task DeleteCommand_DeactivatesReservation()
	{
		var reservation = unitTestsFixture.GetRandomReservation();
		await unitTestsFixture.ReservationDeleteCommandHandler.Handle(new() { Id = reservation.Id, ETag = reservation.ETag }, default);

		var response = await unitTestsFixture.ReservationGetByIdQueryHandler.Handle(new ReservationGetByIdQuery { Id = reservation.Id, OnlyActive = false }, default);

		Assert.NotNull(response);
		Assert.False(response.IsActive);
	}

	[Fact]
	public async Task ListQuery_ReturnsOnlyActiveReservations()
	{
		var response = await unitTestsFixture.ReservationListQueryHandler.Handle(new ReservationListQuery<thc.HotKnobs.Domains.Dummy.Models.Reservation>(), default);
		var items = await BaseTestsFixture.ReadAllAsync(response.Items);

		Assert.NotEmpty(items);
		Assert.All(items, item => Assert.True(item.IsActive));
	}
}
