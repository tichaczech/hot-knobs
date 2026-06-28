using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class BookIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	[Fact]
	public async Task BookCreateIntegrationTest()
	{
		var author = await fixture.PrepareAuthor();
		var result = await fixture.BookCreateCommandHandler.Handle(fixture.CreateBookCommand(author.Id), default);

		var expectedResult = await fixture.BookGetByIdQueryHandler.Handle(new BookGetByIdQuery { Id = result.Id }, default);

		Assert.Equal(expectedResult.Name, result.Name);
		Assert.Equal(expectedResult.AuthorId, result.AuthorId);
		Assert.Equal(expectedResult.Id, result.Id);
		Assert.Equal(expectedResult.IsActive, result.IsActive);
	}

	[Fact]
	public async Task BookGetIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();
		var result = await fixture.BookGetByIdQueryHandler.Handle(new BookGetByIdQuery { Id = book.Id }, default);

		Assert.Equal(book.Name, result.Name);
		Assert.Equal(book.AuthorId, result.AuthorId);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task BookListIntegrationTest()
	{
		//	Arrange
		var book = await fixture.PrepareBook();
		var result = await fixture.BookListQueryHandler.Handle(new BookListQuery<Book>(), default);
		var resultList = await BaseTestsFixture.ReadAllAsync(result.Items);

		Assert.NotEmpty(resultList);
		Assert.Contains(resultList, item => item.Id == book.Id);
	}

	[Fact]
	public async Task BookUpdateIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();
		var result = await fixture.BookUpdateCommandHandler.Handle(fixture.CreateBookUpdateCommand(book, book.AuthorId), default);

		Assert.Equal("Updated book", result.Name);
	}

	[Fact]
	public async Task BookDeleteIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();
		await fixture.BookDeleteCommandHandler.Handle(new() { Id = book.Id, ETag = book.ETag }, default);
		var deletedAuthor = await fixture.BookGetByIdQueryHandler.Handle(new BookGetByIdQuery { Id = book.Id, OnlyActive = false }, default);

		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}
}
