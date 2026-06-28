using Fand.Runtime;

namespace thc.HotKnobs.Models;

/// <summary>
/// Represents errors that occur during entity operations.
/// This is an abstract base class for more specific entity-related exceptions.
/// </summary>
public abstract class EntityException : NonCriticalException
{
	/// <inheritdoc />
	protected EntityException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	protected EntityException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
