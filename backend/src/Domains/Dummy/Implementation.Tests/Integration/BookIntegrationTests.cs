using thc.HotKnobs.Domains.Dummy.UseCases;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class BookIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	private readonly IBookUseCases _bookUseCases = fixture.BookUseCases;

	[Fact]
	public async Task BookCreateIntegrationTest()
	{
		// Arrange

		// Act
		var author = await fixture.PrepareAuthor();

		fixture.BookCreateModel.AuthorId = author.Id;
		var result = await _bookUseCases.CreateAsync(default, fixture.BookCreateModel);

		// Assert
		var expectedResult = await _bookUseCases.GetAsync(result.Id);

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

		// Act
		var result = await _bookUseCases.GetAsync(book.Id);

		// Assert
		Assert.Equal(book.Name, result.Name);
		Assert.Equal(book.AuthorId, result.AuthorId);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task BookListIntegrationTest()
	{
		//	Arrange
		var book = await fixture.PrepareBook();

		// Act
		var result = _bookUseCases.ListAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = new List<string>();
		await foreach (var item in result)
		{
			resultList.Add(item);
		}

		Assert.NotEmpty(resultList);
		Assert.Contains(book.Id, resultList);
	}

	[Fact]
	public async Task BookUpdateIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();

		// Act
		var result = await _bookUseCases.UpdateAsync(book.Id, fixture.BookUpdateModel);

		// Assert
		Assert.Equal(fixture.BookUpdateModel.Name, result.Name);
	}

	[Fact]
	public async Task BookDeleteIntegrationTest()
	{
		// Arrange
		var book = await fixture.PrepareBook();

		// Act
		await _bookUseCases.DeleteAsync(book.Id);
		var deletedAuthor = await _bookUseCases.GetAsync(book.Id, false);

		// Assert
		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}
}
