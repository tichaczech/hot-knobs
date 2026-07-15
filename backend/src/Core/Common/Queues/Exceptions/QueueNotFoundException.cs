namespace Fand.Runtime.Queues;

/// <summary>
/// Exception thrown when an attempt is made to work with a queue that does not exist.
/// </summary>
public class QueueNotFoundException : CriticalException
{
	internal QueueNotFoundException()
		: base("The specified queue was not found.")
	{
	}

	/// <inheritdoc />
	public QueueNotFoundException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public QueueNotFoundException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}

