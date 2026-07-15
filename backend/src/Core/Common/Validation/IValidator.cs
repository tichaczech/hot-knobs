namespace Fand.Runtime.Validation;

/// <summary>
/// Defines a contract for a validator that can validate objects and return validation results.
/// </summary>
public interface IValidator
{
	/// <summary>
	/// Validates the specified object and returns a collection of validation errors, if any.
	/// </summary>
	/// <param name="instance">The object to validate.</param>
	/// <param name="validationResults">A collection of validation errors, if any.</param>
	/// <returns><c>true</c> if the object is valid; otherwise, <c>false</c>.</returns>
	bool TryValidateObject(object instance, out IEnumerable<ValidationError> validationResults);

	/// <summary>
	/// Validates the specified object and throws a <see cref="ValidationException"/> if validation fails.
	/// </summary>
	/// <param name="instance">The object to validate.</param>
	/// <exception cref="ValidationException">Thrown when validation fails.</exception>
	void ValidateObject(object instance);
}
