using System.Reflection;

using Microsoft.Extensions.Configuration;

namespace Fand.Runtime.Configuration;

/// <summary>
/// Extension metody pro <see cref="IConfiguration"/>.
/// </summary>
public static class ExtensionsForIConfiguration
{
	private const string CONNECTION_STRING_NAME = "ConnectionStringName=";

	public static string? GetConnectionStringEx(this IConfiguration configuration, string? connectionString)
	{
		if (connectionString?.StartsWith(CONNECTION_STRING_NAME) ?? false)
			return configuration.GetConnectionString(connectionString.Replace(CONNECTION_STRING_NAME, ""));

		return connectionString;
	}
}
