using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Service;

public class AuthorServiceTests(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{
	[Fact]
	public async Task CreateAsyncTest()
	{
		var response = await unitTestsFixture.AuthorUseCases.CreateAsync(default, unitTestsFixture.AuthorCreateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task GetAsyncTest()
	{
		var response = await unitTestsFixture.AuthorUseCases.GetAsync(BaseTestsFixture.GetRandomAuthor().Id);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task UpdateAsyncTest()
	{
		var response = await unitTestsFixture.AuthorUseCases.UpdateAsync(BaseTestsFixture.GetRandomAuthor().Id, unitTestsFixture.AuthorUpdateModel);

		Assert.NotNull(response);
	}

	[Fact]
	public async Task DeleteAsyncTest()
	{
		var authorId = BaseTestsFixture.GetRandomAuthor().Id;
		await unitTestsFixture.AuthorUseCases.DeleteAsync(authorId);

		var response = await unitTestsFixture.AuthorUseCases.GetAsync(authorId, false);

		Assert.NotNull(response);
	}

	[Fact]
	public Task ListAsyncTest()
	{
		var listOfModified = unitTestsFixture.AuthorUseCases.ListAsync(DateTimeOffset.Now.AddSeconds(-1));

		Assert.NotNull(listOfModified);
		return Task.CompletedTask;
	}
}
