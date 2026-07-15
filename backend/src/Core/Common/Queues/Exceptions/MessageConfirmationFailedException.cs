namespace Fand.Runtime.Queues;

/// <summary>
/// Exception thrown when an attempt is made to confirm a message that has already been deleted or has an invalid pop receipt.
/// </summary>
public class MessageConfirmationFailedException : CriticalException
{
	internal MessageConfirmationFailedException()
		: base("Message has already been deleted or the pop receipt is invalid.")
	{
	}

	/// <inheritdoc />
	public MessageConfirmationFailedException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public MessageConfirmationFailedException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}

public class MessageDeserializationException : CriticalException
{
	internal MessageDeserializationException()
		: base("Failed to deserialize message.")
	{
	}

	/// <inheritdoc />
	public MessageDeserializationException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public MessageDeserializationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}

public class MessageSerializationException : CriticalException
{
	internal MessageSerializationException()
		: base("Failed to serialize message.")
	{
	}

	/// <inheritdoc />
	public MessageSerializationException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public MessageSerializationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
