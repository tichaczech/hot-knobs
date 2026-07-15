using CommunityToolkit.Diagnostics;

using Fand.Runtime.Mapping;

using Microsoft.Extensions.DependencyInjection;

namespace Fand.Runtime.Validation;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register Fand Mapper with validations (using <see cref="IValidator"/>).
/// </summary>
public static class ExtensionsForIServiceCollection
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
	public const string SERVICE_KEY = "###-fand-original";
#pragma warning restore CA1707 // Identifiers should not contain underscores

	/// <summary>
	/// Adds <see cref="IValidator"/> implementation using <see cref="ValidatingMapper"/> to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <remarks>
	/// This method replaces the existing <see cref="IMapper"/> registration with a new registration that uses <see cref="ValidatingMapper"/> to validate the destination object after mapping.
	/// </remarks>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	/// <exception cref="InvalidOperationException">When <see cref="IMapper"/> service is not registered or not registered with a valid implementation.</exception>
	public static IServiceCollection AddFandMapperWithValidations(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services, nameof(services));

		var originalDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IMapper)) ?? throw new InvalidOperationException("IMapper service is not registered. Please register IMapper before calling AddFandMapperWithValidations.");
		ServiceDescriptor keyedDescriptor;

		if (originalDescriptor.ImplementationType is not null)
		{
			keyedDescriptor = ServiceDescriptor.DescribeKeyed(originalDescriptor.ServiceType, SERVICE_KEY, originalDescriptor.ImplementationType, lifetime);
		}
		else if (originalDescriptor.ImplementationFactory is not null)
		{
			object factory(IServiceProvider services, object? key) => originalDescriptor.ImplementationFactory(services);

			keyedDescriptor = ServiceDescriptor.DescribeKeyed(originalDescriptor.ServiceType, SERVICE_KEY, factory, lifetime);
		}
#pragma warning disable IDE0045 // Convert to conditional expression
		else if (originalDescriptor.ImplementationInstance is not null)
#pragma warning restore IDE0045 // Convert to conditional expression
		{
			keyedDescriptor = ServiceDescriptor.KeyedSingleton(originalDescriptor.ServiceType, SERVICE_KEY, originalDescriptor.ImplementationInstance);
		}
		else
		{
			throw new InvalidOperationException("IMapper service is not registered with a valid implementation. Please register IMapper before calling AddFandMapperWithValidations.");
		}

		// Add the original IMapper registration with a unique key to the service collection
		services.Add(keyedDescriptor);
		// Remove the original IMapper registration and replace it with ValidatingMapper
		_ = services.Remove(originalDescriptor);

		services.Add(ServiceDescriptor.Describe(typeof(IMapper), typeof(ValidatingMapper), lifetime));

		return services;
	}
}
