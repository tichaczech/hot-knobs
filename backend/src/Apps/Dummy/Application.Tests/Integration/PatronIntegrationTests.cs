using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Domains.Dummy.Queries;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class PatronIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	[Fact]
	public async Task PatronCreateIntegrationTest()
	{
		var result = await fixture.PatronCreateCommandHandler.Handle(fixture.CreatePatronCommand(), default);

		var expectedResult = await fixture.PatronGetByIdQueryHandler.Handle(new PatronGetByIdQuery { Id = result.Id }, default);

		Assert.Equal(expectedResult.FirstName, result.FirstName);
		Assert.Equal(expectedResult.LastName, result.LastName);
		Assert.Equal(expectedResult.DateOfBirth, result.DateOfBirth);
		Assert.Equal(expectedResult.IsActive, result.IsActive);
	}

	[Fact]
	public async Task PatronGetIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();
		var result = await fixture.PatronGetByIdQueryHandler.Handle(new PatronGetByIdQuery { Id = patron.Id }, default);

		Assert.Equal(patron.FirstName, result.FirstName);
		Assert.Equal(patron.LastName, result.LastName);
		Assert.Equal(patron.DateOfBirth, result.DateOfBirth);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task PatronListIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();
		var result = await fixture.PatronListQueryHandler.Handle(new PatronListQuery<Patron>(), default);
		var resultList = await BaseTestsFixture.ReadAllAsync(result.Items);

		Assert.NotEmpty(resultList);
		Assert.Contains(resultList, item => item.Id == patron.Id);
	}

	[Fact]
	public async Task PatronUpdateIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();
		var result = await fixture.PatronUpdateCommandHandler.Handle(fixture.CreatePatronUpdateCommand(patron), default);

		Assert.Equal("Updated", result.FirstName);
	}

	[Fact]
	public async Task PatronDeleteIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();
		await fixture.PatronDeleteCommandHandler.Handle(new() { Id = patron.Id, ETag = patron.ETag }, default);
		var deletedAuthor = await fixture.PatronGetByIdQueryHandler.Handle(new PatronGetByIdQuery { Id = patron.Id, OnlyActive = false }, default);
		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}

}
