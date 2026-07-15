namespace Fand.Runtime.Validation;

/// <summary>
/// Exception thrown when validation fails. This exception contains a collection of <see cref="ValidationError"/> that describes the validation error(s).
/// </summary>
public class ValidationException : NonCriticalException
{
	/// <summary>
	/// Gets the <see cref="ValidationError" /> instances that describe the validation errors.
	/// </summary>
	public IEnumerable<ValidationError> ValidationErrors { get; protected set; }

	/// <summary>
	/// Constructor that accepts an error message, a structured <see cref="ValidationError" /> describing the problem.
	/// </summary>
	/// <param name="errorMessage">The localized error message.</param>
	/// <param name="validationErrors">The value describing the validation error(s).</param>
	public ValidationException(string errorMessage, IEnumerable<ValidationError> validationErrors) : base(errorMessage)
	{
		ValidationErrors = validationErrors;
	}

	/// <summary>
	/// Constructor that accepts an error message, a structured <see cref="ValidationError" /> describing the problem, and an inner exception.
	/// </summary>
	/// <param name="message">The localized error message.</param>
	/// <param name="validationErrors">The value describing the validation error(s).</param>
	/// <param name="innerException">The inner exception.</param>
	public ValidationException(string message, IEnumerable<ValidationError> validationErrors, Exception innerException) : base(message, innerException)
	{
		ValidationErrors = validationErrors;
	}
}
