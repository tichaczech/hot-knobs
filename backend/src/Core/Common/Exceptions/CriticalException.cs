namespace Fand.Runtime;

/// <summary>
/// Base class used to implement custom critical exceptions within Fand framework.
/// </summary>
/// <remarks>
/// A critical exception should be thrown in the situation when immediate intervention is expected to happen to deal with unexpected system behavior. However, the program can continue to run, and no termination of the entire program is required (contrary to a fatal exception).
/// </remarks>
[Serializable]
public abstract class CriticalException : BaseException
{
	/// <inheritdoc />
	protected CriticalException(string message) : base(message) { }

	/// <inheritdoc />
	protected CriticalException(string message, Exception innerException) : base(message, innerException) { }
}
