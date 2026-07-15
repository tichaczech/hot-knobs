using Azure.Core;
using Azure.Identity;

namespace thc.HotKnobs.Runtime.Configuration;

/// <summary>
/// Configuration for Cosmos DB.
/// </summary>
public class CosmosConfiguration : PersistenceConfiguration
{
	/// <summary>
	/// The Azure credentials to be used for authentication.
	/// </summary>
	public TokenCredential AzureCredentials { get; set; } = new DefaultAzureCredential();

	/// <summary>
	/// The URI of the Cosmos DB endpoint.
	/// </summary>
#pragma warning disable CA1056 // URI-like properties should not be strings
	public string EndpointUri { get; set; } = String.Empty;
#pragma warning restore CA1056 // URI-like properties should not be strings
}
