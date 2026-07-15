namespace Fand.Runtime.Queues;

/// <summary>
/// Represents the message received from a queue.
/// </summary>
public interface IReceiveResult<TMessage>
	where TMessage : notnull
{
	/// <summary>
	/// Gets the time until the message has to be confirmed.
	/// </summary>
	DateTimeOffset? ConfirmUntil { get; }

	/// <summary>
	/// Gets the message that was dequeued.
	/// </summary>
	TMessage Message { get; }
}
