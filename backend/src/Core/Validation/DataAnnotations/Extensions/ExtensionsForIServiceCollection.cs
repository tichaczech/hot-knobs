using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.DependencyInjection;

namespace Fand.Runtime.Validation.DataAnnotations;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register Fand IValidator for Data Annotations.
/// </summary>
public static class ExtensionsForIServiceCollection
{
	/// <summary>
	/// Adds <see cref="IValidator"/> implementation using Data Annotations to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandValidatorForDataAnnotations(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		// Scan assemblies for metadata classes and register them with the TypeDescriptor.
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray();
		foreach (var assembly in assemblies)
		{
			var typesWithMetadata = assembly.GetTypes().Where(type => type.IsClass).SelectMany(type => type.GetCustomAttributes<MetadataTypeAttribute>(false)
				.ToDictionary(metaAttr => type, metaAttr => metaAttr.MetadataClassType));

			foreach (var item in typesWithMetadata)
				TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(item.Key, item.Value), item.Key);
		}

		services.Add(ServiceDescriptor.Describe(typeof(IValidator), typeof(DataAnnotationsValidator), lifetime));

		return services;
	}
}
