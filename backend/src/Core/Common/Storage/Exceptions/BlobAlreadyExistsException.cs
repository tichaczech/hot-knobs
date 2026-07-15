namespace Fand.Runtime.Storage;

/// <summary>
/// Exception thrown when an attempt is made to create a file that already exists.
/// </summary>
public class BlobAlreadyExistsException : NonCriticalException
{
	/// <inheritdoc />
	public BlobAlreadyExistsException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public BlobAlreadyExistsException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
