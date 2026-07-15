namespace Fand.Runtime.Queues.AzureQueueStorage;

internal class AzureQueueStorageDequeuedMessage<TMessage> : IReceiveResult<TMessage>
	where TMessage : notnull
{
	public TMessage Message { get; }

	internal Azure.Storage.Queues.Models.QueueMessage QueueMessage { get; }

	public DateTimeOffset? ConfirmUntil => QueueMessage.NextVisibleOn;

	public AzureQueueStorageDequeuedMessage(TMessage message, Azure.Storage.Queues.Models.QueueMessage queueMessage)
	{
		Message = message;
		QueueMessage = queueMessage;
	}
}
