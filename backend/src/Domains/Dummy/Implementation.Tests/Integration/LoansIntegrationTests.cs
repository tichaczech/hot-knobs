using thc.HotKnobs.Domains.Dummy.UseCases;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class LoansIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	private readonly ILoanUseCases _loanUseCases = fixture.LoanUseCases;

	[Fact]
	public async Task LoanCreateIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();
		var patron = await fixture.PreparePatron();
		var loanCreateModel = new LoanCreateModel()
		{
			LoanedOn = fixture.LoanCreateModel.LoanedOn,
			DueOn = fixture.LoanCreateModel.DueOn,
			BookId = book.Id,
			PatronId = patron.Id
		};


		// Act
		var result = await _loanUseCases.CreateAsync(default, loanCreateModel);

		// Assert
		var expectedResult = await _loanUseCases.GetAsync(result.Id);

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

		// Act
		var result = await _loanUseCases.GetAsync(loan.Id);

		// Assert
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

		// Act
		var result = _loanUseCases.ListAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = new List<string>();
		await foreach (var item in result)
		{
			resultList.Add(item);
		}

		Assert.NotEmpty(resultList);
		Assert.Contains(loan.Id, resultList);
	}

	[Fact]
	public async Task LoanUpdateIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();

		// Act
		var result = await _loanUseCases.UpdateAsync(loan.Id, fixture.LoanUpdateModel);

		// Assert
		Assert.Equal(fixture.LoanUpdateModel.ReturnedOn, result.ReturnedOn);
	}

	[Fact]
	public async Task LoanDeleteIntegrationTest()
	{
		// Arrange
		var loan = await fixture.PrepareLoan();

		// Act
		await _loanUseCases.DeleteAsync(loan.Id);

		// Assert
		var deletedLoan = await _loanUseCases.GetAsync(loan.Id, false);
		Assert.NotNull(deletedLoan);
		Assert.False(deletedLoan.IsActive);
	}
}
