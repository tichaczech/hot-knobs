using Azure.Monitor.OpenTelemetry.AspNetCore;

using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Configuration;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// <see cref="IHostApplicationBuilder"/> implementation for Runtime.Server.
/// </summary>
[ServiceImplementation(typeof(ILogger))]
public class ServerDiagnosticsAzureMonitorHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public string Name => "azure-monitor";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		options.Logger?.LogInformation("Configuring server diagnostics using Azure Monitor...");

		// Initialize OpenTelemetry for Azure Monitor
		if (!builder.Configuration.HasApplicationInsightsConnectionString())
			throw new InvalidOperationException("Application Insights initialization failed! Application Insights connection string is not configured.");

		_ = builder.Services.AddOpenTelemetry().UseAzureMonitor();

		return builder;
	}
}
