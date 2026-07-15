using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

using Azure.Storage.Queues;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public abstract class AzureQueueStorageQueueClient
{
	private readonly ILogger _logger;

	private readonly QueueServiceClient _client;

	private readonly ConcurrentDictionary<string, QueueClient> _queueClients = new();

	protected private AzureQueueStorageQueueClient(ILogger<AzureQueueStorageQueueClient> logger, QueueServiceClient client)
	{
		_logger = logger;
		_client = client;
	}

	/// <summary>
	/// Gets a QueueClient for the specified queue name. If the queue does not exist and throwIfNotExists is true, a QueueNotFoundException is thrown.
	/// </summary>
	/// <param name="queueName">The name of the queue.</param>
	/// <param name="throwIfNotExists">Whether to throw an exception if the queue does not exist.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>The QueueClient for the specified queue.</returns>
	/// <exception cref="QueueNotFoundException"></exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected async Task<QueueClient> GetQueueClientAsync(string queueName, bool throwIfNotExists = true, CancellationToken cancellationToken = default)
	{
		_logger.LogTrace("Getting queue client for: {QueueName}", queueName);

		if (_queueClients.TryGetValue(queueName, out var queueClient))
			return queueClient;

		queueClient = _client.GetQueueClient(queueName);
		var exists = await queueClient.ExistsAsync(cancellationToken).ConfigureAwait(false);
		if (!exists && throwIfNotExists)
			throw new QueueNotFoundException($"Queue '{queueName}' does not exist.");

		return _queueClients.GetOrAdd(queueName, queueClient);
	}
}
