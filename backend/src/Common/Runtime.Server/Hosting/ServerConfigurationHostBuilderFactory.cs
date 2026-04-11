using Microsoft.Extensions.Hosting;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
///
/// </summary>
public class ServerConfigurationHostBuilderFactory : ConfigurationHostBuilderFactory
{
	protected override IEnumerable<Tuple<string, bool>> GetConfigurationFiles(IHostApplicationBuilder hostBuilder)
	{
		ArgumentNullException.ThrowIfNull(hostBuilder);

		return [
			new Tuple<string, bool>("appsettings.Module.Server.json", true),
			new Tuple<string, bool>($"appsettings.Module.Server.{hostBuilder.Environment.EnvironmentName}.json", true)
		];
	}
}
