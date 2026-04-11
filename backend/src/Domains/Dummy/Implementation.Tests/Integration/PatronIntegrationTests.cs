using thc.HotKnobs.Domains.Dummy.UseCases;

using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Integration;

public class PatronIntegrationTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
	private readonly IPatronUseCases _patronUseCases = fixture.PatronUseCases;

	[Fact]
	public async Task PatronCreateIntegrationTest()
	{
		// Arrange

		// Act
		var result = await _patronUseCases.CreateAsync(default, fixture.PatronCreateModel);

		// Assert
		var expectedResult = await _patronUseCases.GetAsync(result.Id);

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

		// Act
		var result = await _patronUseCases.GetAsync(patron.Id);

		// Assert

		Assert.Equal(fixture.PatronCreateModel.FirstName, result.FirstName);
		Assert.Equal(fixture.PatronCreateModel.LastName, result.LastName);
		Assert.Equal(fixture.PatronCreateModel.DateOfBirth, result.DateOfBirth);
		Assert.True(result.IsActive);
	}

	[Fact]
	public async Task PatronListIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();

		// Act
		var result = _patronUseCases.ListAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = new List<string>();
		await foreach (var item in result)
		{
			resultList.Add(item);
		}

		Assert.NotEmpty(resultList);
		Assert.Contains(patron.Id, resultList);
	}

	[Fact]
	public async Task PatronUpdateIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();

		// Act
		var result = await _patronUseCases.UpdateAsync(patron.Id, fixture.PatronUpdateModel);

		// Assert
		Assert.Equal(fixture.PatronUpdateModel.FirstName, result.FirstName);
	}

	[Fact]
	public async Task PatronDeleteIntegrationTest()
	{
		// Arrange
		var patron = await fixture.PreparePatron();

		// Act
		await _patronUseCases.DeleteAsync(patron.Id);

		// Assert
		var deletedAuthor = await _patronUseCases.GetAsync(patron.Id, false);
		Assert.NotNull(deletedAuthor);
		Assert.False(deletedAuthor.IsActive);
	}

}
