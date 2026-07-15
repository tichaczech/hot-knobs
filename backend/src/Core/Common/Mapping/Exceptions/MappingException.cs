namespace Fand.Runtime.Mapping;

/// <summary>
/// Exception thrown when a mapping error occurs.
/// </summary>
public class MappingException : CriticalException
{
	/// <inheritdoc />
	public MappingException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public MappingException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
