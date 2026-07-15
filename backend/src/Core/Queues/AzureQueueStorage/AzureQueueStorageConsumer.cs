using Azure;
using Azure.Storage.Queues;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fand.Runtime.Queues.AzureQueueStorage;

public partial class AzureQueueStorageConsumer : AzureQueueStorageQueueClient, IQueueConsumer
{
	private readonly ILogger _logger;

	private readonly AzureQueueStorageConsumerOptions _options;

	public AzureQueueStorageConsumer(ILogger<AzureQueueStorageConsumer> logger, QueueServiceClient client, IOptions<AzureQueueStorageConsumerOptions> optionsAccessor) : base(logger, client)
	{
		Guard.IsNotNull(optionsAccessor, nameof(optionsAccessor));

		_logger = logger;
		_options = optionsAccessor.Value;
	}

	/// <inheritdoc />
	public async Task ConfirmReceiveAsync<TMessage>(string queue, IReceiveResult<TMessage> message, CancellationToken cancellationToken = default)
		where TMessage : notnull
	{
		Guard.IsNotNullOrWhiteSpace(queue, nameof(queue));
		Guard.IsNotNull(message, nameof(message));

		using (_logger.BeginScope(new Dictionary<string, object> { ["Queue"] = queue }))
		{
			_logger.LogInformation("Confirming message receipt.");

			// Check if the queue exists first
			var queueClient = await GetQueueClientAsync(queue, true, cancellationToken).ConfigureAwait(false);

			if (message is not AzureQueueStorageDequeuedMessage<TMessage> queueMessage)
				throw new ArgumentException($"Invalid message type when confirming receive. Expected {nameof(AzureQueueStorageDequeuedMessage<TMessage>)}, got {message.GetType().Name}.");

			using (_logger.BeginScope(new Dictionary<string, object> { ["MessageId"] = queueMessage.QueueMessage.MessageId }))
			{

				_logger.LogTrace("Confirming message receipt.");
				try
				{
					_ = await queueClient.DeleteMessageAsync(queueMessage.QueueMessage.MessageId, queueMessage.QueueMessage.PopReceipt, cancellationToken).ConfigureAwait(false);
					_logger.LogDebug("Message receipt confirmed.");
				}
				catch (RequestFailedException ex) when (ex.ErrorCode == "PopReceiptMismatch" && ex.Source == "Azure.Storage.Queues")
				{
					throw new MessageConfirmationFailedException($"Message with id: {queueMessage.QueueMessage.MessageId} has already been deleted or the pop receipt is invalid (PopReceiptMismatch).", ex);
				}
			}
		}
	}

	/// <inheritdoc />
	public async Task<IReceiveResult<TMessage>?> ReceiveMessageAsync<TMessage>(string queue, CancellationToken cancellationToken = default)
		where TMessage : notnull
	{
		using (_logger.BeginScope(new Dictionary<string, object> { ["Queue"] = queue }))
		{
			_logger.LogInformation("Receiving a message from the queue.");
			_logger.LogTrace("Visibility timeout: {VisibilityTimeout}.", _options.VisibilityTimeout);

			var queueClient = await GetQueueClientAsync(queue, true, cancellationToken).ConfigureAwait(false);
			var result = await queueClient.ReceiveMessageAsync(_options.VisibilityTimeout, cancellationToken).ConfigureAwait(false);

			_logger.LogTrace("Message received.");

			if (result.Value == null)
			{
				_logger.LogDebug("No message(s) available to receive.");
				return null;
			}

			try
			{
				using (_logger.BeginScope(new Dictionary<string, object> { ["MessageId"] = result.Value.MessageId }))
				{
					var deserializer = _options.DeserializerFactory.Create<TMessage>();
					var message = deserializer.DeserializeMessage(result.Value.MessageText);

					_logger.LogTrace("Message deserialized.");
					return new AzureQueueStorageDequeuedMessage<TMessage>(message, result.Value);
				}
			}
			catch (Exception ex)
			{
				throw new MessageDeserializationException("Failed to deserialize message.", ex);
			}
		}
	}
}
