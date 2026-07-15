using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Configuration;

namespace thc.HotKnobs.Runtime.Hosting;

public class AuthenticationConfigurationHostBuilderFactory : IHostApplicationBuilderFactory
{
	internal const string CONFIGURATION_SECTION_NAME = "HotKnobs:Runtime:Authentication";

	public string Name => "default";

	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		// FIXME: This is a temporary solution to configure authentication configuration.
		options.Logger?.LogInformation("Configuring authentication configuration...");

		var authenticationConfigurationSection = builder.Configuration.GetSection(CONFIGURATION_SECTION_NAME);
		_ = builder.Services.Configure<AuthenticationConfiguration>("", authenticationConfigurationSection);

		return builder;
	}
}
