using System.Reflection;
using System.Runtime.Loader;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Hosting;

/// <summary>
/// Options for configuring the behavior of the <see cref="ExtensionsForAppDomain.GetFactories"/> method.
/// </summary>
public class GetFactoriesOptions
{
	/// <summary>
	/// The factory to use for creating <see cref="AssemblyLoadContext"/> instances.
	/// </summary>
	public Func<string, AssemblyLoadContext> LoadContextFactory { get; set; }

	/// <summary>
	/// Gets or sets the logger to use for logging messages during factory discovery.
	/// </summary>
	public ILogger? Logger { get; set; } = null;

	/// <summary>
	/// Gets or sets a value indicating whether an exception should be thrown if an error occurs during factory discovery.
	/// </summary>
	public bool ThrowOnError { get; set; } = true;
}

/// <summary>
/// Options for loading assemblies into an AppDomain.
/// </summary>
public class LoadAssembliesOptions
{
	/// <summary>
	/// The factory to use for creating <see cref="AssemblyLoadContext"/> instances.
	/// </summary>
	public Func<string, AssemblyLoadContext> LoadContextFactory { get; set; }

	/// <summary>
	/// Gets or sets the logger to use for logging messages during assembly loading.
	/// </summary>
	public ILogger? Logger { get; set; } = null;

	/// <summary>
	/// Gets or sets a dictionary of search directories to use when loading assemblies.
	/// The key is the name of the directory, and the value is an array of file extensions to search for.
	/// </summary>
	public Dictionary<string, string[]> SearchDirectories { get; set; } = new Dictionary<string, string[]>();

	/// <summary>
	/// Gets or sets a value indicating whether to throw an exception if an error occurs while loading an assembly.
	/// </summary>
	public bool ThrowOnError { get; set; } = true;
}

/// <summary>
/// Provides extension methods for the <see cref="AppDomain"/> class.
/// </summary>
public static class ExtensionsForAppDomain
{
	/// <summary>
	/// Loads additional assemblies to the current AppDomain from the specified directories (and search patterns).
	/// </summary>
	/// <param name="appDomain">The AppDomain to load the assemblies into.</param>
	/// <param name="configure">A delegate to configure the options for loading the assemblies.</param>
	/// <returns>An IEnumerable of the loaded assemblies.</returns>
	public static IEnumerable<Assembly> LoadAssemblies(this AppDomain appDomain, Action<LoadAssembliesOptions> configure)
	{
		Guard.IsNotNull(appDomain, nameof(appDomain));
		Guard.IsNotNull(configure, nameof(configure));

		var options = new LoadAssembliesOptions();
		configure(options);

		Guard.IsNotNull(options.LoadContextFactory, nameof(options.LoadContextFactory));

		options.Logger?.LogInformation($"Loading additional assemblies to current AppDomain...");

		var result = new List<Assembly>();
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDynamic == false).ToArray();
		options.Logger?.LogDebug("Following assemblies are loaded in current AppDomain: \n{0}", String.Join("\n", assemblies.Select(a => String.Format("\t- {0}", a.FullName))));
		foreach (var searchDirectory in options.SearchDirectories)
		{
			var directory = searchDirectory.Key;
			if (directory == "*")
				directory = AppDomain.CurrentDomain.BaseDirectory;
			if (directory.StartsWith("."))
				directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
			directory = Path.GetFullPath(directory);

			if (!Directory.Exists(directory))
			{
				var logMessage = $"Directory: '{directory}' does not exist!";

				if (options.ThrowOnError)
					// TODO: Change exception type!
					throw new FactoryCreationException(logMessage);

				options.Logger?.LogWarning(logMessage);
			}

			var loadContext = AssemblyLoadContext.Default;
			if (directory != AppDomain.CurrentDomain.BaseDirectory)
			{
				options.Logger?.LogDebug($"Creating custom assembly load context for directory: '{directory}'...");

				loadContext = options.LoadContextFactory(directory);
				loadContext.Resolving += (context, assembly) =>
				{
					// TODO: Consider using eager loading for referenced assemblies instead of lazy loading (this handler)!
					options.Logger?.LogDebug($"Resolving dependent assembly: '{assembly.FullName}' in assembly load context : '{context}'...");

					var path = Path.Combine(directory, assembly.Name + ".dll");
					// TODO: Improve file path construction from assembly name using AssemblyDependencyResolver!
					// var resolver = new AssemblyDependencyResolver(Path.Combine(directory, "tmp"));
					// var path = resolver.ResolveAssemblyToPath(assembly)!;

					options.Logger?.LogInformation($"Loading file: '{path}' into current AppDomain.");
					return loadContext.LoadFromAssemblyPath(path);
				};
			}

			options.Logger?.LogDebug($"Loading assemblies from directory: '{directory}'...");
			foreach (var searchPattern in searchDirectory.Value)
			{
				var availableFiles = Directory.GetFiles(directory, searchPattern);
				options.Logger?.LogDebug("Following files were found in directory: '{0}' for search pattern: '{1}':\n{3}", directory, searchPattern, String.Join("\n", availableFiles.Select(a => String.Format("\t- {0}", a))));

				var assembliesToLoad = availableFiles.Where(x => !assemblies.Select(a => a.Location).Contains(x, StringComparer.InvariantCultureIgnoreCase) || x != Assembly.GetEntryAssembly()!.Location).ToArray();
				options.Logger?.LogDebug("Following assemblies are missing in current AppDomain:\n{0}", String.Join("\n", assembliesToLoad.Select(a => String.Format("\t- {0}", a))));
				foreach (var assemblyToLoad in assembliesToLoad)
				{
					// TODO: Improve this sanity check!
					if (!assemblyToLoad.EndsWith(".dll"))
					{
						options.Logger?.LogDebug($"Skipping loading file: '{assemblyToLoad}' as it is not loadable assembly.");
						continue;
					}

					try
					{
						var assembly = loadContext.LoadFromAssemblyPath(assemblyToLoad);

						options.Logger?.LogInformation($"Loading file: '{assemblyToLoad}' into current AppDomain.");
						result.Add(assembly);
					}
					catch (BadImageFormatException)
					{
						options.Logger?.LogInformation($"Failed loading file: '{assemblyToLoad}' as it is not loadable assembly.");
						continue;
					}

				}
			}
		}
		assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.IsDynamic == false).ToArray();
		options.Logger?.LogDebug("Following assemblies are loaded in current AppDomain: \n{0}", String.Join("\n", assemblies.Select(a => String.Format("\t- {0}", a.FullName))));

		return assemblies;
	}

	/// <summary>
	/// Searches the <see cref="AppDomain" /> for factories of the defined type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Factory type.</typeparam>
	/// <param name="appDomain">The <see cref="AppDomain" /> used to search for factories.</param>
	/// <param name="configure">The <see cref="GetFactoriesOptions"/> configuration delegate.</param>
	/// <returns>(Newly created) instances of factories.</returns>
	/// <exception cref="FactoryCreationException">factory creation failed.</exception>
	public static IEnumerable<T> GetFactories<T>(this AppDomain appDomain, Action<GetFactoriesOptions> configure)
		where T : class, IFactory
	{
		Guard.IsNotNull(appDomain, nameof(appDomain));
		Guard.IsNotNull(configure, nameof(configure));

		var options = new GetFactoriesOptions();
		configure(options);

		Guard.IsNotNull(options.LoadContextFactory, nameof(options.LoadContextFactory));

		options.Logger?.LogInformation($"Getting factories of type: '{typeof(T)}'...");

		var assemblies = appDomain.LoadAssemblies(a =>
		{
			a.LoadContextFactory = options.LoadContextFactory;
			a.Logger = options.Logger;
			a.SearchDirectories.Add("*", new[] { "*.dll" });
		}).ToArray();
		var types = assemblies.SelectMany(a => a.GetTypes()).Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract).ToArray();
		options.Logger?.LogDebug($"Following factories of type: '{typeof(T).FullName}' were found: \n{{0}}", String.Join("\n", types.Select(a => String.Format("\t- {0}", a.FullName))));

		var result = new List<T>();
		foreach (var type in types)
		{
			try
			{
				var factory = (T)Activator.CreateInstance(type)!;
				options.Logger?.LogInformation($"Factory: '{type.FullName}' of name: '{factory.Name}' was loaded.");

				result.Add(factory);
			}
			catch (Exception ex)
			{
				var logMessage = $"Loading factory: '{type.FullName}' failed!";

				if (options.ThrowOnError)
					throw new FactoryCreationException(logMessage, ex);

				options.Logger?.LogWarning(ex, logMessage);
			}
		}

		return result;
	}
}
