using System.Globalization;

using Fand.Runtime.Configuration;
using Fand.Runtime.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;
using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Persistence;

namespace thc.HotKnobs.Runtime.Hosting;

[ServiceImplementation(typeof(RepositoryContext))]
public abstract class RepositoryContextHostBuilderFactory<TContext, TConfiguration> : IHostApplicationBuilderFactory
	where TContext : RepositoryContext
	where TConfiguration : PersistenceConfiguration
{
	private const string CONFIGURATION_SECTION_NAME = "HotKnobs:Runtime:Persistence:";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public string Name => ImplementationName.ToLower(CultureInfo.CurrentCulture);

	protected abstract string ImplementationName { get; }

	protected abstract Type RepositoryType { get; }

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		// TODO: Add validation for RepositoryType

		var configuration = builder.Configuration.GetSection(CONFIGURATION_SECTION_NAME + ImplementationName).Get<TConfiguration>()!;
		if (!String.IsNullOrEmpty(configuration.ConnectionString))
			configuration.ConnectionString = builder.Configuration.GetConnectionStringEx(configuration.ConnectionString)!;

		_ = builder.Services.AddDbContext<TContext>(contextOptions =>
		{
			_ = contextOptions.EnableDetailedErrors();
			ConfigureDbContextOptions(contextOptions, configuration, options.Logger);
		}, ServiceLifetime.Transient);

		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray();
		var entityTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Entity))).ToArray();

		foreach (var entityType in entityTypes)
		{
			var serviceType = typeof(IRepository<>).MakeGenericType(entityType);
			var implementationType = RepositoryType.MakeGenericType(entityType, typeof(TContext));

			_ = builder.Services.AddScoped(serviceType, implementationType);
		}

		return builder;
	}

	protected abstract void ConfigureDbContextOptions(DbContextOptionsBuilder options, TConfiguration configuration, ILogger? logger = null);
}
