using thc.HotKnobs.Domains.Dummy.UseCases;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class ReservationIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	private readonly IReservationUseCases _reservationUseCases = fixture.ReservationUseCases;

	[Fact]
	public async Task ReservationCreateIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();
		var book = await fixture.PrepareBook();
		var reservationCreateModel = new ReservationCreateModel()
		{
			StartsOn = fixture.ReservationCreateModel.StartsOn,
			EndsOn = fixture.ReservationCreateModel.EndsOn,
			BookId = book.Id,
			PatronId = patron.Id
		};

		// Act
		var result = await _reservationUseCases.CreateAsync(default, reservationCreateModel);

		// Assert
		var expectedResult = await _reservationUseCases.GetAsync(result.Id);

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

		// Act
		var result = await _reservationUseCases.GetAsync(reservation.Id);

		// Assert
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

		// Act
		var result = _reservationUseCases.ListAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = new List<string>();
		await foreach (var item in result)
		{
			resultList.Add(item);
		}

		Assert.NotEmpty(resultList);
		Assert.Contains(reservation.Id, resultList);
	}

	[Fact]
	public async Task ReservationUpdateIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();

		// Act
		var result = await _reservationUseCases.UpdateAsync(reservation.Id, fixture.ReservationUpdateModel);

		// Assert
		Assert.Equal(fixture.ReservationUpdateModel.StartsOn, result.StartsOn);
		Assert.Equal(fixture.ReservationUpdateModel.EndsOn, result.EndsOn);
	}

	[Fact]
	public async Task ReservationDeleteIntegrationTest()
	{
		// Arrange
		var reservation = await fixture.PrepareReservation();

		// Act
		await _reservationUseCases.DeleteAsync(reservation.Id);

		// Assert
		var deletedLoan = await _reservationUseCases.GetAsync(reservation.Id, false);
		Assert.NotNull(deletedLoan);
		Assert.False(deletedLoan.IsActive);
	}
}
