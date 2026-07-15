using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class BookServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateCommand_CreatesBook()
	{
		var author = await unitTestsFixture.AuthorCreateCommandHandler.Handle(unitTestsFixture.CreateAuthorCommand(), default);
		var bookResponse = await unitTestsFixture.BookCreateCommandHandler.Handle(unitTestsFixture.CreateBookCommand(author.Id), default);

		Assert.NotNull(bookResponse);
		Assert.Equal(author.Id, bookResponse.AuthorId);
	}

	[Fact]
	public async Task GetByIdQuery_ReturnsBook()
	{
		var book = unitTestsFixture.GetRandomBook();
		var response = await unitTestsFixture.BookGetByIdQueryHandler.Handle(new BookGetByIdQuery { Id = book.Id }, default);

		Assert.NotNull(response);
		Assert.Equal(book.Id, response.Id);
	}

	[Fact]
	public async Task UpdateCommand_UpdatesBook()
	{
		var book = unitTestsFixture.GetRandomBook();
		var response = await unitTestsFixture.BookUpdateCommandHandler.Handle(unitTestsFixture.CreateBookUpdateCommand(book, book.AuthorId), default);

		Assert.NotNull(response);
		Assert.Equal("Updated book", response.Name);
	}

	[Fact]
	public async Task DeleteCommand_DeactivatesBook()
	{
		var book = unitTestsFixture.GetRandomBook();
		await unitTestsFixture.BookDeleteCommandHandler.Handle(new() { Id = book.Id, ETag = book.ETag }, default);

		var response = await unitTestsFixture.BookGetByIdQueryHandler.Handle(new BookGetByIdQuery { Id = book.Id, OnlyActive = false }, default);

		Assert.NotNull(response);
		Assert.False(response.IsActive);
	}

	[Fact]
	public async Task ListQuery_ReturnsOnlyActiveBooks()
	{
		var response = await unitTestsFixture.BookListQueryHandler.Handle(new BookListQuery(), default);
		var items = await BaseTestsFixture.ReadAllAsync(response.Items);

		Assert.NotEmpty(items);
		Assert.All(items, item => Assert.True(item.IsActive));
	}

	[Fact]
	public async Task GetByExternalIdQuery_ReturnsMatchingBook()
	{
		var book = unitTestsFixture.GetRandomBook();
		var response = await unitTestsFixture.BookGetByExternalIdQueryHandler.Handle(new BookGetByExternalIdQuery { ExternalId = book.ExternalId }, default);

		Assert.Equal(book.Id, response.Id);
	}
}
