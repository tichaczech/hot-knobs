using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class AuthorIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	[Fact]
	public async Task AuthorCreateIntegrationTest()
	{
		var result = await fixture.AuthorCreateCommandHandler.Handle(fixture.CreateAuthorCommand(), default);

		var expectedResult = await fixture.AuthorGetByIdQueryHandler.Handle(new AuthorGetByIdQuery { Id = result.Id }, default);

		Assert.Equal(expectedResult.FirstName, result.FirstName);
		Assert.Equal(expectedResult.LastName, result.LastName);
		Assert.True(expectedResult.IsActive);
	}

	[Fact]
	public async Task AuthorGetIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		var result = await fixture.AuthorGetByIdQueryHandler.Handle(new AuthorGetByIdQuery { Id = author.Id }, default);

		Assert.Equal(author.FirstName, result.FirstName);
		Assert.Equal(author.LastName, result.LastName);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task AuthorListIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		var result = await fixture.AuthorListQueryHandler.Handle(new AuthorListQuery<Author>(), default);
		var resultList = await BaseTestsFixture.ReadAllAsync(result.Items);

		Assert.NotEmpty(resultList);
		Assert.Contains(resultList, item => item.Id == author.Id);
	}

	[Fact]
	public async Task AuthorUpdateIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		var result = await fixture.AuthorUpdateCommandHandler.Handle(fixture.CreateAuthorUpdateCommand(author), default);

		Assert.Equal("Updated", result.FirstName);
		Assert.Equal(author.LastName, result.LastName);
	}

	[Fact]
	public async Task AuthorDeleteIntegrationTest()
	{
		// Arrange
		var author = await fixture.PrepareAuthor();

		await fixture.AuthorDeleteCommandHandler.Handle(new() { Id = author.Id, ETag = author.ETag }, default);

		var deletedAuthor = await fixture.AuthorGetByIdQueryHandler.Handle(new AuthorGetByIdQuery { Id = author.Id, OnlyActive = false }, default);
		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}
}
