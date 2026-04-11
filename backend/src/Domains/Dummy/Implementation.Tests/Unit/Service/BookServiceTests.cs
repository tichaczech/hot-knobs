using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class BookServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateAsyncTest()
	{
		var authorResponse = await unitTestsFixture.AuthorUseCases.CreateAsync(default, unitTestsFixture.AuthorCreateModel);
		var TestedAuthorId = authorResponse.Id;

		unitTestsFixture.BookCreateModel.AuthorId = TestedAuthorId;
		var bookResponse = await unitTestsFixture.BookUseCases.CreateAsync(default, unitTestsFixture.BookCreateModel);

		Assert.NotNull(bookResponse);
	}

	[Fact]
	public async Task GetAsyncTest()
	{
		var response = await unitTestsFixture.BookUseCases.GetAsync(BaseTestsFixture.GetRandomBook().Id);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task UpdateAsyncTest()
	{
		unitTestsFixture.BookUpdateModel.AuthorId = BaseTestsFixture.GetRandomAuthor().Id;
		var response = await unitTestsFixture.BookUseCases.UpdateAsync(BaseTestsFixture.GetRandomBook().Id, unitTestsFixture.BookUpdateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var bookId = BaseTestsFixture.GetRandomBook().Id;
		await unitTestsFixture.BookUseCases.DeleteAsync(bookId);

		var response = await unitTestsFixture.BookUseCases.GetAsync(bookId, false);

		Assert.NotNull(response);
	}

	[Fact]
	public Task ListAsyncTest()
	{
		var listOfModified = unitTestsFixture.BookUseCases.ListAsync(authorId: BaseTestsFixture.GetRandomAuthor().Id);

		Assert.NotNull(listOfModified);
		return Task.CompletedTask;
	}
}
