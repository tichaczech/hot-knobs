using Azure.Identity;

using Fand.Runtime.Configuration;
using Fand.Runtime.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
///
/// </summary>
public abstract class ConfigurationHostBuilderFactory : IHostApplicationBuilderFactory
{
	private const string APP_CONFIGURATION_CLIENT_ID = "MEUC_APPCONFIGURATION_CLIENT_ID";
	private const string APP_CONFIGURATION_CONNECTION_STRING_NAME = "MEUC_APPCONFIGURATION_CONNECTION_STRING";

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Configuration;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		// Web SDK fix - Configuration file path is not correct in the debugger
		if (builder.Environment.IsLocal())
		{
			options.Logger?.LogWarning("Applying Web SDK debugger fix for configuration file path...");

			ApplyWebSDKDebuggerFix(builder, options);
		}

		// Add appsettings files
		var files = new List<Tuple<string, bool>> {
			new("appsettings.Module.json", false),
			new($"appsettings.Module.{builder.Environment.EnvironmentName}.json", true),
		};
		files.AddRange(GetConfigurationFiles(builder));

		var lastAppSettingsSource = builder.Configuration.Sources.Last(s => s.GetType() == typeof(JsonConfigurationSource));
		var index = builder.Configuration.Sources.IndexOf(lastAppSettingsSource);

		foreach (var file in files)
			AddAppSettingsSource(builder, file.Item1, file.Item2, ++index);

		var connectionString = builder.Configuration.GetValue<string>(APP_CONFIGURATION_CONNECTION_STRING_NAME);
		// TODO:
		// if (!String.IsNullOrEmpty(connectionString) || builder.Environment.IsAzureHosted())
		if (!String.IsNullOrEmpty(connectionString))
		{
			options.Logger?.LogInformation("Configuring application with AppConfiguration using connection string...");

			AddAppConfigurationSource(builder, builder.Configuration.GetConnectionStringEx(connectionString!)!);
		}

		// TODO: Improve the redaction of sensitive information
		var fullConfiguration = ((IConfigurationRoot)builder.Configuration).GetDebugView(context =>
		{
			if (context.Value == null)
				return "[NULL]";
			if (String.IsNullOrEmpty(context.Value))
				return "[EMPTY]";

			if (context.Path.StartsWith("ConnectionStrings:", StringComparison.InvariantCultureIgnoreCase))
				return "[REDACTED]";
			if (context.Key == "ConnectionString" && !(context.Value?.StartsWith("ConnectionStringName=", StringComparison.InvariantCultureIgnoreCase) ?? false))
				return "[REDACTED]";
			if (context.Key.EndsWith("Secret", StringComparison.InvariantCultureIgnoreCase))
				return "[REDACTED]";

			return context.Value;
		});
		options.Logger?.LogInformation($"Application was configured with the following configuration:\n{fullConfiguration}");

		return builder;
	}

	protected abstract IEnumerable<Tuple<string, bool>> GetConfigurationFiles(IHostApplicationBuilder hostBuilder);

	private static void AddAppConfigurationSource(IHostApplicationBuilder hostBuilder, string connectionString)
	{
		_ = hostBuilder.Configuration.AddAzureAppConfiguration(options =>
		{
			_ = options.ConfigureKeyVault(keyVault =>
			{
				var credentialOptions = new DefaultAzureCredentialOptions
				{
					ManagedIdentityClientId = hostBuilder.Configuration.GetValue<string>(APP_CONFIGURATION_CLIENT_ID)
				};
				_ = keyVault.SetCredential(new DefaultAzureCredential(credentialOptions));
			});
			_ = options.Connect(connectionString);
		});

		// HACK: Not possible to reorder sources, see https://github.com/dotnet/extensions/issues/1711#issuecomment-492296046
		// HACK: Not possible to create an instace of AzureAppConfigurationSource directly, see https://github.com/Azure/AppConfiguration-DotnetProvider/blob/4ce9ec53988dc27d3045406601ee9961c5e1cda8/src/Microsoft.Extensions.Configuration.AzureAppConfiguration/AzureAppConfigurationSource.cs#L8
		var lastAppSettingsSource = hostBuilder.Configuration.Sources.OfType<JsonConfigurationSource>().Last();
		var index = hostBuilder.Configuration.Sources.IndexOf(lastAppSettingsSource);
		var sharedConfigurationSource = hostBuilder.Configuration.Sources.Last();
		_ = hostBuilder.Configuration.Sources.Remove(sharedConfigurationSource);
		hostBuilder.Configuration.Sources.Insert(++index, sharedConfigurationSource); // Insert after the last appsettings.json file
	}

	private static void AddAppSettingsSource(IHostApplicationBuilder hostBuilder, string file, bool optional, int index)
	{
		var source = new JsonConfigurationSource
		{
			FileProvider = null,
			Path = Path.Join(AppDomain.CurrentDomain.BaseDirectory, file),
			Optional = optional,
			ReloadOnChange = false
		};
		source.ResolveFileProvider(); // SOURCE: https://stackoverflow.com/questions/69912329/why-does-jsonconfigurationsource-throw-filenotfoundexception-when-file-clearly-o
		hostBuilder.Configuration.Sources.Insert(index, source);
	}

	private static void ApplyWebSDKDebuggerFix(IHostApplicationBuilder hostBuilder, HostBuilderOptions options)
	{
		var appSettingsSources = hostBuilder.Configuration.Sources.OfType<JsonConfigurationSource>().ToArray();
		foreach (var appSettingsSource in appSettingsSources)
		{
			if (appSettingsSource.FileProvider is PhysicalFileProvider fileProvider && fileProvider.Root != AppDomain.CurrentDomain.BaseDirectory)
			{
				options.Logger?.LogWarning($"Removing and re-adding: \"{appSettingsSource.Path}\" with correct path...");

				var index = hostBuilder.Configuration.Sources.IndexOf(appSettingsSource);
				_ = hostBuilder.Configuration.Sources.Remove(appSettingsSource);
				AddAppSettingsSource(hostBuilder, appSettingsSource.Path!, appSettingsSource.Optional, index);
			}
		}
	}
}
