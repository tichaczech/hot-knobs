using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class LoanServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateCommand_CreatesLoan()
	{
		var patron = await unitTestsFixture.PatronCreateCommandHandler.Handle(unitTestsFixture.CreatePatronCommand(), default);
		var author = await unitTestsFixture.AuthorCreateCommandHandler.Handle(unitTestsFixture.CreateAuthorCommand(), default);
		var book = await unitTestsFixture.BookCreateCommandHandler.Handle(unitTestsFixture.CreateBookCommand(author.Id), default);
		var loanResponse = await unitTestsFixture.LoanCreateCommandHandler.Handle(unitTestsFixture.CreateLoanCommand(book.Id, patron.Id), default);

		Assert.NotNull(loanResponse);
	}

	[Fact]
	public async Task CreateFromReservationCommand_CreatesLoanFromReservation()
	{
		var reservation = unitTestsFixture.GetRandomReservation();
		var response = await unitTestsFixture.LoanCreateFromReservationCommandHandler.Handle(unitTestsFixture.CreateLoanFromReservationCommand(reservation.Id), default);

		Assert.NotNull(response);
		Assert.Equal(reservation.Id, response.ReservationId);
	}

	[Fact]
	public async Task GetByIdQuery_ReturnsLoan()
	{
		var loan = unitTestsFixture.GetRandomLoan();
		var response = await unitTestsFixture.LoanGetByIdQueryHandler.Handle(new LoanGetByIdQuery { Id = loan.Id }, default);

		Assert.NotNull(response);
		Assert.Equal(loan.Id, response.Id);
	}

	[Fact]
	public async Task UpdateCommand_UpdatesLoan()
	{
		var loan = unitTestsFixture.GetRandomLoan();
		var response = await unitTestsFixture.LoanUpdateCommandHandler.Handle(unitTestsFixture.CreateLoanUpdateCommand(loan), default);

		Assert.NotNull(response);
		Assert.NotNull(response.ReturnedOn);
	}

	[Fact]
	public async Task DeleteCommand_DeactivatesLoan()
	{
		var loan = unitTestsFixture.GetRandomLoan();
		await unitTestsFixture.LoanDeleteCommandHandler.Handle(new() { Id = loan.Id, ETag = loan.ETag }, default);

		var response = await unitTestsFixture.LoanGetByIdQueryHandler.Handle(new LoanGetByIdQuery { Id = loan.Id, OnlyActive = false }, default);

		Assert.False(response.IsActive);
		Assert.NotNull(response);
	}

	[Fact]
	public async Task ListQuery_ReturnsOnlyActiveLoans()
	{
		var response = await unitTestsFixture.LoanListQueryHandler.Handle(new LoanListQuery(), default);
		var items = await BaseTestsFixture.ReadAllAsync(response.Items);

		Assert.NotEmpty(items);
		Assert.All(items, item => Assert.True(item.IsActive));
	}
}
