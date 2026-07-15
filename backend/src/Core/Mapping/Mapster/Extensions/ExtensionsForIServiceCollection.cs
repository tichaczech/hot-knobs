using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.DependencyInjection;

namespace Fand.Runtime.Mapping.Mapster;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register Fand IMapper using Mapster.
/// </summary>
public static class ExtensionsForIServiceCollection
{
	/// <summary>
	/// Adds <see cref="IMapper"/> implementation using Mapster to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a MapsterMapper has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandMapperForMapster(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		services.Add(ServiceDescriptor.Describe(typeof(IMapper), typeof(MapsterMapperImpl), lifetime));

		return services;
	}
}
