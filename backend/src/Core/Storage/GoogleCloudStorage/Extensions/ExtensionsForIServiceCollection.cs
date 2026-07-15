using CommunityToolkit.Diagnostics;

using Google.Cloud.Storage.V1;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

public static class ExtensionsForIServiceCollection
{
	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a StorageClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		services.Add(ServiceDescriptor.Describe(typeof(IBlobStorage), typeof(GoogleCloudStorageImpl), lifetime));

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="client">The <see cref="StorageClient"/> to use for interacting with Google Cloud Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="client"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, StorageClient client, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);

		services.Add(ServiceDescriptor.Singleton(client));
		_ = services.AddFandBlobStorageForGoogleCloudStorage(lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="StorageClient"/> for interacting with Google Cloud Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="clientFactory"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, Func<IServiceProvider, StorageClient> clientFactory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);

		services.Add(ServiceDescriptor.Describe(typeof(StorageClient), clientFactory, ServiceLifetime.Transient));
		_ = services.AddFandBlobStorageForGoogleCloudStorage(lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a StorageClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, object? serviceKey, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(IBlobStorage), serviceKey, (x, y) =>
		{
			var client = x.GetRequiredKeyedService<StorageClient>(y);
			var logger = x.GetRequiredService<ILogger<GoogleCloudStorageImpl>>();

			return new GoogleCloudStorageImpl(logger, client);
		}, lifetime));

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="client">The <see cref="StorageClient"/> to use for interacting with Google Cloud Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="client"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, object? serviceKey, StorageClient client, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);

		services.Add(ServiceDescriptor.KeyedSingleton(serviceKey, client));
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(serviceKey, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Google Cloud Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="StorageClient"/> for interacting with Google Cloud Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="clientFactory"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForGoogleCloudStorage(this IServiceCollection services, object? serviceKey, Func<IServiceProvider, object?, StorageClient> clientFactory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(StorageClient), serviceKey, clientFactory, ServiceLifetime.Transient));
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(serviceKey, lifetime);

		return services;
	}
}
