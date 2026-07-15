namespace Fand.Runtime.Storage;

/// <summary>
/// Defines a contract for blob storage that can perform operations such as uploading, downloading, deleting, and listing blobs in a specified storage.
/// </summary>
public interface IBlobStorage
{
	/// <summary>
	/// Deletes a blob from the specified container.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="blobName">The name of the blob to delete.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	/// <exception cref="ArgumentNullException">When <paramref name="containerName"/> or <paramref name="blobName"/> is null or empty.</exception>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	/// <exception cref="BlobNotFoundException">When blob does not exist.</exception>
	Task DeleteBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Reads the contents of a blob from the specified container.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="blobName">The name of the blob within the container.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A byte array containing the contents of the blob.</returns>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	/// <exception cref="BlobNotFoundException">When blob does not exist.</exception>
	Task<byte[]> DownloadBlobAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the metadata associated with the specified blob in the given container.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="blobName">The name of the blob.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A dictionary containing the metadata associated with the blob.</returns>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	/// <exception cref="BlobNotFoundException">When blob does not exist.</exception>
	Task<IDictionary<string, string>> GetBlobMetadataAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of blob names in the specified directory of the specified container.
	/// </summary>
	/// <param name="containerName">The name of the container to retrieve the blobs from.</param>
	/// <param name="namePrefix">The prefix of the blob names to retrieve. Only blobs with names that start with this prefix will be included.</param>
	/// <param name="recursive">Whether to retrieve blobs recursively from subdirectories.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>An enumerable collection of blob names in the specified directory.</returns>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	IAsyncEnumerable<string> ListBlobsAsync(string containerName, string? namePrefix, bool recursive = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates the metadata of a blob in the specified container.
	/// </summary>
	/// <param name="containerName">The name of the container containing the blob.</param>
	/// <param name="blobName">The name of the blob to update.</param>
	/// <param name="metadata">The new metadata to set for the blob.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	/// <exception cref="BlobNotFoundException">When blob does not exist.</exception>
	Task UpdateBlobMetadataAsync(string containerName, string blobName, IDictionary<string, string> metadata, CancellationToken cancellationToken = default);

	/// <summary>
	/// Writes the specified blob data to the given blob in the specified container.
	/// </summary>
	/// <param name="containerName">The name of the container to write the blob to.</param>
	/// <param name="blobName">The name of the blob to write the data to.</param>
	/// <param name="data">The data of the blob.</param>
	/// <param name="contentType">The content type of the blob.</param>
	/// <param name="overwrite">Whether to overwrite the blob if it already exists.</param>
	/// <param name="metadata">Optional metadata to associate with the blob.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="ContainerNotFoundException">When container does not exist.</exception>
	/// <exception cref="BlobAlreadyExistsException">When blob already exists.</exception>
	Task UploadBlobAsync(string containerName, string blobName, byte[] data, string contentType, bool overwrite = false, IDictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);
}
