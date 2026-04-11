using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Runtime.Hosting;

public abstract class MongoRepositoryContextHostBuilderFactory<TContext> : RepositoryContextHostBuilderFactory<TContext, PersistenceConfiguration>
	where TContext : MongoRepositoryContext
{
	/// <inheritdoc />
	protected override string ImplementationName => "MongoDB";

	/// <inheritdoc />
	protected override Type RepositoryType => typeof(MongoRepository<,>);

	/// <inheritdoc />
	protected override void ConfigureDbContextOptions(DbContextOptionsBuilder options, PersistenceConfiguration configuration, ILogger? logger)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentNullException.ThrowIfNull(configuration);

		_ = options.UseMongoDB(configuration.ConnectionString, configuration.DatabaseName);
	}
}
