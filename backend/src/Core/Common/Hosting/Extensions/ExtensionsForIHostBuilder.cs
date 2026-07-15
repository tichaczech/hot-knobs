using System.Reflection;
using System.Runtime.Loader;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Hosting;

/// <summary>
/// Options for configuring services using factories.
/// </summary>
public class ConfigureWithFactoriesOptions
{
	/// <summary>
	/// The configuration to use for configuring services.
	/// </summary>
	public IConfiguration? Configuration { get; set; }

	/// <summary>
	/// The search pattern to use for finding configuration files.
	/// </summary>
	public string? FileNameSearchPattern { get; set; }

	/// <summary>
	/// The factory to use for creating <see cref="AssemblyLoadContext"/> instances.
	/// </summary>
	public Func<string, AssemblyLoadContext> LoadContextFactory { get; set; }

	/// <summary>
	/// The logger to use for logging messages.
	/// </summary>
	public ILogger? Logger { get; set; }

	/// <summary>
	/// The collection of service names and their corresponding factory types.
	/// </summary>
	public IDictionary<string, string[]> ServiceConfiguredFactories { get; set; } = new Dictionary<string, string[]>();
}

/// <summary>
/// Provides extension methods for <see cref="IHostBuilder"/> to configure it using factories.
/// </summary>
public static class ExtensionsForIHostBuilder
{
	public static IHostApplicationBuilder ConfigureUsingFactories(this IHostApplicationBuilder hostBuilder, Action<ConfigureWithFactoriesOptions> configure)
	{
		return ConfigureUsingFactories<IHostApplicationBuilderFactory>(hostBuilder, configure);
	}

	public static IHostApplicationBuilder ConfigureUsingFactories<TFactory>(this IHostApplicationBuilder hostBuilder, Action<ConfigureWithFactoriesOptions> configure)
		where TFactory : class, IHostApplicationBuilderFactory
	{
		Guard.IsNotNull(hostBuilder, nameof(hostBuilder));
		Guard.IsNotNull(configure, nameof(configure));

		var options = new ConfigureWithFactoriesOptions();
		configure(options);

		Guard.IsNotNull(options.LoadContextFactory, nameof(options.LoadContextFactory));

		options.Logger?.LogInformation($"Configuring IHostBuilder with factories...");

		var hostBuilderFactories = AppDomain.CurrentDomain.GetFactories<TFactory>(opts =>
		{
			opts.LoadContextFactory = options.LoadContextFactory;
			opts.Logger = options.Logger;
		});

		var hostbuilderOptions = new HostBuilderOptions { Configuration = options.Configuration, Logger = options.Logger };

		var configurationFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Configuration).ToArray();
		foreach (var configurationFactory in configurationFactories)
		{
			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend IConfigurationBuilder...", configurationFactory.Name, configurationFactory.GetType().FullName);
			_ = configurationFactory.ConfigureBuilder(hostBuilder, hostbuilderOptions);
		}

		var loggingFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Logging).ToArray();
		foreach (var loggingFactory in loggingFactories)
		{
			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend ILoggerBuilder...", loggingFactory.Name, loggingFactory.GetType().FullName);
			_ = loggingFactory.ConfigureBuilder(hostBuilder, hostbuilderOptions);
		}

		var serviceFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Services).ToArray();
		var factoriesWithServiceImplementation = serviceFactories.Where(x => null != x.GetType().GetCustomAttribute<ServiceImplementationAttribute>()).ToDictionary(x => x, y => y.GetType().GetCustomAttribute<ServiceImplementationAttribute>()!.ImplementedService);

		options.Logger?.LogDebug("Following factories with service implementation were found: \n{0}", String.Join("\n", factoriesWithServiceImplementation.Select(x => String.Format("\t- {0}: {2} (in: '{1}')", x.Key.Name, x.Key.GetType().FullName, x.Value.FullName))));

		var serviceConfiguredImplementations = factoriesWithServiceImplementation.Values.Distinct().ToDictionary(x => x, y =>
		{
			if (options.ServiceConfiguredFactories.TryGetValue(y.FullName!, out var factories))
				return factories;

			return new string[] { };
		});

		options.Logger?.LogDebug("Following implementations were configured for services: \n{0}", String.Join("\n", serviceConfiguredImplementations.Select(x => String.Format("\t- {0}: [{1}]", x.Key.FullName, String.Join(",", x.Value)))));

		foreach (var hostBuilderFactory in serviceFactories)
		{
			if (factoriesWithServiceImplementation.TryGetValue(hostBuilderFactory, out var implementedService))
			{
				if (!serviceConfiguredImplementations[implementedService]?.Contains(hostBuilderFactory.Name) ?? false)
				{
					options.Logger?.LogDebug("Skipping implementation: '{0}' of service: '{2}' (in: '{1}') as it is not configured.", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName, implementedService.FullName);
					continue;
				}
				else
				{
					options.Logger?.LogDebug("Registering implementation: '{0}' of service: '{2}' (in: '{1}') as it is configured.", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName, implementedService.FullName);
				}
			}

			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend IHostBuilder...", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName);
			hostBuilderFactory.ConfigureBuilder(hostBuilder, new HostBuilderOptions { Configuration = options.Configuration, Logger = options.Logger });
		}

		return hostBuilder;
	}

	/// <summary>
	/// Configures the <see cref="IHostBuilder"/> using factories.
	/// </summary>
	/// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
	/// <param name="configure">The action to configure the <see cref="ConfigureWithFactoriesOptions"/>.</param>
	/// <returns>The configured <see cref="IHostBuilder"/>.</returns>
	public static IHostBuilder ConfigureUsingFactories(this IHostBuilder hostBuilder, Action<ConfigureWithFactoriesOptions> configure)
	{
		return ConfigureUsingFactories<IHostBuilderFactory>(hostBuilder, configure);
	}

	/// <summary>
	/// Configures the <see cref="IHostBuilder"/> using factories.
	/// </summary>
	/// <typeparam name="TFactory">The type of the factory.</typeparam>
	/// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
	/// <param name="configure">The action to configure the options.</param>
	/// <returns>The configured <see cref="IHostBuilder"/>.</returns>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="hostBuilder"/> or <paramref name="configure"/> is null.</exception>
	public static IHostBuilder ConfigureUsingFactories<TFactory>(this IHostBuilder hostBuilder, Action<ConfigureWithFactoriesOptions> configure)
		where TFactory : class, IHostBuilderFactory
	{
		Guard.IsNotNull(hostBuilder, nameof(hostBuilder));
		Guard.IsNotNull(configure, nameof(configure));

		var options = new ConfigureWithFactoriesOptions();
		configure(options);

		Guard.IsNotNull(options.LoadContextFactory, nameof(options.LoadContextFactory));

		options.Logger?.LogInformation($"Configuring IHostBuilder with factories...");

		var hostBuilderFactories = AppDomain.CurrentDomain.GetFactories<TFactory>(opts =>
		{
			opts.LoadContextFactory = options.LoadContextFactory;
			opts.Logger = options.Logger;
		});

		var hostbuilderOptions = new HostBuilderOptions { Configuration = options.Configuration, Logger = options.Logger };

		var configurationFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Configuration).ToArray();
		foreach (var configurationFactory in configurationFactories)
		{
			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend IConfigurationBuilder...", configurationFactory.Name, configurationFactory.GetType().FullName);
			_ = configurationFactory.ConfigureBuilder(hostBuilder, hostbuilderOptions);
		}

		var loggingFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Logging).ToArray();
		foreach (var loggingFactory in loggingFactories)
		{
			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend ILoggerBuilder...", loggingFactory.Name, loggingFactory.GetType().FullName);
			_ = loggingFactory.ConfigureBuilder(hostBuilder, hostbuilderOptions);
		}

		var serviceFactories = hostBuilderFactories.Where(x => x.Target == HostBuilderFactoryTarget.Services).ToArray();
		var factoriesWithServiceImplementation = serviceFactories.Where(x => null != x.GetType().GetCustomAttribute<ServiceImplementationAttribute>()).ToDictionary(x => x, y => y.GetType().GetCustomAttribute<ServiceImplementationAttribute>()!.ImplementedService);

		options.Logger?.LogDebug("Following factories with service implementation were found: \n{0}", String.Join("\n", factoriesWithServiceImplementation.Select(x => String.Format("\t- {0}: {2} (in: '{1}')", x.Key.Name, x.Key.GetType().FullName, x.Value.FullName))));

		var serviceConfiguredImplementations = factoriesWithServiceImplementation.Values.Distinct().ToDictionary(x => x, y =>
		{
			if (options.ServiceConfiguredFactories.TryGetValue(y.FullName!, out var factories))
				return factories;

			return new string[] { };
		});

		options.Logger?.LogDebug("Following implementations were configured for services: \n{0}", String.Join("\n", serviceConfiguredImplementations.Select(x => String.Format("\t- {0}: [{1}]", x.Key.FullName, String.Join(",", x.Value)))));

		foreach (var hostBuilderFactory in serviceFactories)
		{
			if (factoriesWithServiceImplementation.TryGetValue(hostBuilderFactory, out var implementedService))
			{
				if (!serviceConfiguredImplementations[implementedService]?.Contains(hostBuilderFactory.Name) ?? false)
				{
					options.Logger?.LogDebug("Skipping implementation: '{0}' of service: '{2}' (in: '{1}') as it is not configured.", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName, implementedService.FullName);
					continue;
				}
				else
				{
					options.Logger?.LogDebug("Registering implementation: '{0}' of service: '{2}' (in: '{1}') as it is configured.", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName, implementedService.FullName);
				}
			}

			options.Logger?.LogInformation("Using factory: '{0}' (in: '{1}') to extend IHostBuilder...", hostBuilderFactory.Name, hostBuilderFactory.GetType().FullName);
			hostBuilderFactory.ConfigureBuilder(hostBuilder, new HostBuilderOptions { Configuration = options.Configuration, Logger = options.Logger });
		}

		return hostBuilder;
	}
}

/// <summary>
/// Adds Configuration, Logging and Service registration(s) to <see cref="IHostBuilder" />.
/// Přidá konfiguraci, logování a registrace služeb pro projekt CzechPoint do <see cref="IHostBuilder" />.
/// </summary>
/// <param name="builder"><see cref="IHostBuilder"/> kam se bude přidávat.</param>
/// <returns><see cref="IHostBuilder" /> pro možnost řetězení volání.</returns>
/// <exception cref="HostBuilderInicializationException">pokud inicializace HostBuilderu selže</exception>
/// <remarks>
/// Přidání probíhá následovně:
/// <para>
/// Algoritmus projde všehny dostupné soubory v adresáři aplikace a pokud jejich jméno odpovídá masce "CzechPoint.Backend.*", pak se načtou jako assemblies. Ze všech těchto assemblies se pak najdou továrny pro HostBuilder:
/// <list type="bullet">
/// <item><see cref="IHostBuilderFactoryForConfiguration" /></item>
/// <item><see cref="IHostBuilderFactoryForLogging" /></item>
/// <item><see cref="IHostBuilderFactoryForServices" /></item>
/// </list>
/// a následně se provede jejich inicializace a spuštění. Továrny jsou spuštěné v uvedeném pořadí. Továrny pro <see cref="IHostBuilderFactoryForServices" /> se navíc ještě nejprve seřadí podle závislostí.
/// </para>
/// </remarks>
