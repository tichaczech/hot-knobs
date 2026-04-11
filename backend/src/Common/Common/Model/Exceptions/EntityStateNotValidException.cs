namespace thc.HotKnobs.Model;

/// <summary>
/// Represents an error that occurs when an operation is attempted on an entity that is not in a valid state.
/// This exception is typically thrown when the entity's state does not allow the requested operation.
/// </summary>
public class EntityStateNotValidException : EntityException
{
	/// <inheritdoc />
	public EntityStateNotValidException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityStateNotValidException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
