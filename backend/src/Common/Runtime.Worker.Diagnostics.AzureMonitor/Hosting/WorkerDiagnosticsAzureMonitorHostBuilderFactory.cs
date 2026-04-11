using Azure.Monitor.OpenTelemetry.Exporter;

using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Configuration;

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// <see cref="IHostApplicationBuilder"/> implementation for Runtime.Server.
/// </summary>
[ServiceImplementation(typeof(ILogger))]
public class WorkerDiagnosticsAzureMonitorHostBuilderFactory : IHostApplicationBuilderFactory
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

		options.Logger?.LogInformation("Configuring worker diagnostics using Azure Monitor...");

		// Initialize OpenTelemetry for Azure Monitor
		if (!builder.Configuration.HasApplicationInsightsConnectionString())
			throw new InvalidOperationException("Application Insights initialization failed! Application Insights connection string is not configured.");

		var connectionString = builder.Configuration.GetApplicationInsightsConnectionString();

		_ = builder.Logging.AddOpenTelemetry(options => options.AddAzureMonitorLogExporter(o => o.ConnectionString = connectionString));

		_ = Sdk.CreateMeterProviderBuilder()
			.AddAzureMonitorMetricExporter(o => o.ConnectionString = connectionString)
			.Build();

		_ = Sdk.CreateTracerProviderBuilder()
			.AddAzureMonitorTraceExporter(o => o.ConnectionString = connectionString)
			.Build();

		_ = builder.Services.AddApplicationInsightsTelemetryWorkerService();

		return builder;
	}
}
