namespace Fand.Runtime.Queues;

/// <summary>
/// Defines a contract for a queue producer that can send messages to a specified queue.
/// </summary>
public interface IQueueProducer
{
	/// <summary>
	/// Sends a message of type <typeparamref name="TMessage"/> to the specified <paramref name="queue"/>.
	/// </summary>
	/// <typeparam name="TMessage">The type of message to send.</typeparam>
	/// <param name="queue">The name of the queue to which the message should be added.</param>
	/// <param name="message">The message to send.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains an <see cref="ISendReceipt" /> object representing the result of the operation.</returns>
	Task<ISendReceipt> SendMessageAsync<TMessage>(string queue, TMessage message, CancellationToken cancellationToken = default)
		where TMessage : notnull;
}
