using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class LoanServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
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

		unitTestsFixture.LoanCreateModel.BookId = testedBookId;
		unitTestsFixture.LoanCreateModel.PatronId = testedPatronId;
		var loanResponse = await unitTestsFixture.LoanUseCases.CreateAsync(default, unitTestsFixture.LoanCreateModel);

		Assert.NotNull(loanResponse);
	}

	[Fact]
	public async Task GetAsyncTest()
	{
		var response = await unitTestsFixture.LoanUseCases.GetAsync(BaseTestsFixture.GetRandomLoan().Id);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task UpdateAsyncTest()
	{
		var response = await unitTestsFixture.LoanUseCases.UpdateAsync(BaseTestsFixture.GetRandomLoan().Id, unitTestsFixture.LoanUpdateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var loanId = BaseTestsFixture.GetRandomLoan().Id;
		await unitTestsFixture.LoanUseCases.DeleteAsync(loanId);

		var response = await unitTestsFixture.LoanUseCases.GetAsync(loanId, false);

		Assert.False(response.IsActive);
		Assert.NotNull(response);
	}

	[Fact]
	public Task ListAsyncTest()
	{
		var bookId = BaseTestsFixture.GetRandomBook().Id;
		var patronId = BaseTestsFixture.GetRandomPatron().Id;
		var listOfModified = unitTestsFixture.LoanUseCases.ListAsync(bookId: bookId, patronId: patronId);

		Assert.NotNull(listOfModified); ;
		return Task.CompletedTask;
	}
}
