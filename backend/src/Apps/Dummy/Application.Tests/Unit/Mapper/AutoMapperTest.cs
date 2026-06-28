using Xunit;

namespace thc.HotKnobs.Domains.Dummy.Runtime.Tests.Unit.Mapper;

public class AutoMapperTest(UnitTestsFixture unitTestsFixture) : IClassFixture<UnitTestsFixture>
{

	[Fact]
	public void MappingConfigurationIsValid()
	{
		var exception = Record.Exception(() => unitTestsFixture.MappingConfig.Compile());

		Assert.Null(exception);
	}
}
