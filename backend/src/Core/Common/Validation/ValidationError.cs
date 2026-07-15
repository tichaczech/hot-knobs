namespace Fand.Runtime.Validation;

/// <summary>
/// Container class for the results of a validation request.
/// </summary>
public class ValidationError
{
	public ValidationError(string errorMessage, IEnumerable<string>? memberNames)
	{
		ErrorMessage = errorMessage;
		MemberNames = memberNames ?? Array.Empty<string>();
	}

	/// <summary>
	/// Gets the collection of member names affected by this result.  The collection may be empty but will never be null.
	/// </summary>
	public IEnumerable<string> MemberNames { get; protected set; }

	/// <summary>
	/// Gets the error message for this result.
	/// </summary>
	public string ErrorMessage { get; protected set; }

	/// <summary>
	/// Override the string representation of this instance, returning the <see cref="ErrorMessage" />.
	/// </summary>
	/// <returns>
	/// The <see cref="ErrorMessage" /> property value.
	/// </returns>
	public override string ToString() => ErrorMessage;
}
