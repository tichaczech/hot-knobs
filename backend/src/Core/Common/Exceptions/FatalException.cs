namespace Fand.Runtime;

/// <summary>
/// Base class used to implement custom fatal exceptions within Fand framework.
/// </summary>
/// <remarks>
/// A fatal exception should be thrown in the situation when the program can not continue and termination of the entire program is required.
/// </remarks>
[Serializable]
public abstract class FatalException : CriticalException
{
	/// <inheritdoc />
	protected FatalException(string message) : base(message) { }

	/// <inheritdoc />
	protected FatalException(string message, Exception innerException) : base(message, innerException) { }
}
