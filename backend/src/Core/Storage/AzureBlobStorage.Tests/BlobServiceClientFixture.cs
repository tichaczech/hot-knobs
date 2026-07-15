using Azure.Storage.Blobs;

using Fand.Runtime.Tests;

using Microsoft.Extensions.Configuration;

namespace Fand.Runtime.Storage.AzureBlobStorage;

public sealed class BlobServiceClientFixture : BaseFixture
{
	public BlobServiceClient BlobServiceClient { get; private set; }

	public BlobServiceClientFixture() => BlobServiceClient = new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage"), new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_12_02));
}
