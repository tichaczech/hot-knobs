using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Validation.DataAnnotations;

public class DataAnnotationsValidator : IValidator
{
	private readonly ILogger _logger;

	public DataAnnotationsValidator(ILogger<DataAnnotationsValidator> logger)
	{
		Guard.IsNotNull(logger, nameof(logger));

		_logger = logger;
	}

	/// <inheritdoc />
	public bool TryValidateObject(object instance, out IEnumerable<ValidationError> validationResults)
	{
		Guard.IsNotNull(instance, nameof(instance));

		var validationResultsList = new List<ValidationResult>();
		var context = new ValidationContext(instance);

		if (!Validator.TryValidateObject(instance, context, validationResultsList, validateAllProperties: true))
		{
			validationResults = validationResultsList.Select(vr => new ValidationError(vr.ErrorMessage ?? "Unknown validation error", vr.MemberNames));
			return false;
		}

		validationResults = [];
		return true;
	}

	/// <inheritdoc />
	public void ValidateObject(object instance)
	{
		Guard.IsNotNull(instance, nameof(instance));

		if (!TryValidateObject(instance, out var validationResults))
			throw new ValidationException($"Validation failed for the object: {instance}.", validationResults);
	}
}
