using Azure.Storage.Queues;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fand.Runtime.Queues.AzureQueueStorage;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register Fand IQueueProducer for Azure Queue Storage.
/// </summary>
public static class ExtensionsForIServiceCollection
{
	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a QueueServiceClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(configureOptions);

		_ = services.Configure(configureOptions);
		services.Add(ServiceDescriptor.Describe(typeof(IQueueConsumer), typeof(AzureQueueStorageConsumer), lifetime));

		return services;
	}

	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="client">The <see cref="QueueServiceClient"/> to use for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="client"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, QueueServiceClient client, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.Singleton(client));
		_ = services.AddFandQueueConsumerForAzureQueueStorage(configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="QueueServiceClient"/> for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="clientFactory"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, Func<IServiceProvider, QueueServiceClient> clientFactory, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.Describe(typeof(QueueServiceClient), clientFactory, ServiceLifetime.Transient));
		_ = services.AddFandQueueConsumerForAzureQueueStorage(configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <remarks>
	/// This method expects that a QueueServiceClient has been already registered in the service collection.
	/// </remarks>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueProducerForAzureQueueStorage(this IServiceCollection services, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(configureOptions);

		_ = services.Configure(configureOptions);
		services.Add(ServiceDescriptor.Describe(typeof(IQueueProducer), typeof(AzureQueueStorageProducer), lifetime));

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="client">The <see cref="QueueServiceClient"/> to use for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="client"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueProducerForAzureQueueStorage(this IServiceCollection services, QueueServiceClient client, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.Singleton(client));
		_ = services.AddFandQueueProducerForAzureQueueStorage(configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="QueueServiceClient"/> for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="clientFactory"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddFandQueueProducerForAzureQueueStorage(this IServiceCollection services, Func<IServiceProvider, QueueServiceClient> clientFactory, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.Describe(typeof(QueueServiceClient), clientFactory, ServiceLifetime.Transient));
		_ = services.AddFandQueueProducerForAzureQueueStorage(configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(configureOptions);

		_ = services.Configure(configureOptions);
		services.Add(ServiceDescriptor.DescribeKeyed(typeof(IQueueConsumer), serviceKey, (x, y) =>
		{
			var client = x.GetRequiredKeyedService<QueueServiceClient>(y);
			var logger = x.GetRequiredService<ILogger<AzureQueueStorageConsumer>>();
			var options = x.GetRequiredService<IOptions<AzureQueueStorageConsumerOptions>>();

			return new AzureQueueStorageConsumer(logger, client, options);
		}, lifetime));

		return services;
	}

	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="client">The <see cref="QueueServiceClient"/> to use for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="client"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, QueueServiceClient client, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.KeyedSingleton(serviceKey, client));
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(serviceKey, configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueConsumer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="QueueServiceClient"/> for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageConsumerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="clientFactory"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueConsumerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, Func<IServiceProvider, object?, QueueServiceClient> clientFactory, Action<AzureQueueStorageConsumerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(QueueServiceClient), serviceKey, clientFactory, ServiceLifetime.Transient));
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(serviceKey, configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueProducerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(configureOptions);

		_ = services.Configure(configureOptions);
		services.Add(ServiceDescriptor.DescribeKeyed(typeof(IQueueProducer), serviceKey, (x, y) =>
		{
			var client = x.GetRequiredKeyedService<QueueServiceClient>(y);
			var logger = x.GetRequiredService<ILogger<AzureQueueStorageProducer>>();
			var options = x.GetRequiredService<IOptions<AzureQueueStorageProducerOptions>>();

			return new AzureQueueStorageProducer(logger, client, options);
		}, lifetime));

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="client">The <see cref="QueueServiceClient"/> to use for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="client"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueProducerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, QueueServiceClient client, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(client);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.KeyedSingleton(serviceKey, client));
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(serviceKey, configureOptions, lifetime);

		return services;
	}

	/// <summary>
	/// Adds IQueueProducer implementation using Azure Queue Storage to the specified <see cref="IServiceCollection" />.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
	/// <param name="serviceKey">The key to identify the service.</param>
	/// <param name="clientFactory">The factory to use to create the <see cref="QueueServiceClient"/> for interacting with Azure Queue Storage.</param>
	/// <param name="configureOptions">An action to configure the provided <see cref="AzureQueueStorageProducerOptions"/>.</param>
	/// <param name="lifetime">The lifetime of the service.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="services"/>, <paramref name="clientFactory"/> or <paramref name="configureOptions"/> is <see langword="null"/>.</exception>
	public static IServiceCollection AddKeyedFandQueueProducerForAzureQueueStorage(this IServiceCollection services, object? serviceKey, Func<IServiceProvider, object?, QueueServiceClient> clientFactory, Action<AzureQueueStorageProducerOptions> configureOptions, ServiceLifetime lifetime = ServiceLifetime.Singleton)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(clientFactory);
		Guard.IsNotNull(configureOptions);

		services.Add(ServiceDescriptor.DescribeKeyed(typeof(QueueServiceClient), serviceKey, clientFactory, ServiceLifetime.Transient));
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(serviceKey, configureOptions, lifetime);

		return services;
	}
}
