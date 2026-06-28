namespace thc.HotKnobs.Models;

/// <summary>
/// Represents an error that occurs when an attempt is made to create an entity that already exists.
/// This exception is typically thrown during operations like adding a new record to a database
/// where a unique constraint would be violated.
/// </summary>
public class EntityAlreadyExistsException : EntityException
{
	/// <inheritdoc />
	public EntityAlreadyExistsException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
	{
	}
}

