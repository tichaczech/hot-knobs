namespace Fand.Runtime;

/// <summary>
/// Base class used to implement custom non-critical exceptions within Fand framework.
/// </summary>
/// <remarks>
/// A non-critical exception should be thrown in the situation when immediate intervention is not needed (or expected) and the program can continue to run. It is typically an exception thrown upon user input validation, business validation, etc.
/// </remarks>
[Serializable]
public abstract class NonCriticalException : BaseException
{
	/// <inheritdoc />
	protected NonCriticalException(string message) : base(message) { }

	/// <inheritdoc />
	protected NonCriticalException(string message, Exception innerException) : base(message, innerException) { }
}
