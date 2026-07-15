using System.Text;

using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Xunit;

namespace Fand.Runtime.Storage.AzureBlobStorage;

public sealed class AzureBlobStorageIntegrationTests : IClassFixture<BlobServiceClientFixture>, IDisposable
{
	private const string NON_EXISTENT_CONTAINER = "non-existent-container";

	private const string NON_EXISTENT_BLOB = "non-existent-file.txt";

	private readonly BlobClient _blobClient;

	private readonly BlobContainerClient _blobContainerClient;

	private readonly string _containerName = Guid.NewGuid().ToString();

	private readonly string _contentType = "text/plain";

	private readonly IDictionary<string, string> _blobMetadata = new Dictionary<string, string>
	{
		{ "key1", "value1" },
		{ "key2", "value2" }
	};

	private readonly string _blobName = "tests/text-file.txt";

	private readonly string _blobContent = "Hello, World!";

	private readonly IBlobStorage _impl;

	public AzureBlobStorageIntegrationTests(BlobServiceClientFixture fixture)
	{
		_blobContainerClient = fixture.BlobServiceClient.GetBlobContainerClient(_containerName);
		_blobClient = _blobContainerClient.GetBlobClient(_blobName);

		if (_blobContainerClient.Exists())
			_ = _blobContainerClient.Delete();
		_ = _blobContainerClient.Create();

		var blobHttpHeaders = new BlobHttpHeaders { ContentType = _contentType };
		var conditions = new BlobRequestConditions { IfNoneMatch = new ETag("*") };
		var options = new BlobUploadOptions
		{
			Conditions = conditions,
			HttpHeaders = blobHttpHeaders,
			Metadata = _blobMetadata
		};

		_ = _blobClient.Upload(new BinaryData(Encoding.UTF8.GetBytes(_blobContent)), options);
		_ = _blobClient.SetMetadata(_blobMetadata);

		var logger = NullLoggerFactory.Instance.CreateLogger<AzureBlobStorageImpl>();
		_impl = new AzureBlobStorageImpl(logger, fixture.BlobServiceClient);
	}

	public void Dispose()
	{
		if (_blobContainerClient.Exists())
			_ = _blobContainerClient.Delete();
	}

	#region DeleteBlobAsync

	[Fact]
	public async Task DeleteBlobAsync_ShouldSucceed_WhenBlobExists()
	{
		// Arrange

		// Act
		await _impl.DeleteBlobAsync(_containerName, _blobName);

		// Assert
		Assert.False(await _blobClient.ExistsAsync());
	}

	[Fact]
	public async Task DeleteBlobAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExists()
	{
		// Arrange
		var containerName = NON_EXISTENT_CONTAINER;

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(() => _impl.DeleteBlobAsync(containerName, _blobName));
	}

	[Fact]
	public async Task DeleteBlobAsync_ShouldThrowBlobNotFoundException_WhenBlobDoesNotExist()
	{
		// Arrange
		var blobName = NON_EXISTENT_BLOB;

		// Act & Assert
		_ = await Assert.ThrowsAsync<BlobNotFoundException>(() => _impl.DeleteBlobAsync(_containerName, blobName));
	}

	#endregion // DeleteBlobAsync

	#region DownloadBlobAsync

	[Fact]
	public async Task DownloadBlobAsync_ShouldSucceed_WhenBlobExists()
	{
		// Arrange

		// Act
		var result = await _impl.DownloadBlobAsync(_containerName, _blobName);

		// Assert
		Assert.Equal(Encoding.UTF8.GetBytes(_blobContent), result);
	}

	[Fact]
	public async Task DownloadBlobAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExist()
	{
		// Arrange
		var containerName = NON_EXISTENT_CONTAINER;

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(() => _impl.DownloadBlobAsync(containerName, _blobName));
	}

	[Fact]
	public async Task DownloadBlobAsync_ShouldThrowBlobNotFoundException_WhenBlobDoesNotExist()
	{
		// Arrange
		var blobName = NON_EXISTENT_BLOB;

		// Act & Assert
		_ = await Assert.ThrowsAsync<BlobNotFoundException>(() => _impl.DownloadBlobAsync(_containerName, blobName));
	}

	#endregion // DownloadBlobAsync

	#region GetBlobMetadataAsync

	[Fact]
	public async Task GetBlobMetadataAsync_ShouldSucceed_WhenBlobExists()
	{
		// Arrange

		// Act
		var result = await _impl.GetBlobMetadataAsync(_containerName, _blobName);

		// Assert
		Assert.Equal(_blobMetadata, result);
	}

	[Fact]
	public async Task GetBlobMetadataAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExist()
	{
		// Arrange
		var containerName = NON_EXISTENT_CONTAINER;

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(() => _impl.GetBlobMetadataAsync(containerName, _blobName));
	}

	[Fact]
	public async Task GetBlobMetadataAsync_ShouldThrowBlobNotFoundException_WhenBlobDoesNotExist()
	{
		// Arrange
		var blobName = NON_EXISTENT_BLOB;

		// Act & Assert
		_ = await Assert.ThrowsAsync<BlobNotFoundException>(() => _impl.GetBlobMetadataAsync(_containerName, blobName));
	}

	#endregion // GetBlobMetadataAsync

	#region ListBlobsAsync

	[Fact]
	public async Task ListBlobsAsync_ShouldSucceed_WhenRecursiveIsFalse()
	{
		// Arrange
		var namePrefix = _blobName.Split("/")[0];
		var blobName = "tests/subdir/another-text-file.txt";
		var blobClient = _blobContainerClient.GetBlobClient(blobName);
		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(_blobContent));

		_ = await blobClient.UploadAsync(stream, false);
		_ = await blobClient.SetMetadataAsync(_blobMetadata);

		// Act
		var result = await _impl.ListBlobsAsync(_containerName, namePrefix, false).ToListAsync();

		// Assert
		_ = Assert.Single(result);
		Assert.Equal(new[] { _blobName }, result);
	}

	[Fact]
	public async Task ListBlobsAsync_ShouldSucceed_WhenRecursiveIsTrue()
	{
		// Arrange
		var namePrefix = _blobName.Split("/")[0];
		var blobName = "tests/subdir/another-text-file.txt";
		var blobClient = _blobContainerClient.GetBlobClient(blobName);
		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(_blobContent));

		_ = await blobClient.UploadAsync(stream, false);
		_ = await blobClient.SetMetadataAsync(_blobMetadata);

		var files = new List<string> { _blobName, blobName };

		// Act
		var result = await _impl.ListBlobsAsync(_containerName, namePrefix, true).ToListAsync();

		// Assert
		Assert.Equal(files.Count, result.Count);
		Assert.Equivalent(files, result, true);
	}

	[Fact]
	public async Task ListBlobsAsync_ShouldSucceed_WhenDirectoryIsEmpty()
	{
		// Arrange
		var namePrefix = _blobName.Split("/")[0];
		_ = await _blobContainerClient.GetBlobClient(_blobName).DeleteAsync();

		// Act
		var result = await _impl.ListBlobsAsync(_containerName, namePrefix, false).ToListAsync();

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public async Task ListBlobsAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExist()
	{
		// Arrange
		var namePrefix = _blobName.Split("/")[0];
		var containerName = NON_EXISTENT_CONTAINER;

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(async () => await _impl.ListBlobsAsync(containerName, namePrefix, false).ToArrayAsync());
	}

	#endregion // ListBlobsAsync

	#region UpdateBlobMetadataAsync

	[Fact]
	public async Task UpdateBlobMetadataAsync_ShouldSucceed_WhenBlobExists()
	{
		// Arrange
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act
		await _impl.UpdateBlobMetadataAsync(_containerName, _blobName, metadata);

		// Assert
		var actual = await _blobClient.GetPropertiesAsync();
		Assert.Equal(metadata, actual.Value.Metadata);
	}

	[Fact]
	public async Task UpdateBlobMetadataAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExist()
	{
		// Arrange
		var containerName = NON_EXISTENT_CONTAINER;
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(() => _impl.UpdateBlobMetadataAsync(containerName, _blobName, metadata));
	}

	[Fact]
	public async Task UpdateBlobMetadataAsync_ShouldThrowBlobNotFoundException_WhenBlobDoesNotExist()
	{
		// Arrange
		var blobName = NON_EXISTENT_BLOB;
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act & Assert
		_ = await Assert.ThrowsAsync<BlobNotFoundException>(() => _impl.UpdateBlobMetadataAsync(_containerName, blobName, metadata));
	}

	#endregion // UpdateBlobMetadataAsync

	#region UploadBlobAsync

	[Fact]
	public async Task UploadBlobAsync_ShouldSucceed_WhenOverwriteIsFalseAndBlobDoesNotExist()
	{
		// Arrange
		var blobName = _blobName.Replace("text-file", "new-text-file");
		var data = new byte[] { 1, 2, 3 };
		var contentType = "application/octet-stream";
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act
		await _impl.UploadBlobAsync(_containerName, blobName, data, contentType, false, metadata);

		// Assert
		Assert.True(await _blobContainerClient.GetBlobClient(blobName).ExistsAsync());
		Assert.Equal(contentType, (await _blobContainerClient.GetBlobClient(blobName).GetPropertiesAsync()).Value.ContentType);
		Assert.Equivalent(data, (await _blobContainerClient.GetBlobClient(blobName).DownloadContentAsync()).Value.Content.ToArray());
	}

	[Fact]
	public async Task UploadBlobAsync_ShouldThrowBlobAlreadyExistsException_WhenOverwriteIsFalseAndBlobExists()
	{
		// Arrange
		var data = new byte[] { 1, 2, 3 };
		var contentType = "application/octet-stream";
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act & Assert
		_ = await Assert.ThrowsAsync<BlobAlreadyExistsException>(() => _impl.UploadBlobAsync(_containerName, _blobName, data, contentType, false, metadata));
	}

	[Fact]
	public async Task UploadBlobAsync_ShouldSucceed_WhenOverwriteIsTrueAndBlobDoesNotExist()
	{
		// Arrange
		var blobName = _blobName.Replace("text-file", "new-text-file");
		var data = new byte[] { 1, 2, 3 };
		var contentType = "application/octet-stream";
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act
		await _impl.UploadBlobAsync(_containerName, blobName, data, contentType, true, metadata);

		// Assert
		Assert.True(await _blobContainerClient.GetBlobClient(blobName).ExistsAsync());
		Assert.Equal(contentType, (await _blobContainerClient.GetBlobClient(blobName).GetPropertiesAsync()).Value.ContentType);
		Assert.Equivalent(data, (await _blobContainerClient.GetBlobClient(blobName).DownloadContentAsync()).Value.Content.ToArray());
	}

	[Fact]
	public async Task UploadBlobAsync_ShouldSucceed_WhenOverwriteIsTrueAndBlobExists()
	{
		// Arrange
		var data = new byte[] { 1, 2, 3 };
		var contentType = "application/octet-stream";
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act
		await _impl.UploadBlobAsync(_containerName, _blobName, data, contentType, true, metadata);

		// Assert
		Assert.True(await _blobContainerClient.GetBlobClient(_blobName).ExistsAsync());
		Assert.Equal(contentType, (await _blobContainerClient.GetBlobClient(_blobName).GetPropertiesAsync()).Value.ContentType);
		Assert.Equivalent(data, (await _blobContainerClient.GetBlobClient(_blobName).DownloadContentAsync()).Value.Content.ToArray());
	}

	[Fact]
	public async Task UploadBlobAsync_ShouldThrowContainerNotFoundException_WhenContainerDoesNotExist()
	{
		// Arrange
		var containerName = NON_EXISTENT_CONTAINER;
		var data = new byte[] { 1, 2, 3 };
		var contentType = "application/octet-stream";
		var metadata = new Dictionary<string, string> { { "key", "value" } };

		// Act & Assert
		_ = await Assert.ThrowsAsync<ContainerNotFoundException>(() => _impl.UploadBlobAsync(containerName, _blobName, data, contentType, false, metadata));
	}

	#endregion // UploadBlobAsync
}
