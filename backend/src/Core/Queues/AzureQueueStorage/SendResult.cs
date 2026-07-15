namespace Fand.Runtime.Queues.AzureQueueStorage;

/// <summary>
/// Represents the result of an enqueue operation in an Azure Storage Account queue.
/// </summary>
internal class SendReceipt : ISendReceipt
{
	public DateTimeOffset Enqueued { get; }

	public string MessageId { get; }

	public SendReceipt(string messageId, DateTimeOffset enqueued)
	{
		Enqueued = enqueued;
		MessageId = messageId;
	}
}
