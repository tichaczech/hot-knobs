using Azure.Storage.Blobs;

using FakeItEasy;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Fand.Runtime.Storage.AzureBlobStorage;

public class ExtensionsForIServiceCollectionTests
{
	private const string KEY = "alpha";

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage();
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddSingleton(client);

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage(ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage());
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClient_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecifiedAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage(client);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<BlobServiceClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage(client, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<BlobServiceClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		BlobServiceClient client = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecifiedAndClientFactoryIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage(_ =>
		{
			call++;
			return client;
		});
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<BlobServiceClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();
		Assert.Equal(2, call); // once for the client, once for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<BlobServiceClient>();
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddFandBlobStorageForAzureBlobStorage(_ =>
		{
			call++;
			return client;
		}, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredService<BlobServiceClient>();
		var s1 = provider.GetRequiredService<IBlobStorage>();
		var s2 = provider.GetRequiredService<IBlobStorage>();
		Assert.Equal(3, call); // once for the client, twice for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		Func<IServiceProvider, BlobServiceClient> clientFactory = null!;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(clientFactory, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(_ => client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddFandBlobStorageForAzureBlobStorage(_ => client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_ShouldRegisterAsSingleton_WhenLifetimeIsNotSpecified()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_ShouldRegisterAsTransient_WhenLifetimeIsTransient()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();
		_ = services.AddKeyedSingleton(key, client);

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClient_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<BlobServiceClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClient_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<BlobServiceClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClient_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		BlobServiceClient client = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClient_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, client, ServiceLifetime.Scoped));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldRegisterAsSingleton_WhenClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<BlobServiceClient>();
		var key = KEY;
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key, (_, _) =>
		{
			call++;
			return client;
		});
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<BlobServiceClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		Assert.Equal(2, call); // once for the client, once for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.Same(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldRegisterAsTransient_WhenLifetimeIsTransientAndClientIsProvided()
	{
		// Arrange
		var call = 0;
		var client = A.Fake<BlobServiceClient>();
		var key = "beta";
		var services = new ServiceCollection();
		_ = services.AddLogging();

		// Act
		_ = services.AddKeyedFandBlobStorageForAzureBlobStorage(key, (_, _) =>
		{
			call++;
			return client;
		}, ServiceLifetime.Transient);
		var provider = services.BuildServiceProvider();

		// Assert
		var resolvedClient = provider.GetRequiredKeyedService<BlobServiceClient>(key);
		var s1 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		var s2 = provider.GetRequiredKeyedService<IBlobStorage>(key);
		Assert.Equal(3, call); // once for the client, twice for the IBlobStorage

		Assert.Same(client, resolvedClient);
		_ = Assert.IsType<AzureBlobStorageImpl>(s1);
		_ = Assert.IsType<AzureBlobStorageImpl>(s2);
		Assert.NotSame(s1, s2);

		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<BlobServiceClient>);
		_ = Assert.Throws<InvalidOperationException>(provider.GetRequiredService<IBlobStorage>);
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldThrow_WhenClientIsNull()
	{
		// Arrange
		Func<IServiceProvider, object?, BlobServiceClient> clientFactory = null!;
		var key = KEY;
		var services = new ServiceCollection();

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, clientFactory, ServiceLifetime.Transient));
	}

	[Fact]
	public void AddKeyedFandBlobStorageForAzureBlobStorage_WithClientFactory_ShouldThrow_WhenServicesIsNull()
	{
		// Arrange
		var client = A.Fake<BlobServiceClient>();
		BlobServiceClient clientFactory(IServiceProvider serviceProvider, object? key) => client;
		var key = KEY;
		IServiceCollection services = null!;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, clientFactory));
		_ = Assert.Throws<ArgumentNullException>(() => services.AddKeyedFandBlobStorageForAzureBlobStorage(key, clientFactory, ServiceLifetime.Scoped));
	}
}
