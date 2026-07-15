namespace Fand.Runtime.Storage;

/// <summary>
/// Exception thrown when an attempt is made to work with a container that does not exist.
/// </summary>
public class ContainerNotFoundException : CriticalException
{
	internal ContainerNotFoundException()
		: base("The specified container was not found.")
	{
	}

	/// <inheritdoc />
	public ContainerNotFoundException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public ContainerNotFoundException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
