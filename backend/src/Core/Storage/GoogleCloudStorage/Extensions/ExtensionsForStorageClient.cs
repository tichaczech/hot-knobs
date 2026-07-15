using CommunityToolkit.Diagnostics;

using Google;
using Google.Cloud.Storage.V1;

using GASD = Google.Apis.Storage.v1.Data;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

public static class ExtensionsForStorageClient
{
	/// <summary>
	/// Fetches the information about a bucket synchronously.
	/// </summary>
	/// <param name="client"></param>
	/// <param name="bucketName">The name of the bucket. Must not be null.</param>
	/// <param name="options">Additional options for the fetch operation. May be null, in which case appropriate
	/// defaults will be used.</param>
	/// <returns>The <see cref="GASD.Bucket"/> representation of the storage bucket if found; <see langword="null"/> otherwise.</returns>
	public static GASD.Bucket? GetBucketEx(this StorageClient client, string bucketName, GetBucketOptions? options = default)
	{
		Guard.IsNotNull(client, nameof(client));

		try
		{
			// There is no direct method to check if a bucket exists, so we try to get the bucket itself.
			return client.GetBucket(bucketName, options);
		}
		catch (GoogleApiException ex) when (ex.Error.Code == 404 || ex.Error.ErrorResponseContent.TrimEnd() == "Not Found") // Emulator may return "Not Found" text only
		{
			return null;
		}
	}

	/// <summary>
	/// Fetches the information about a bucket asynchronously.
	/// </summary>
	/// <param name="client"></param>
	/// <param name="bucketName">The name of the bucket. Must not be null.</param>
	/// <param name="options">Additional options for the fetch operation. May be null, in which case appropriate defaults will be used.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation, with a result returning the <see cref="GASD.Bucket"/> representation of the storage bucket if found; <see langword="null"/> otherwise.</returns>
	public static async Task<GASD.Bucket?> GetBucketExAsync(this StorageClient client, string bucketName, GetBucketOptions? options = default, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(client, nameof(client));

		try
		{
			// There is no direct method to check if a bucket exists, so we try to get the bucket itself.
			return await client.GetBucketAsync(bucketName, options, cancellationToken).ConfigureAwait(false);
		}
		catch (GoogleApiException ex) when (ex.Error.Code == 404)
		{
			return null;
		}
	}

	/// <summary>
	/// Fetches the information about an object synchronously.
	/// </summary>
	/// <remarks>This does not retrieve the content of the object itself. Use <see cref="StorageClient.DownloadObject(String, String, Stream, DownloadObjectOptions, IProgress{Google.Apis.Download.IDownloadProgress})"/> to download the content.</remarks>
	/// <param name="client"></param>
	/// <param name="bucketName">The name of the bucket containing the object. Must not be null.</param>
	/// <param name="objectName">The name of the object within the bucket. Must not be null.</param>
	/// <param name="options">Additional options for the fetch operation. May be null, in which case appropriate defaults will be used.</param>
	/// <returns>The <see cref="GASD.Object"/> representation of the storage object if found; <see langword="null"/> otherwise.</returns>
	public static GASD.Object? GetObjectEx(this StorageClient client, string bucketName, string objectName, GetObjectOptions? options = null)
	{
		Guard.IsNotNull(client, nameof(client));

		try
		{
			// There is no direct method to check if an object exists, so we try to get the object itself.
			return client.GetObject(bucketName, objectName, options);
		}
		catch (GoogleApiException ex) when (ex.Error.Code == 404 || ex.Error.ErrorResponseContent.TrimEnd() == "Not Found") // Emulator may return "Not Found" text only
		{
			return null;
		}
	}

	/// <summary>
	/// Fetches the information about an object asynchronously.
	/// </summary>
	/// <remarks>This does not retrieve the content of the object itself. Use <see cref="StorageClient.DownloadObjectAsync(String, String, Stream, DownloadObjectOptions, CancellationToken, IProgress{Google.Apis.Download.IDownloadProgress})"/> to download the content.</remarks>
	/// <param name="client"></param>
	/// <param name="bucketName">The name of the bucket containing the object. Must not be null.</param>
	/// <param name="objectName">The name of the object within the bucket. Must not be null.</param>
	/// <param name="options">Additional options for the fetch operation. May be null, in which case appropriate defaults will be used.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous operation, with a result returning the <see cref="GASD.Object"/> representation of the storage object if found; <see langword="null"/> otherwise.</returns>
	public static async Task<GASD.Object?> GetObjectExAsync(this StorageClient client, string bucketName, string objectName, GetObjectOptions? options = null, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(client, nameof(client));

		try
		{
			// There is no direct method to check if an object exists, so we try to get the object itself.
			return await client.GetObjectAsync(bucketName, objectName, options, cancellationToken).ConfigureAwait(false);
		}
		catch (GoogleApiException ex) when (ex.Error.Code == 404)
		{
			return null;
		}
	}
}
