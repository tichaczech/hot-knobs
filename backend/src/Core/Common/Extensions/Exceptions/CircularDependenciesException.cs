namespace Fand.Runtime;

/// <summary>
/// Exception thrown when circular dependency is found during sorting items by their dependencies.
/// </summary>
public class CircularDependenciesException : CriticalException
{
	/// <inheritdoc />
	public CircularDependenciesException(string message)
		: base(message)
	{
	}

	/// <inheritdoc />
	public CircularDependenciesException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
