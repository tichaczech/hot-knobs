using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class PatronServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateAsyncTest()
	{
		var response = await unitTestsFixture.PatronUseCases.CreateAsync(default, unitTestsFixture.PatronCreateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task GetAsyncTest()
	{
		var response = await unitTestsFixture.PatronUseCases.GetAsync(BaseTestsFixture.GetRandomPatron().Id);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task UpdateAsyncTest()
	{
		var response = await unitTestsFixture.PatronUseCases.UpdateAsync(BaseTestsFixture.GetRandomPatron().Id, unitTestsFixture.PatronUpdateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var patronId = BaseTestsFixture.GetRandomPatron().Id;
		await unitTestsFixture.PatronUseCases.DeleteAsync(patronId);

		var response = await unitTestsFixture.PatronUseCases.GetAsync(patronId, false);

		Assert.NotNull(response);
		Assert.False(response.IsActive);
	}

	[Fact]
	public void ListAsyncTest()
	{
		var listOfModified = unitTestsFixture.PatronUseCases.ListAsync(DateTimeOffset.Now.AddSeconds(-1));

		Assert.NotNull(listOfModified);
	}
}
