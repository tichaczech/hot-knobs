using Fand.Runtime;

namespace thc.HotKnobs.Domains.Dummy.Worker;

internal class ImportException : NonCriticalException
{
	/// <inheritdoc />
	public ImportException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public ImportException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
