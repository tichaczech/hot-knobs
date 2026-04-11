using System.Diagnostics;
using System.Runtime.Loader;

using Azure.Monitor.OpenTelemetry.Exporter;

using Fand.Runtime.Hosting;

using Microsoft.Build.Locator;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using thc.HotKnobs.Runtime.Configuration;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// Provides extension methods for <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class ExtensionsForIHostApplicationBuilder
{
	private const string ENV_STARTUP_LOGGER_LOG_LEVEL = "HOTKNOBS_STARTUP_LOGGER_LOG_LEVEL";
	private const string ENV_DATABASE = "HOTKNOBS_DATABASE";
	private const string CMD_DATABASE = "database";

	/// <summary>
	/// Initializes the <see cref="IHostApplicationBuilder"/> with a logger and configures it using factories.
	/// </summary>
	/// <typeparam name="T">The type used for the logger.</typeparam>
	/// <param name="hostBuilder">The <see cref="IHostApplicationBuilder"/> to initialize.</param>
	public static void InitializeBuilder<T>(this IHostApplicationBuilder hostBuilder)
	{
		ArgumentNullException.ThrowIfNull(hostBuilder);

#pragma warning disable CA2000 // Dispose objects before losing scope
		var loggerFactory = LoggerFactory.Create(loggerBuilder =>
		{
			var args = Environment.GetCommandLineArgs();
			// Only add logger(s) if not running in dotnet-swagger (Startup Arguments: /home/vscode/.dotnet/tools/.store/swashbuckle.aspnetcore.cli/7.2.0/swashbuckle.aspnetcore.cli/7.2.0/tools/net8.0/any/dotnet-swagger.dll, _tofile, --output, ../../openapi/server/domains-dummy.v2.json, thc.HotKnobs.Domains.Dummy.Server.dll, v2)
			if (!(args.Length > 2 && args.Any(x => x.EndsWith("dotnet-swagger.dll", StringComparison.OrdinalIgnoreCase)) && args.Any(x => x == "_tofile")))
			{
				var defaultLogLevel = LogLevel.Information;
				if (Enum.TryParse<LogLevel>(Environment.GetEnvironmentVariable(ENV_STARTUP_LOGGER_LOG_LEVEL), true, out var logLevel))
					defaultLogLevel = logLevel;

				_ = loggerBuilder.SetMinimumLevel(defaultLogLevel);
				var connectionString = hostBuilder.Configuration.GetApplicationInsightsConnectionString();
				if (!String.IsNullOrEmpty(connectionString))
				{
					_ = loggerBuilder.Configure(options =>
					{
						options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId | ActivityTrackingOptions.TraceId | ActivityTrackingOptions.ParentId | ActivityTrackingOptions.Baggage | ActivityTrackingOptions.Tags;
					});
					_ = loggerBuilder.AddOpenTelemetry(options =>
					{
						options.IncludeScopes = true;
						_ = options.AddAzureMonitorLogExporter(o =>
						{
							o.ConnectionString = connectionString;
						});
					});

					if (!Debugger.IsAttached)
						_ = loggerBuilder.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Warning); // Filter out logs (except warnings and errors)
				}

				_ = loggerBuilder.AddSimpleConsole();
			}
		});
#pragma warning restore CA2000 // Dispose objects before losing scope

		var startupLogger = loggerFactory.CreateLogger<T>();

		startupLogger.LogWarning("Initializing application...");
		startupLogger.LogInformation($"Startup parameters: {String.Join("\v ", Environment.GetCommandLineArgs())}");
		startupLogger.LogInformation($"Environment: {hostBuilder.Environment.EnvironmentName}");
		startupLogger.LogInformation($"Application Name: {hostBuilder.Environment.ApplicationName}");

		var databaseImplementation = GetDatabaseImplementation();
		startupLogger.LogInformation($"Database Implementation: {databaseImplementation}");

		_ = MSBuildLocator.RegisterDefaults(); // https://stackoverflow.com/questions/43330915/could-not-load-file-or-assembly-microsoft-build-frameworkvs-2017
		_ = hostBuilder.ConfigureUsingFactories(options =>
		{
			options.AssemblyLoadContextResolver = directory => new AssemblyLoadContext(directory);
			options.Logger = startupLogger;
			options.SearchDirectories = new Dictionary<string, string[]> { ["*"] = ["thc.HotKnobs.*.dll"] };
			options.ServiceConfiguredFactories = new Dictionary<string, string[]>
			{
				// TODO: Find a better way to register the database implementation
				{ "thc.HotKnobs.Runtime.Persistence.RepositoryContext", new [] { databaseImplementation } },
			};
		});
	}

	private static string GetDatabaseImplementation()
	{
		var args = Environment.GetCommandLineArgs();
		var databaseImplementation = args.FirstOrDefault(x => x.StartsWith($"--{CMD_DATABASE}=", StringComparison.OrdinalIgnoreCase))?.Split('=')[1];
		if (String.IsNullOrEmpty(databaseImplementation))
			databaseImplementation = Environment.GetEnvironmentVariable(ENV_DATABASE);

		if (String.IsNullOrEmpty(databaseImplementation))
			throw new InvalidOperationException($"Database implementation is not specified. Please set the environment variable '{ENV_DATABASE}' or use the command line argument '--{CMD_DATABASE}=<implementation>'.");

		return databaseImplementation;
	}
}
