using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Contracts;


namespace thc.HotKnobs.Runtime.Hosting;

public class ServiceConfigurationHostBuilderFactory : IHostApplicationBuilderFactory
{
	// TODO: Find appropriate path for this configuration.
	private const string CONFIGURATION_SECTION_NAME = "HotKnobs:Services";

	public string Name => "default";


	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		// FIXME: This is a temporary solution to configure service configuration.
		options.Logger?.LogInformation("Configuring service configuration...");

		var serviceConfigurationSection = builder.Configuration.GetSection(CONFIGURATION_SECTION_NAME);
		_ = builder.Services.Configure<ServiceConfiguration>("", serviceConfigurationSection);

		return builder;
	}
}
