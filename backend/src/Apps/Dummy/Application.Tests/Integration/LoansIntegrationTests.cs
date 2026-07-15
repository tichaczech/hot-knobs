using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class LoansIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	[Fact]
	public async Task LoanCreateIntegrationTest()
	{
		var book = await fixture.PrepareBook();
		var patron = await fixture.PreparePatron();
		var result = await fixture.LoanCreateCommandHandler.Handle(fixture.CreateLoanCommand(book.Id, patron.Id), default);

		var expectedResult = await fixture.LoanGetByIdQueryHandler.Handle(new LoanGetByIdQuery { Id = result.Id }, default);

		Assert.Equal(expectedResult.BookId, book.Id);
		Assert.Equal(expectedResult.PatronId, patron.Id);
		Assert.Equal(expectedResult.LoanedOn, result.LoanedOn);
		Assert.Equal(expectedResult.DueOn, result.DueOn);
	}

	[Fact]
	public async Task LoanGetIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();
		var result = await fixture.LoanGetByIdQueryHandler.Handle(new LoanGetByIdQuery { Id = loan.Id }, default);

		Assert.Equal(loan.BookId, result.BookId);
		Assert.Equal(loan.PatronId, result.PatronId);
		Assert.Equal(loan.LoanedOn, result.LoanedOn);
		Assert.Equal(loan.DueOn, result.DueOn);
		Assert.Equal(loan.ReturnedOn, result.ReturnedOn);
	}

	[Fact]
	public async Task LoanListIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();
		var result = await fixture.LoanListQueryHandler.Handle(new LoanListQuery(), default);
		var resultList = await BaseTestsFixture.ReadAllAsync(result.Items);

		Assert.NotEmpty(resultList);
		Assert.Contains(resultList, item => item.Id == loan.Id);
	}

	[Fact]
	public async Task LoanUpdateIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();
		var result = await fixture.LoanUpdateCommandHandler.Handle(fixture.CreateLoanUpdateCommand(loan), default);

		Assert.NotNull(result.ReturnedOn);
	}

	[Fact]
	public async Task LoanDeleteIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();
		await fixture.LoanDeleteCommandHandler.Handle(new() { Id = loan.Id, ETag = loan.ETag }, default);
		var deletedLoan = await fixture.LoanGetByIdQueryHandler.Handle(new LoanGetByIdQuery { Id = loan.Id, OnlyActive = false }, default);
		Assert.NotNull(deletedLoan);
		Assert.False(deletedLoan.IsActive);
	}
}
