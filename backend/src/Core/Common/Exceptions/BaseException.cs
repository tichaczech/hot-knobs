namespace Fand.Runtime;

/// <summary>
/// Base class used to implement custom exceptions within Fand framework.
/// </summary>
/// <remarks>
/// All custom exceptions must derive from this class!
/// </remarks>
[Serializable]
public abstract class BaseException : Exception
{
	/// <inheritdoc />
	protected BaseException(string message) : base(message) { }

	/// <inheritdoc />
	protected BaseException(string message, Exception innerException) : base(message, innerException) { }

	/// <summary>
	/// Using parameter-less constructor is prohibited.
	/// </summary>
	private BaseException()
	{
	}
}
