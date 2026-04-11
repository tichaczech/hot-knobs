using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Runtime.Hosting;

public abstract class CosmosRepositoryContextHostBuilderFactory<TContext> : RepositoryContextHostBuilderFactory<TContext, CosmosConfiguration>
	where TContext : CosmosRepositoryContext
{
	/// <inheritdoc />
	protected override string ImplementationName => "CosmosDB";

	/// <inheritdoc />
	protected override Type RepositoryType => typeof(CosmosRepository<,>);

	/// <inheritdoc />
	protected override void ConfigureDbContextOptions(DbContextOptionsBuilder options, CosmosConfiguration configuration, ILogger? logger)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentNullException.ThrowIfNull(configuration);

		_ = options.UseCosmos(configuration, cosmosOptions =>
		{
			_ = cosmosOptions.ConnectionMode(ConnectionMode.Gateway);
			_ = cosmosOptions.HttpClientFactory(() => new HttpClient(new HttpClientHandler()
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			}));
		});
	}
}
