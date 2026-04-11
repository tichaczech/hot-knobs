using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace thc.HotKnobs.Runtime.Resources;

/// <summary>
///
/// </summary>
public static class ResourceCache
{
	/// <summary>
	/// Specifies the resource names
	/// </summary>
	public enum ResourceType
	{
		ErrorMessages,
		Messages,
		JipMessages
	}

	private static Dictionary<(string, ResourceType), Type> ResourceClassTypes { get; set; } = [];

	/// <summary>
	/// Gets resource Value by resource name and resource type.
	/// </summary>
	/// <param name="resourceName"></param>
	/// <param name="resourceType"></param>
	/// <param name="culture"></param>
	/// <returns></returns>
	public static string? GetResourceValue(string resourceName, ResourceType resourceType, CultureInfo? culture = default)
	{
		culture ??= Thread.CurrentThread.CurrentCulture;
		if (!ResourceClassTypes.TryGetValue((resourceName, resourceType), out var resourceClassType))
		{
			return null;
		}

		var resourceManagerProp =
			resourceClassType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
		var resourceManager = resourceManagerProp?.GetValue(null, null) as ResourceManager;
		return resourceManager?.GetString(resourceName, culture);
	}

	/// <summary>
	/// Initializes the resource types.
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	public static void InitializeResourceTypes(string defaultCulture)
	{
		var foundResources = Enum.GetValues<ResourceType>()
			.Cast<ResourceType>()
			.ToDictionary(resourceName => resourceName, FindResourceClasses);

		var culture = new CultureInfo(defaultCulture);
		foreach (var resToResClassType in foundResources)
		{
			foreach (var classType in resToResClassType.Value)
			{
				if (!TryGetResourceManager(classType, out var resourceManager))
				{
					continue;
				}

				var resourceSet = resourceManager?.GetResourceSet(culture, true, true);
				if (resourceSet == null)
				{
					continue;
				}

				ProcessResourceSet(resToResClassType.Key, resourceSet, classType);
			}
		}
	}

	private static IEnumerable<Type> FindResourceClasses(ResourceType resourceType)
	{
		var res = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(assembly => assembly.GetTypes())
			.Where(type => type.Name == resourceType.ToString() && type.IsClass)
			.Where(type => type.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static) != null);
		return res;
	}

	private static void ProcessResourceSet(ResourceType resourceType, ResourceSet resourceSet, Type resourceClassType)
	{
		foreach (DictionaryEntry entry in resourceSet)
		{
			if (entry.Key is not string key)
			{
				continue;
			}

			if (!ResourceClassTypes.TryAdd((key, resourceType), resourceClassType))
			{
				throw new InvalidOperationException($"Duplicate key {key} in resource {resourceClassType.Name}");
			}
		}
	}

	private static bool TryGetResourceManager(Type resourceClassType, out ResourceManager? resourceManager)
	{
		var resourceManagerProp =
			resourceClassType.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
		resourceManager = resourceManagerProp?.GetValue(null, null) as ResourceManager;
		return resourceManager != null;
	}
}
