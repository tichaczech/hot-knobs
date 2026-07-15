namespace Fand.Runtime.Queues;

/// <summary>
/// Represents the result of a send operation.
/// </summary>
public interface ISendReceipt
{
	/// <summary>
	/// Gets or sets the date and time when the message was enqueued in the queue.
	/// </summary>
	DateTimeOffset Enqueued { get; }

	/// <summary>
	/// Gets or sets the (unique) identifier of the message.
	/// </summary>
	string MessageId { get; }
}
