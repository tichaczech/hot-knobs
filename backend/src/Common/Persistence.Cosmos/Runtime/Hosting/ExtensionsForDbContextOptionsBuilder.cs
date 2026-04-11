using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Extensions;
using thc.HotKnobs.Runtime.Configuration;

namespace thc.HotKnobs.Runtime.Hosting;

public static class ExtensionsForDbContextOptionsBuilder
{
	public static DbContextOptionsBuilder UseCosmos(this DbContextOptionsBuilder options, CosmosConfiguration configuration, Action<CosmosDbContextOptionsBuilder>? cosmosOptionsAction = null, ILogger? logger = null)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		if (!String.IsNullOrEmpty(configuration.EndpointUri))
		{
			logger?.LogInformation("Using Azure Identity for CosmosDB connection EndpointUri:'{EndpointUri}',DatabaseName:'{DatabaseName}'", configuration.EndpointUri, configuration.DatabaseName);
			return options.UseCosmos(configuration.EndpointUri, configuration.AzureCredentials, configuration.DatabaseName, cosmosOptionsAction);
		}

		if (!String.IsNullOrEmpty(configuration.ConnectionString))
		{
			if (logger is not null)
			{
				if (configuration.ConnectionString.TryGetElementFromCosmosDbConnectionString("AccountEndpoint", out var accountEndpointValue))
				{
					logger.LogInformation("Using connection string for CosmosDB connection AccountEndpoint:'{AccountEndpoint}',DatabaseName:'{DatabaseName}'", accountEndpointValue, configuration.DatabaseName);
				}
				else
				{
					logger.LogInformation("Using connection string for CosmosDB connection DatabaseName:'{DatabaseName}'", configuration.DatabaseName);
				}
			}
			return options.UseCosmos(configuration.ConnectionString, configuration.DatabaseName, cosmosOptionsAction);
		}

		// TODO: Finish exception implementation!
		throw new NotImplementedException("");
	}
}
