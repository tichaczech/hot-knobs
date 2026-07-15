using System.Text;

using Google;
using Google.Cloud.Storage.V1;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Xunit;
using Xunit.Abstractions;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

public sealed class GoogleCloudStorageIntegrationTests : IClassFixture<StorageClientFixture>, IDisposable
{
	private const string NON_EXISTENT_CONTAINER = "non-existent-container";

	private const string NON_EXISTENT_BLOB = "non-existent-file.txt";

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

	private readonly StorageClient _storageClient;

	public GoogleCloudStorageIntegrationTests(StorageClientFixture fixture, ITestOutputHelper outputHelper)
	{
		_storageClient = fixture.StorageClient;

		outputHelper.WriteLine("Using Google Cloud Storage Emulator at: " + Environment.GetEnvironmentVariable("STORAGE_EMULATOR_HOST"));

		var bucket = _storageClient.GetBucketEx(_containerName);
		if (bucket != null)
			_storageClient.DeleteBucket(_containerName);
		_ = _storageClient.CreateBucket(fixture.ProjectId, _containerName);

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(_blobContent));
		var obj = _storageClient.UploadObject(_containerName, _blobName, _contentType, stream);

		obj.Metadata = (obj.Metadata ?? new Dictionary<string, string>()).Union(_blobMetadata).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		_ = _storageClient.UpdateObject(obj);

		var logger = NullLoggerFactory.Instance.CreateLogger<GoogleCloudStorageImpl>();
		_impl = new GoogleCloudStorageImpl(logger, fixture.StorageClient);
	}

	public void Dispose()
	{
		var bucket = _storageClient.GetBucketEx(_containerName);
		if (bucket != null)
			_storageClient.DeleteBucket(_containerName, new DeleteBucketOptions { DeleteObjects = true });
	}

	#region DeleteBlobAsync

	[Fact]
	public async Task DeleteBlobAsync_ShouldSucceed_WhenBlobExists()
	{
		// Arrange

		// Act
		await _impl.DeleteBlobAsync(_containerName, _blobName);

		// Assert
		var ex = await Record.ExceptionAsync(async () => await _storageClient.GetObjectAsync(_containerName, _blobName));
		var gaex = Assert.IsType<GoogleApiException>(ex);
		Assert.True(404 == gaex.Error.Code || "Not Found" == gaex.Error.ErrorResponseContent.TrimEnd());
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

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(_blobContent));
		_ = await _storageClient.UploadObjectAsync(_containerName, blobName, _contentType, stream);

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

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(_blobContent));
		_ = await _storageClient.UploadObjectAsync(_containerName, blobName, _contentType, stream);

		var blobs = new List<string> { _blobName, blobName };

		// Act
		var result = await _impl.ListBlobsAsync(_containerName, namePrefix, true).ToListAsync();

		// Assert
		Assert.Equal(blobs.Count, result.Count);
		Assert.Equivalent(blobs, result, true);
	}

	[Fact]
	public async Task ListBlobsAsync_ShouldSucceed_WhenDirectoryIsEmpty()
	{
		// Arrange
		var namePrefix = _blobName.Split("/")[0];
		await _storageClient.DeleteObjectAsync(_containerName, _blobName);

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
		var actual = await _storageClient.GetObjectAsync(_containerName, _blobName);
		Assert.Equal(metadata, actual.Metadata);
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
		Google.Apis.Storage.v1.Data.Object? actual = null;
		using var stream = new MemoryStream();

		var ex = await Record.ExceptionAsync(async () => actual = await _storageClient.DownloadObjectAsync(_containerName, blobName, stream));
		Assert.Null(ex);
		Assert.Equal(contentType, actual!.ContentType);
		Assert.Equivalent(data, stream.ToArray());
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
		Google.Apis.Storage.v1.Data.Object? actual = null;
		using var stream = new MemoryStream();

		var ex = await Record.ExceptionAsync(async () => actual = await _storageClient.DownloadObjectAsync(_containerName, blobName, stream));
		Assert.Null(ex);
		Assert.Equal(contentType, actual!.ContentType);
		Assert.Equivalent(data, stream.ToArray());
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
		Google.Apis.Storage.v1.Data.Object? actual = null;
		using var stream = new MemoryStream();

		var ex = await Record.ExceptionAsync(async () => actual = await _storageClient.DownloadObjectAsync(_containerName, _blobName, stream));
		Assert.Null(ex);
		Assert.Equal(contentType, actual!.ContentType);
		Assert.Equivalent(data, stream.ToArray());
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
