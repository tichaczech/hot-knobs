using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Runtime.Hosting;

public abstract class PostgresRepositoryContextHostBuilderFactory<TContext> : RepositoryContextHostBuilderFactory<TContext, PersistenceConfiguration>
	where TContext : PostgresRepositoryContext
{
	/// <inheritdoc />
	protected override string ImplementationName => "PostgreSQL";

	/// <inheritdoc />
	protected override Type RepositoryType => typeof(PostgresRepository<,>);

	/// <inheritdoc />
	protected override void ConfigureDbContextOptions(DbContextOptionsBuilder options, PersistenceConfiguration configuration, ILogger? logger)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentNullException.ThrowIfNull(configuration);

		var connectionString = configuration.ConnectionString + $";Database={configuration.DatabaseName};";
		_ = options.UseNpgsql(connectionString, npgsqlOptions =>
		{
			_ = npgsqlOptions.EnableRetryOnFailure();
			_ = npgsqlOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);
		});
	}
}
