using System.Reflection;

using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging.EventSource;

using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// <see cref="IHostApplicationBuilder"/> implementation for Runtime.Diagnostics.
/// </summary>
public class DiagnosticsHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Logging;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		options.Logger?.LogInformation("Configuring diagnostics using OTel...");

		var serviceName = Environment.GetEnvironmentVariable("HOTKNOBS_OTEL_SERVICE_NAME") ?? Assembly.GetEntryAssembly()?.GetName().Name;
		var serviceNamespace = Environment.GetEnvironmentVariable("HOTKNOBS_OTEL_SERVICE_NAMESPACE") ?? "Hot Knobs";
		var serviceVersion = Environment.GetEnvironmentVariable("HOTKNOBS_OTEL_SERVICE_VERSION") ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

		options.Logger?.LogInformation("Using following parameters: \n\t- service name = {ServiceName}\n\t- namespace = {ServiceNamespace}\n\t- version = {ServiceVersion}", serviceName, serviceNamespace, serviceVersion);

		var activitySourceName = $"{serviceNamespace}/{serviceName}";

		var otelBuilder = builder.Services.AddOpenTelemetry();

		//=== Logging

		// Disable Debug & Envent Source logging
		_ = builder.Logging.AddFilter<DebugLoggerProvider>(null, LogLevel.None);
#pragma warning disable CA1416
		_ = builder.Logging.AddFilter<EventLogLoggerProvider>(null, LogLevel.None);
#pragma warning restore CA1416
		_ = builder.Logging.AddFilter<EventSourceLoggerProvider>(null, LogLevel.None);

		// TODO: Check this out!
		// Filter out logs (except warnings and errors)
		if (!builder.Environment.IsLocal())
			_ = builder.Logging.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Warning);

		_ = builder.Logging.Configure(options =>
		{
			options.ActivityTrackingOptions = ActivityTrackingOptions.Baggage | ActivityTrackingOptions.ParentId | ActivityTrackingOptions.SpanId | ActivityTrackingOptions.Tags | ActivityTrackingOptions.TraceId;
		});

		_ = builder.Logging.AddOpenTelemetry(options =>
		{
			options.IncludeScopes = true;
		});
		_ = otelBuilder.WithLogging(configure => { });

		//=== Tracing
		_ = otelBuilder.WithTracing(traceBuilder =>
		{
			_ = traceBuilder.AddSource(activitySourceName);

			_ = traceBuilder.AddEntityFrameworkCoreInstrumentation();
			_ = traceBuilder.AddHttpClientInstrumentation();
			_ = traceBuilder.AddRedisInstrumentation();
		});

		//=== Metrics
		_ = otelBuilder.WithMetrics(metricsBuilder =>
		{
			_ = metricsBuilder.AddHttpClientInstrumentation();
			_ = metricsBuilder.AddRuntimeInstrumentation();
		});

		//=== Application instrumentation
#pragma warning disable CA2000 // Dispose objects before losing scope
		_ = builder.Services.AddSingleton(new Instrumentation(activitySourceName, serviceVersion!));
#pragma warning restore CA2000 // Dispose objects before losing scope

		return builder;
	}
}
