namespace thc.HotKnobs.Model;

/// <summary>
/// Represents an exception that is thrown when an entity has been changed in the background
/// while an operation was being performed on it. This typically indicates a concurrency conflict.
/// </summary>
public class EntityChangedInBackgroundException : EntityException
{
	/// <inheritdoc />
	public EntityChangedInBackgroundException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityChangedInBackgroundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
