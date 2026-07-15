using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Storage.AzureBlobStorage;

/// <summary>
/// Provides an implementation of <see cref="IBlobStorage"/> using Azure Blob Storage.
/// </summary>
public class AzureBlobStorageImpl : IBlobStorage
{
	private const char DELIMITER = '/';

	private readonly BlobServiceClient _client;

	private readonly ConcurrentDictionary<string, BlobContainerClient> _containerClients = new();

	private readonly ILogger _logger;

	// [LoggerMessage(Level = LogLevel.Debug, Message = "Processing {Data} items")]
	// private partial void LogProcessingData(int[] data);

	// [LoggerMessage(Level = LogLevel.Trace, Message = "Data: Count={Count}, Items={Items}")]
	// private partial void LogTraceData(int count, int[] items);

	/// <summary>
	/// Initializes a new instance of the <see cref="AzureBlobStorageImpl"/> class.
	/// </summary>
	/// <param name="logger">The ILogger instance.</param>
	/// <param name="client">The <see cref="BlobServiceClient"/> used to interact with the Azure Blob Storage service.</param>
	public AzureBlobStorageImpl(ILogger<AzureBlobStorageImpl> logger, BlobServiceClient client)
	{
		_logger = logger;
		_client = client;
	}

	/// <inheritdoc />
	public async Task DeleteBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Deleting blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var blobClient = GetBlobClient(containerName, blobName);

		_ = await blobClient.DeleteAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<byte[]> DownloadBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Downloading blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var blobClient = GetBlobClient(containerName, blobName);
		var blobDownloadInfo = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return blobDownloadInfo.Value.Content.ToArray();
	}

	/// <inheritdoc />
	public async Task<IDictionary<string, string>> GetBlobMetadataAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);

		_logger.LogDebug("Getting metadata for blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var blobClient = GetBlobClient(containerName, blobName);
		var blobProperties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

		return blobProperties.Value.Metadata;
	}

	/// <inheritdoc />
	public async IAsyncEnumerable<string> ListBlobsAsync(string containerName, string? namePrefix, bool recursive = false, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);

		_logger.LogDebug("Listing blobs with prefix: {NamePrefix} in container: {ContainerName}...", namePrefix, containerName);

		var containerClient = GetContainerClient(containerName);

		var prefix = String.IsNullOrEmpty(namePrefix) ? null : namePrefix!.TrimEnd(DELIMITER) + DELIMITER; // Ensure prefix ends with delimiter if provided
		await foreach (var blobItem in containerClient.GetBlobsAsync(BlobTraits.None, BlobStates.None, prefix, cancellationToken).ConfigureAwait(false))
		{
			// Skip items from "subdirectories" when not recursive
			if (!recursive && blobItem.Name[(prefix?.Length ?? 0)..].Contains(DELIMITER, StringComparison.InvariantCulture))
				continue;

			yield return blobItem.Name;
		}
	}

	/// <inheritdoc />
	public async Task UpdateBlobMetadataAsync(string containerName, string blobName, IDictionary<string, string> metadata, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);
		Guard.IsNotNull(metadata);

		_logger.LogDebug("Updating metadata for blob: {BlobName} in container: {ContainerName}...", blobName, containerName);

		var blobClient = GetBlobClient(containerName, blobName);

		var blobMetadata = new Dictionary<string, string>(metadata, StringComparer.OrdinalIgnoreCase);
		_ = await blobClient.SetMetadataAsync(blobMetadata, cancellationToken: cancellationToken).ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task UploadBlobAsync(string containerName, string blobName, byte[] data, string contentType, bool overwrite = false, IDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNullOrEmpty(containerName);
		Guard.IsNotNullOrEmpty(blobName);
		Guard.IsNotNull(data);

		_logger.LogDebug("Uploading blob: {BlobName} to container: {ContainerName}...", blobName, containerName);

		var blobClient = GetBlobClient(containerName, blobName, false);

		var blobHttpHeaders = new BlobHttpHeaders { ContentType = contentType };
		var conditions = new BlobRequestConditions { IfNoneMatch = new ETag("*") };
		if (overwrite)
			conditions = new BlobRequestConditions { IfMatch = new ETag("*") };
		var options = new BlobUploadOptions
		{
			Conditions = conditions,
			HttpHeaders = blobHttpHeaders,
			Metadata = metadata
		};

		try
		{
			_ = await blobClient.UploadAsync(BinaryData.FromBytes(data), options, cancellationToken: cancellationToken).ConfigureAwait(false);
		}
		catch (RequestFailedException ex)
		{
			if (ex.ErrorCode == "BlobAlreadyExists")
				throw new BlobAlreadyExistsException($"Blob '{blobName}' already exists.", ex);

			throw;
		}
	}

	/// <summary>
	/// Gets the <see cref="BlobContainerClient"/> for the specified container name.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="throwIfNotExists">Whether to throw an exception if the container does not exist.</param>
	/// <returns>The <see cref="BlobContainerClient"/> instance if found; otherwise <see langword="null"/> when <paramref name="throwIfNotExists"/> is <c>false</c>.</returns>
	/// <exception cref="ContainerNotFoundException">Thrown when the container does not exist and <paramref name="throwIfNotExists"/> is <c>true</c>.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private BlobContainerClient GetContainerClient(string containerName, bool throwIfNotExists = true)
	{
		return _containerClients.GetOrAdd(containerName, (key) =>
		{
			var containerClient = _client.GetBlobContainerClient(key);
			if (!containerClient.Exists() && throwIfNotExists)
				throw new ContainerNotFoundException($"Container '{key}' does not exist.");

			return containerClient;
		});
	}

	/// <summary>
	/// Gets the <see cref="BlobClient"/> for the specified container and blob name.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="blobName">The name of the blob to retrieve.</param>
	/// <param name="throwIfNotExists">Whether to throw an exception if the blob does not exist.</param>
	/// <returns>The <see cref="BlobClient"/> instance if found; otherwise <see langword="null"/> when <paramref name="throwIfNotExists"/> is <c>false</c>.</returns>
	/// <exception cref="BlobNotFoundException">Thrown when the blob does not exist and <paramref name="throwIfNotExists"/> is <c>true</c>.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private BlobClient GetBlobClient(string containerName, string blobName, bool throwIfNotExists = true)
	{
		var blobClient = GetContainerClient(containerName).GetBlobClient(blobName);
		if (!blobClient.Exists() && throwIfNotExists)
			throw new BlobNotFoundException($"Blob '{blobName}' does not exist.");

		return blobClient;
	}
}
