using Azure.Storage.Blobs;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Storage.AzureBlobStorage;

public static class ExtensionsForIServiceCollection
{
	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a BlobServiceClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForAzureBlobStorage(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		services.Add(ServiceDescriptor.Describe(typeof(IBlobStorage), typeof(AzureBlobStorageImpl), lifetime));

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="client">The <see cref="BlobServiceClient"/> to use for interacting with Azure Blob Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="client"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForAzureBlobStorage(this IServiceCollection services, BlobServiceClient client, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);

		services.Add(ServiceDescriptor.Singleton(client));
		_ = services.AddFandBlobStorageForAzureBlobStorage(lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="BlobServiceClient"/> for interacting with Azure Blob Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="clientFactory"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandBlobStorageForAzureBlobStorage(this IServiceCollection services, Func<IServiceProvider, BlobServiceClient> clientFactory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);

		services.Add(ServiceDescriptor.Describe(typeof(BlobServiceClient), clientFactory, ServiceLifetime.Transient));
		_ = services.AddFandBlobStorageForAzureBlobStorage(lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a BlobServiceClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForAzureBlobStorage(this IServiceCollection services, object? serviceKey, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(IBlobStorage), serviceKey, (x, y) =>
		{
			var client = x.GetRequiredKeyedService<BlobServiceClient>(y);
			var logger = x.GetRequiredService<ILogger<AzureBlobStorageImpl>>();

			return new AzureBlobStorageImpl(logger, client);
		}, lifetime));

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="client">The <see cref="BlobServiceClient"/> to use for interacting with Azure Blob Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="client"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForAzureBlobStorage(this IServiceCollection services, object? serviceKey, BlobServiceClient client, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);

		services.Add(ServiceDescriptor.KeyedSingleton(serviceKey, client));
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(serviceKey, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IBlobStorage implementation using Azure Blob Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">An object that specifies the key of service object to get.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="BlobServiceClient"/> for interacting with Azure Blob Storage.</param>
	/// <param name="lifetime">The <see cref="ServiceLifetime"/> with which to register the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="clientFactory"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandBlobStorageForAzureBlobStorage(this IServiceCollection services, object? serviceKey, Func<IServiceProvider, object?, BlobServiceClient> clientFactory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(BlobServiceClient), serviceKey, clientFactory, ServiceLifetime.Transient));
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(serviceKey, lifetime);

		return services;
	}
}
