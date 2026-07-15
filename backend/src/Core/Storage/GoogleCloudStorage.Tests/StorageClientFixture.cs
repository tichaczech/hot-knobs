using Fand.Runtime.Tests;

using Google.Cloud.Storage.V1;

using Microsoft.Extensions.Configuration;

namespace Fand.Runtime.Storage.GoogleCloudStorage;

public sealed class StorageClientFixture : BaseFixture
{
	public StorageClient StorageClient { get; private set; }

	public string ProjectId { get; private set; }

	public StorageClientFixture()
	{
		Environment.SetEnvironmentVariable("STORAGE_EMULATOR_HOST", Configuration.GetConnectionString("GoogleCloudStorage"));
		ProjectId = "fand-runtime-dev";

		var builder = new StorageClientBuilder
		{
			EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOnly
		};
		StorageClient = builder.Build();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			StorageClient?.Dispose();
			StorageClient = null!;
		}

		base.Dispose(disposing);
	}
}
