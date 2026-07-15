using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class PatronServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateCommand_CreatesPatron()
	{
		var response = await unitTestsFixture.PatronCreateCommandHandler.Handle(unitTestsFixture.CreatePatronCommand(), default);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task GetByIdQuery_ReturnsPatron()
	{
		var patron = unitTestsFixture.GetRandomPatron();
		var response = await unitTestsFixture.PatronGetByIdQueryHandler.Handle(new PatronGetByIdQuery { Id = patron.Id }, default);

		Assert.NotNull(response);
		Assert.Equal(patron.Id, response.Id);
	}

	[Fact]
	public async Task UpdateCommand_UpdatesPatron()
	{
		var patron = unitTestsFixture.GetRandomPatron();
		var response = await unitTestsFixture.PatronUpdateCommandHandler.Handle(unitTestsFixture.CreatePatronUpdateCommand(patron), default);

		Assert.NotNull(response);
		Assert.Equal("Updated", response.FirstName);
	}

	[Fact]
	public async Task DeleteCommand_DeactivatesPatron()
	{
		var patron = unitTestsFixture.GetRandomPatron();
		await unitTestsFixture.PatronDeleteCommandHandler.Handle(new() { Id = patron.Id, ETag = patron.ETag }, default);

		var response = await unitTestsFixture.PatronGetByIdQueryHandler.Handle(new PatronGetByIdQuery { Id = patron.Id, OnlyActive = false }, default);

		Assert.NotNull(response);
		Assert.False(response.IsActive);
	}

	[Fact]
	public async Task ListQuery_ReturnsOnlyActivePatrons()
	{
		var response = await unitTestsFixture.PatronListQueryHandler.Handle(new PatronListQuery(), default);
		var items = await BaseTestsFixture.ReadAllAsync(response.Items);

		Assert.NotEmpty(items);
		Assert.All(items, item => Assert.True(item.IsActive));
	}
}
