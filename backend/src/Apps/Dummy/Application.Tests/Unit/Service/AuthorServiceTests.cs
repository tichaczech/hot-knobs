using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class AuthorServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateCommand_CreatesAuthor()
	{
		var command = unitTestsFixture.CreateAuthorCommand();
		var response = await unitTestsFixture.AuthorCreateCommandHandler.Handle(command, default);

		Assert.NotNull(response);
		Assert.Equal(command.Id, response.Id);
	}

	[Fact]
	public async Task GetByIdQuery_ReturnsExistingAuthor()
	{
		var author = unitTestsFixture.GetRandomAuthor();
		var response = await unitTestsFixture.AuthorGetByIdQueryHandler.Handle(new AuthorGetByIdQuery { Id = author.Id }, default);

		Assert.NotNull(response);
		Assert.Equal(author.Id, response.Id);
	}

	[Fact]
	public async Task UpdateCommand_UpdatesAuthor()
	{
		var author = unitTestsFixture.GetRandomAuthor();
		var response = await unitTestsFixture.AuthorUpdateCommandHandler.Handle(unitTestsFixture.CreateAuthorUpdateCommand(author), default);

		Assert.NotNull(response);
		Assert.Equal("Updated", response.FirstName);
	}

	[Fact]
	public async Task DeleteCommand_DeactivatesAuthor()
	{
		var author = unitTestsFixture.GetRandomAuthor();
		await unitTestsFixture.AuthorDeleteCommandHandler.Handle(new() { Id = author.Id, ETag = author.ETag }, default);

		var response = await unitTestsFixture.AuthorGetByIdQueryHandler.Handle(new AuthorGetByIdQuery { Id = author.Id, OnlyActive = false }, default);

		Assert.NotNull(response);
		Assert.False(response.IsActive);
	}

	[Fact]
	public async Task ListQuery_ReturnsOnlyActiveAuthors()
	{
		var response = await unitTestsFixture.AuthorListQueryHandler.Handle(new AuthorListQuery<thc.HotKnobs.Domains.Dummy.Models.Author>(), default);
		var items = await BaseTestsFixture.ReadAllAsync(response.Items);

		Assert.NotEmpty(items);
		Assert.All(items, item => Assert.True(item.IsActive));
	}

	[Fact]
	public async Task GetByExternalIdQuery_ReturnsMatchingAuthor()
	{
		var author = unitTestsFixture.GetRandomAuthor();
		var response = await unitTestsFixture.AuthorGetByExternalIdQueryHandler.Handle(new AuthorGetByExternalIdQuery { ExternalId = author.ExternalId! }, default);

		Assert.Equal(author.Id, response.Id);
	}
}
