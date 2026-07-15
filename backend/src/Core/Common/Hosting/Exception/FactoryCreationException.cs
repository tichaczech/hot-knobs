namespace Fand.Runtime.Hosting;

/// <summary>
/// Exception thrown when factory instance creation fails.
/// </summary>
public class FactoryCreationException : CriticalException
{
	public FactoryCreationException(string message)
		: base(message)
	{
	}

	public FactoryCreationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
