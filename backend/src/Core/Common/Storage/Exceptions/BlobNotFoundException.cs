namespace Fand.Runtime.Storage;

/// <summary>
/// Exception thrown when an attempt is made to work with a file that does not exist.
/// </summary>
public class BlobNotFoundException : NonCriticalException
{
	/// <inheritdoc />
	public BlobNotFoundException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public BlobNotFoundException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
