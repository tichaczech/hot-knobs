using FakeItEasy;

using Google.Cloud.Storage.V1;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

public class ExtensionsForIServiceCollectionTests
{
	private const string KEY = "alpha";

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage();
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage(ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage());
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClient_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecifiedAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage(client);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<StorageClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage(client, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<StorageClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		StorageClient client = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecifiedAndClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage(_ =>
		{
			call++;
			return client;
		});
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<StorageClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();
		Assert.Equal(2, call); // once for the client, once for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<StorageClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForGoogleCloudStorage(_ =>
		{
			call++;
			return client;
		}, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<StorageClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();
		Assert.Equal(3, call); // once for the client, twice for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		Func<IServiceProvider, StorageClient> clientFactory = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(clientFactory, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(_ => client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForGoogleCloudStorage(_ => client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<StorageClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<StorageClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		StorageClient client = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, client, ServiceLifetime.Scoped));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<StorageClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, (_, _) =>
		{
			call++;
			return client;
		});
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<StorageClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		Assert.Equal(2, call); // once for the client, once for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<StorageClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<StorageClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		Assert.Equal(3, call); // once for the client, twice for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s1);
		_ = Assert.IsType<GoogleCloudStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<StorageClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		Func<IServiceProvider, object?, StorageClient> clientFactory = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, clientFactory, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForGoogleCloudStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<StorageClient>();
		var key = KEY;
		IServiceCollection services = null!;

		StorageClient clientFactory(IServiceProvider serviceProvider, object? key) => client;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForGoogleCloudStorage(key, clientFactory, ServiceLifetime.Scoped));
	}
}
