namespace Fand.Runtime.Queues;

/// <summary>
/// Defines a contract for a queue consumer that can receive messages from a specified queue.
/// </summary>
public interface IQueueConsumer
{
	/// <summary>
	/// Confirms that a message was received and processed successfully.
	/// </summary>
	/// <typeparam name="TMessage">The type of message that was received.</typeparam>
	/// <param name="queue">The name of the queue the message was received from.</param>
	/// <param name="message">The message that was received and processed.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
	/// <exception cref="MessageConfirmationFailedException">>The message confirmation failed.</exception>
	/// <exception cref="QueueNotFoundException">The specified queue does not exist.</exception>
	Task ConfirmReceiveAsync<TMessage>(string queue, IReceiveResult<TMessage> message, CancellationToken cancellationToken = default)
		where TMessage : notnull;

	/// <summary>
	/// Receives a message of type <typeparamref name="TMessage"/> from the specified queue.
	/// </summary>
	/// <typeparam name="TMessage">The type of message.</typeparam>
	/// <param name="queue">The name of the queue to receive from.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
	/// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the received message, or <see langword="null"/> if no message was found.</returns>
	/// <remarks>
	/// The method should return <see langword="null"/> if no message was received.
	/// </remarks>
	/// <remarks>
	/// Received message has to be confirmed using <see cref="ConfirmReceiveAsync{TMessage}(String, IReceiveResult{TMessage}, CancellationToken)"/> method after successful processing. The message will be returned back the the queue after the <see cref="IReceiveResult{TMessage}.ConfirmUntil"/> time has passed if not confirmed.
	/// </remarks>
	/// <exception cref="QueueNotFoundException">The specified queue does not exist.</exception>
	Task<IReceiveResult<TMessage>?> ReceiveMessageAsync<TMessage>(string queue, CancellationToken cancellationToken = default)
		where TMessage : notnull;
}
