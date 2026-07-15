using Azure.Storage.Queues;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public class AzureQueueStorageProducer : AzureQueueStorageQueueClient, IQueueProducer
{
	private readonly ILogger _logger;

	private readonly AzureQueueStorageProducerOptions _options;

	public AzureQueueStorageProducer(ILogger<AzureQueueStorageProducer> logger, QueueServiceClient client, IOptions<AzureQueueStorageProducerOptions> optionsAccessor) : base(logger, client)
	{
		Guard.IsNotNull(optionsAccessor, nameof(optionsAccessor));

		_logger = logger;
		_options = optionsAccessor.Value;
	}

	public async Task<ISendReceipt> SendMessageAsync<TMessage>(string queue, TMessage message, CancellationToken cancellationToken = default)
		where TMessage : notnull
	{
		using (_logger.BeginScope(new Dictionary<string, object> { { "Queue", queue } }))
		{
			_logger.LogInformation("Sending message to the queue.");

			string serializedMessage;
			try
			{
				var serializer = _options.SerializerFactory.Create<TMessage>();
				serializedMessage = serializer.SerializeMessage(message);
			}
			catch (Exception ex)
			{
				throw new MessageSerializationException("Failed to serialize message.", ex);
			}

			var queueClient = await GetQueueClientAsync(queue, true, cancellationToken).ConfigureAwait(false);
			var receipt = await queueClient.SendMessageAsync(serializedMessage, cancellationToken).ConfigureAwait(false);

			_logger.LogDebug("Message queued, send receipt id: {MessageId}.", receipt.Value.MessageId);

			return new SendReceipt(receipt.Value.MessageId, receipt.Value.InsertionTime);
		}
	}
}
