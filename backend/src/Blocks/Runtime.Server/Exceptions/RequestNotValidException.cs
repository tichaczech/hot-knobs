using Fand.Runtime;

namespace thc.HotKnobs.Runtime;

public class RequestNotValidException : NonCriticalException
{
	/// <inheritdoc />
	public RequestNotValidException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public RequestNotValidException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
