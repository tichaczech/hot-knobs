using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

using CommunityToolkit.Diagnostics;

using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

/// <summary>
/// Provides an implementation of <see cref="IBlobStorage"/> using Google Cloud Storage.
/// </summary>
public class GoogleCloudStorageImpl : IBlobStorage
{
	private const char DELIMITER = '/';

	private const string DELIMITER_STRING = "/";

	private readonly StorageClient _storageClient;

	private readonly ILogger _logger;

	private readonly ConcurrentDictionary<string, Bucket?> _buckets = new();

	/// <summary>
	/// Initializes a new instance of the <see cref="GoogleCloudStorageImpl"/> class.
	/// </summary>
	/// <param name="logger">The ILogger instance.</param>
	/// <param name="client">The <see cref="StorageClient"/> instance to interact with Google Cloud Storage service.</param>
	public GoogleCloudStorageImpl(ILogger<GoogleCloudStorageImpl> logger, StorageClient client)
	{
		_logger = logger;
		_storageClient = client;
	}

	public async Task DeleteBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Deleting blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var obj = await GetObjectAsync(containerName, blobName, true, cancellationToken: cancellationToken).ConfigureAwait(false);
		await _storageClient.DeleteObjectAsync(obj, cancellationToken: cancellationToken).ConfigureAwait(false);
	}

	public async Task<byte[]> DownloadBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Downloading blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		using var dest = new MemoryStream();

		var obj = await GetObjectAsync(containerName, blobName, true, cancellationToken: cancellationToken).ConfigureAwait(false);
		_ = await _storageClient.DownloadObjectAsync(obj, dest, cancellationToken: cancellationToken).ConfigureAwait(false);

		return dest.ToArray();
	}

	public async Task<IDictionary<string, string>> GetBlobMetadataAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Getting metadata for blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var obj = await GetObjectAsync(containerName, blobName, true, cancellationToken: cancellationToken).ConfigureAwait(false);
		var metadata = obj!.Metadata ?? new Dictionary<string, string>();

		return metadata;
	}

	public async IAsyncEnumerable<string> ListBlobsAsync(string containerName, string? namePrefix, bool recursive = false, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);

		_logger.LogDebug("Listing blobs with prefix: {NamePrefix} in container: {ContainerName}...", namePrefix ?? "(none)", containerName);
		_ = await GetBucketAsync(containerName, true, cancellationToken).ConfigureAwait(false);

		var prefix = String.IsNullOrEmpty(namePrefix) ? null : namePrefix!.TrimEnd(DELIMITER) + DELIMITER; // Ensure prefix ends with delimiter if provided
		var options = new ListObjectsOptions
		{
			Delimiter = recursive ? null : DELIMITER_STRING, // For details how delimiter works see https://docs.cloud.google.com/storage/docs/listing-objects#list-objects
			Fields = "items(name),nextPageToken" // Limit the fields returned for performance
		};

		await foreach (var obj in _storageClient.ListObjectsAsync(containerName, prefix, options).ConfigureAwait(false))
		{
			// Remove "directories" from the listing
			if (obj.Name.EndsWith(DELIMITER_STRING, StringComparison.InvariantCulture)) // Overload with Char parameter is not available in .NET Standard 2.1
				continue;

			yield return obj.Name;
		}
	}

	public async Task UpdateBlobMetadataAsync(string containerName, string blobName, IDictionary<string, string> metadata, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);
		Guard.IsNotNull(metadata);

		_logger.LogDebug("Updating metadata for blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var obj = await GetObjectAsync(containerName, blobName, true, cancellationToken: cancellationToken).ConfigureAwait(false);
		obj!.Metadata = new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase);

		_ = await _storageClient.UpdateObjectAsync(obj, cancellationToken: cancellationToken).ConfigureAwait(false);
	}

	public async Task UploadBlobAsync(string containerName, string blobName, byte[] data, string contentType, bool overwrite = false, IDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);
		Guard.IsNotNull(data);

		_logger.LogDebug("Uploading blob: {BlobName} to container: {ContainerName}...", blobName, containerName);

		// NOTE: Explicit existence check via GetObjectAsync is performed here because the Google Cloud Storage SDK does not reliably support preconditions for existence checks (see https://docs.cloud.google.com/storage/docs/metadata#generation-number).
		var obj = await GetObjectAsync(containerName, blobName, false, cancellationToken: cancellationToken).ConfigureAwait(false);
		if (obj != null && !overwrite)
			throw new BlobAlreadyExistsException($"Blob '{blobName}' already exists.");

		using var stream = new MemoryStream(data);
		obj = await _storageClient.UploadObjectAsync(containerName, blobName, contentType, stream, cancellationToken: cancellationToken).ConfigureAwait(false);

		if (metadata != null)
		{
			obj!.Metadata = new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase);
			_ = await _storageClient.UpdateObjectAsync(obj, cancellationToken: cancellationToken).ConfigureAwait(false);
		}
	}

	// NOTE: Due to different way of bucket/object existence checks and slow response from Google Cloud Storage (comparing to e.g. Azure SDK), both GetBucket and GetObject methods are implemented asynchronously.

	/// <summary>
	/// Asynchronously gets the <see cref="Bucket"/> for the specified bucket name, caching the result.
	/// </summary>
	/// <param name="bucketName">The name of the bucket.</param>
	/// <param name="throwIfNotExists">Whether to throw if the bucket does not exist.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The <see cref="Bucket"/> instance if found; otherwise <see langword="null"/> when <paramref name="throwIfNotExists"/> is <c>false</c>.</returns>
	/// <exception cref="ContainerNotFoundException">Thrown when the bucket does not exist and <paramref name="throwIfNotExists"/> is <c>true</c>.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private async Task<Bucket?> GetBucketAsync(string bucketName, bool throwIfNotExists = true, CancellationToken cancellationToken = default)
	{
		void checkBucket(Bucket? bckt)
		{
			if (bckt == null && throwIfNotExists)
				throw new ContainerNotFoundException($"Container '{bucketName}' does not exist.");
		}

		// Fast path: return cached bucket if available.
		if (_buckets.TryGetValue(bucketName, out var cached))
		{
			checkBucket(cached);
			return cached;
		}

		var bucket = await _storageClient.GetBucketExAsync(bucketName, cancellationToken: cancellationToken).ConfigureAwait(false);
		bucket = _buckets.GetOrAdd(bucketName, bucket);
		checkBucket(bucket);

		return bucket;
	}

	/// <summary>
	/// Asynchronously gets the <see cref="Google.Apis.Storage.v1.Data.Object"/> for the specified bucket and object name.
	/// </summary>
	/// <param name="bucketName">The name of the bucket containing the object.</param>
	/// <param name="objectName">The name of the object to retrieve.</param>
	/// <param name="throwIfNotExists">Whether to throw an exception if the object does not exist.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The <see cref="Google.Apis.Storage.v1.Data.Object"/> instance if found; otherwise <see langword="null"/> when <paramref name="throwIfNotExists"/> is <c>false</c>.</returns>
	/// <exception cref="BlobNotFoundException">Thrown when the object does not exist and <paramref name="throwIfNotExists"/> is <c>true</c>.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private async Task<Google.Apis.Storage.v1.Data.Object?> GetObjectAsync(string bucketName, string objectName, bool throwIfNotExists = true, CancellationToken cancellationToken = default)
	{
		_ = await GetBucketAsync(bucketName, true, cancellationToken).ConfigureAwait(false);

		var obj = await _storageClient.GetObjectExAsync(bucketName, objectName, cancellationToken: cancellationToken).ConfigureAwait(false);
		if (obj == null && throwIfNotExists)
			throw new BlobNotFoundException($"Blob '{objectName}' does not exist.");

		return obj;
	}
}
