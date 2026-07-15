using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class ReservationIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	[Fact]
	public async Task ReservationCreateIntegrationTest()
	{
		var patron = await fixture.PreparePatron();
		var book = await fixture.PrepareBook();
		var result = await fixture.ReservationCreateCommandHandler.Handle(fixture.CreateReservationCommand(book.Id, patron.Id), default);

		var expectedResult = await fixture.ReservationGetByIdQueryHandler.Handle(new ReservationGetByIdQuery { Id = result.Id }, default);

		Assert.Equal(expectedResult.BookId, book.Id);
		Assert.Equal(expectedResult.PatronId, patron.Id);
		Assert.Equal(expectedResult.StartsOn, result.StartsOn);
		Assert.Equal(expectedResult.EndsOn, result.EndsOn);
	}

	[Fact]
	public async Task ReservationGetIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();
		var result = await fixture.ReservationGetByIdQueryHandler.Handle(new ReservationGetByIdQuery { Id = reservation.Id }, default);

		Assert.Equal(reservation.BookId, result.BookId);
		Assert.Equal(reservation.PatronId, result.PatronId);
		Assert.Equal(reservation.StartsOn, result.StartsOn);
		Assert.Equal(reservation.EndsOn, result.EndsOn);
	}

	[Fact]
	public async Task ReservationListIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();
		var result = await fixture.ReservationListQueryHandler.Handle(new ReservationListQuery(), default);
		var resultList = await BaseTestsFixture.ReadAllAsync(result.Items);

		Assert.NotEmpty(resultList);
		Assert.Contains(resultList, item => item.Id == reservation.Id);
	}

	[Fact]
	public async Task ReservationUpdateIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();
		var result = await fixture.ReservationUpdateCommandHandler.Handle(fixture.CreateReservationUpdateCommand(reservation), default);

		Assert.True(result.StartsOn > reservation.StartsOn);
		Assert.True(result.EndsOn > reservation.EndsOn);
	}

	[Fact]
	public async Task ReservationDeleteIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();
		await fixture.ReservationDeleteCommandHandler.Handle(new() { Id = reservation.Id, ETag = reservation.ETag }, default);
		var deletedLoan = await fixture.ReservationGetByIdQueryHandler.Handle(new ReservationGetByIdQuery { Id = reservation.Id, OnlyActive = false }, default);
		Assert.NotNull(deletedLoan);
		Assert.False(deletedLoan.IsActive);
	}
}
