using Microsoft.Extensions.Hosting;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
///
/// </summary>
public class WorkerConfigurationHostBuilderFactory : ConfigurationHostBuilderFactory
{
	protected override IEnumerable<Tuple<string, bool>> GetConfigurationFiles(IHostApplicationBuilder hostBuilder)
	{
		ArgumentNullException.ThrowIfNull(hostBuilder);

		return [
			new Tuple<string, bool>("appsettings.Module.Worker.json", true),
			new Tuple<string, bool>($"appsettings.Module.Worker.{hostBuilder.Environment.EnvironmentName}.json", true)
		];
	}
}
