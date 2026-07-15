using Azure.Storage.Queues;

using Fand.Runtime.Tests;

using Microsoft.Extensions.Configuration;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public sealed class QueueServiceClientFixture : BaseFixture
{
	public QueueServiceClient QueueServiceClient { get; private set; }

	public QueueServiceClientFixture() => QueueServiceClient = new QueueServiceClient(Configuration.GetConnectionString("AzureQueueStorage"), new QueueClientOptions(QueueClientOptions.ServiceVersion.V2022_11_02));
}
