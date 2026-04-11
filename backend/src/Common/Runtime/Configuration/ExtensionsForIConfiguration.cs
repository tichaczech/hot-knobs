using Microsoft.Extensions.Configuration;

namespace thc.HotKnobs.Runtime.Configuration;

/// <summary>
/// Extensions for <see cref="IConfiguration"/>.
/// </summary>
public static class ExtensionsForIConfiguration
{
	private const string APPLICATIONINSIGHTS_CONNECTION_STRING_ENV = "APPLICATIONINSIGHTS_CONNECTION_STRING";
	private const string APPLICATIONINSIGHTS_CONNECTION_STRING_CONFIG = "ApplicationInsights:ConnectionString";

	public static string? GetApplicationInsightsConnectionString(this IConfiguration configuration)
	{
		var connectionString = Environment.GetEnvironmentVariable(APPLICATIONINSIGHTS_CONNECTION_STRING_ENV);
		if (String.IsNullOrEmpty(connectionString))
		{
			connectionString = configuration.GetValue<string>(APPLICATIONINSIGHTS_CONNECTION_STRING_ENV);
			if (String.IsNullOrEmpty(connectionString))
			{
				connectionString = configuration.GetValue<string>(APPLICATIONINSIGHTS_CONNECTION_STRING_CONFIG);
			}
		}

		return connectionString;
	}

	/// <summary>
	/// Checks if the Application Insights connection string has been set.
	/// </summary>
	public static bool HasApplicationInsightsConnectionString(this IConfiguration configuration)
	{
		return !String.IsNullOrEmpty(GetApplicationInsightsConnectionString(configuration));
	}
}
