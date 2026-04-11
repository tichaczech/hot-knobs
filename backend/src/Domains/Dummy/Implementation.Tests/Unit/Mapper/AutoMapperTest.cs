using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Mapper;

public class AutoMapperTest(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{

	[Fact]
	public void MappingConfigurationIsValid()
	{
		var mapper = unitTestsFixture.EntityMapper;

		var exception = Record.Exception(mapper.ConfigurationProvider.AssertConfigurationIsValid);

		Assert.Null(exception);
	}
}
