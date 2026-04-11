using thc.HotKnobs.Domains.Dummy.UseCases;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class AuthorIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	private readonly IAuthorUseCases _authorUseCases = fixture.AuthorUseCases;

	[Fact]
	public async Task AuthorCreateIntegrationTest()
	{
		// Arrange

		// Act
		var result = await _authorUseCases.CreateAsync(default, fixture.AuthorCreateModel);

		// Assert
		var expectedResult = await _authorUseCases.GetAsync(result.Id);

		Assert.Equal(expectedResult.FirstName, result.FirstName);
		Assert.Equal(expectedResult.LastName, result.LastName);
		Assert.True(expectedResult.IsActive);
	}

	[Fact]
	public async Task AuthorGetIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		// Act
		var result = await _authorUseCases.GetAsync(author.Id);

		// Assert
		Assert.Equal(fixture.AuthorCreateModel.FirstName, result.FirstName);
		Assert.Equal(fixture.AuthorCreateModel.LastName, result.LastName);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task AuthorListIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		// Act
		var result = _authorUseCases.ListAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = new List<string>();
		await foreach (var item in result)
		{
			resultList.Add(item);
		}

		Assert.NotEmpty(resultList);
		Assert.Contains(author.Id, resultList);
	}

	[Fact]
	public async Task AuthorUpdateIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		// Act
		var result = await _authorUseCases.UpdateAsync(author.Id, fixture.AuthorUpdateModel);

		// Assert
		Assert.Equal(fixture.AuthorUpdateModel.FirstName, result.FirstName);
		Assert.Equal(fixture.AuthorUpdateModel.LastName, result.LastName);
	}

	[Fact]
	public async Task AuthorDeleteIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		// Act
		await _authorUseCases.DeleteAsync(author.Id);

		// Assert
		var deletedAuthor = await _authorUseCases.GetAsync(author.Id, false);
		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}
}
