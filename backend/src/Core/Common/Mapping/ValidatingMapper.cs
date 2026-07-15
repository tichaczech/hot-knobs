using CommunityToolkit.Diagnostics;

using Fand.Runtime.Validation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Mapping;

/// <summary>
/// <see cref="IMapper"/> implementation that validates the destination object after mapping using a validator.
/// </summary>
public class ValidatingMapper : IMapper
{
	private readonly ILogger<ValidatingMapper> _logger;

	private readonly IMapper _mapper;

	private readonly IValidator _validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="ValidatingMapper"/> class.
	/// </summary>
	/// <param name="logger">The logger to use for logging mapping and validation operations.</param>
	/// <param name="mapper">The underlying mapper to use for mapping operations.</param>
	/// <param name="validator">The validator to use for validating mapped objects.</param>
	public ValidatingMapper(ILogger<ValidatingMapper> logger, [FromKeyedServices(ExtensionsForIServiceCollection.SERVICE_KEY)] IMapper mapper, IValidator validator)
	{
		Guard.IsNotNull(logger, nameof(logger));
		Guard.IsNotNull(mapper, nameof(mapper));
		Guard.IsNotNull(validator, nameof(validator));

		_logger = logger;
		_mapper = mapper;
		_validator = validator;
	}

	/// <inheritdoc/>
	public TDestination Map<TDestination>(object source)
	{
		Guard.IsNotNull(source, nameof(source));

		var destination = _mapper.Map<TDestination>(source);
		_validator.ValidateObject(destination!);

		return destination;
	}

	/// <inheritdoc/>
	public TDestination Map<TSource, TDestination>(TSource source)
	{
		Guard.IsNotNull(source, nameof(source));

		var destination = _mapper.Map<TSource, TDestination>(source);
		_validator.ValidateObject(destination!);

		return destination;
	}

	/// <inheritdoc/>
	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(destination, nameof(destination));

#pragma warning disable IDE0001 // Simplify Names
		var result = _mapper.Map<TSource, TDestination>(source, destination);
#pragma warning restore IDE0001 // Simplify Names
		_validator.ValidateObject(result!);

		return result;
	}

	/// <inheritdoc/>
	public object Map(object source, Type sourceType, Type destinationType)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(sourceType, nameof(sourceType));
		Guard.IsNotNull(destinationType, nameof(destinationType));

		var destination = _mapper.Map(source, sourceType, destinationType);
		_validator.ValidateObject(destination!);

		return destination;
	}

	/// <inheritdoc/>
	public object Map(object source, object destination, Type sourceType, Type destinationType)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(destination, nameof(destination));
		Guard.IsNotNull(sourceType, nameof(sourceType));
		Guard.IsNotNull(destinationType, nameof(destinationType));

		var result = _mapper.Map(source, destination, sourceType, destinationType);
		_validator.ValidateObject(result!);

		return result;
	}
}
