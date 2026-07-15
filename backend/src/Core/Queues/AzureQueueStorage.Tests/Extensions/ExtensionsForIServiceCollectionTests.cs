using Azure.Storage.Queues;

using FakeItEasy;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public class ExtensionsForIServiceCollectionTests
{
	private const string KEY = "alpha";

	// -------- Consumer: non-keyed --------

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(_ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();

		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(_ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();

		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		QueueServiceClient client = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClient_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(client, null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(_ =>
		{
			call++;
			return client;
		}, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();
		Assert.Equal(2, call); // once for client, once for consumer

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueConsumerForAzureQueueStorage(_ =>
		{
			call++;
			return client;
		}, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var c1 = provider.GetRequiredService<IQueueConsumer>();
		var c2 = provider.GetRequiredService<IQueueConsumer>();
		Assert.Equal(3, call); // one for client + two for consumers

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenFactoryIsNull()
	{
		// Arrange
		Func<IServiceProvider, QueueServiceClient> factory = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(factory, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(factory, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueConsumerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), _ => { }, ServiceLifetime.Transient));
	}

	// -------- Producer: non-keyed --------

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(_ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();

		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(_ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();

		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(client, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		QueueServiceClient client = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClient_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(client, null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(_ =>
		{
			call++;
			return client;
		}, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();
		Assert.Equal(2, call);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandQueueProducerForAzureQueueStorage(_ =>
		{
			call++;
			return client;
		}, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<QueueServiceClient>();
		var p1 = provider.GetRequiredService<IQueueProducer>();
		var p2 = provider.GetRequiredService<IQueueProducer>();
		Assert.Equal(3, call);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenFactoryIsNull()
	{
		// Arrange
		Func<IServiceProvider, QueueServiceClient> factory = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(factory, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(factory, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandQueueProducerForAzureQueueStorage(_ => A.Fake<QueueServiceClient>(), _ => { }, ServiceLifetime.Transient));
	}

	// -------- Consumer: keyed --------

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);

		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);

		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		QueueServiceClient client = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Scoped));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		Assert.Equal(2, call); // once for client, once for consumer

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.Same(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var c1 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		var c2 = provider.GetRequiredKeyedService<IQueueConsumer>(key);
		Assert.Equal(3, call); // one for client + two for consumers

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c1);
		_ = Assert.IsType<AzureQueueStorageConsumer>(c2);
		Assert.NotSame(c1, c2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueConsumer>);
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenFactoryIsNull()
	{
		// Arrange
		var services = new ServiceCollection();
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (Func<IServiceProvider, object?, QueueServiceClient>)null!, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (Func<IServiceProvider, object?, QueueServiceClient>)null!, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var services = new ServiceCollection();
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueConsumerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueConsumerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), _ => { }, ServiceLifetime.Transient));
	}

	// -------- Producer: keyed --------

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);

		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);

		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		QueueServiceClient client = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, client, _ => { }, ServiceLifetime.Scoped));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, _ => { });
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		Assert.Equal(2, call); // once for client, once for producer

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.Same(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<QueueServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, _ => { }, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<QueueServiceClient>(key);
		var p1 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		var p2 = provider.GetRequiredKeyedService<IQueueProducer>(key);
		Assert.Equal(3, call); // one for client + two for producers

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureQueueStorageProducer>(p1);
		_ = Assert.IsType<AzureQueueStorageProducer>(p2);
		Assert.NotSame(p1, p2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<QueueServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IQueueProducer>);
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenFactoryIsNull()
	{
		// Arrange
		var services = new ServiceCollection();
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (Func<IServiceProvider, object?, QueueServiceClient>)null!, _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (Func<IServiceProvider, object?, QueueServiceClient>)null!, _ => { }, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenConfigureOptionsIsNull()
	{
		// Arrange
		var services = new ServiceCollection();
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), null!));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), null!, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandQueueProducerForAzureQueueStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;
		var key = KEY;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), _ => { }));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandQueueProducerForAzureQueueStorage(key, (_, _) => A.Fake<QueueServiceClient>(), _ => { }, ServiceLifetime.Transient));
	}
}
