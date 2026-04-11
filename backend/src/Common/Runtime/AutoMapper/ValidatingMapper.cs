using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

using AutoMapper;

using Microsoft.Extensions.Logging;

namespace thc.HotKnobs.Runtime.AutoMapper;

/// <summary>
/// <see cref="IMapper"/> implementation that validates the destination object after mapping.
/// </summary>
public class ValidatingMapper : IMapper
{
	private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

	private readonly IMapper _innerMapper;

	private readonly ILogger<ValidatingMapper> _logger;

	/// <inheritdoc />
	public IConfigurationProvider ConfigurationProvider => _innerMapper.ConfigurationProvider;

	public ValidatingMapper(IMapper innerMapper, ILogger<ValidatingMapper> logger)
	{
		_innerMapper = innerMapper;
		_logger = logger;
	}

	/// <inheritdoc />
	public TDestination Map<TDestination>(object source)
	{
		var result = _innerMapper.Map<TDestination>(source);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source)
	{
		var result = _innerMapper.Map<TSource, TDestination>(source);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
	{
		var result = _innerMapper.Map(source, destination);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public object Map(object source, Type sourceType, Type destinationType)
	{
		var result = _innerMapper.Map(source, sourceType, destinationType);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public object Map(object source, object destination, Type sourceType, Type destinationType)
	{
		var result = _innerMapper.Map(source, destination, sourceType, destinationType);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
	{
		var result = _innerMapper.Map(source, opts);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
	{
		var result = _innerMapper.Map(source, opts);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
	{
		var result = _innerMapper.Map(source, destination, opts);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
	{
		var result = _innerMapper.Map(source, sourceType, destinationType, opts);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
	{
		var result = _innerMapper.Map(source, destination, sourceType, destinationType, opts);
		Validate(source, result);
		return result;
	}

	/// <inheritdoc />
	public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object? parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
	{
		var result = _innerMapper.ProjectTo(source, parameters, membersToExpand);
		foreach (var item in result)
		{
			Validate(source, item);
		}

		return result;
	}

	/// <inheritdoc />
	public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
	{
		var result = _innerMapper.ProjectTo<TDestination>(source, parameters, membersToExpand);
		foreach (var item in result)
		{
			Validate(source, item);
		}

		return result;
	}

	/// <inheritdoc />
	public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object>? parameters = null, params string[] membersToExpand)
	{
		var result = _innerMapper.ProjectTo(source, destinationType, parameters, membersToExpand);
		foreach (var item in result)
		{
			Validate(source, item);
		}

		return result;
	}

	public static TDestination MapAndValidate<TDestination>(object source, ResolutionContext context)
		where TDestination : class
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(context);

		var destination = context.Mapper.Map<TDestination>(source);
		try
		{
			Validate(destination);
			return destination;
		}
		catch (ValidationException e)
		{
			Console.WriteLine($"Error during mapping from {source.GetType().Name} to {destination.GetType().Name}: {e.Message}");
			throw;
		}
	}

	public static void Validate(object? destination)
	{
		ArgumentNullException.ThrowIfNull(destination, nameof(destination));

		var objectType = destination.GetType();
		var validationResults = new List<ValidationResult>();

		var context = new ValidationContext(destination);

		var metadataAttr = objectType.GetCustomAttribute<MetadataTypeAttribute>();
		if (metadataAttr?.MetadataClassType != null)
		{
			TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(objectType, metadataAttr.MetadataClassType), destination);
		}

		if (Validator.TryValidateObject(destination, context, validationResults, validateAllProperties: true))
		{
			return;
		}

		var message = String.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
		throw new ValidationException(message);
	}

	private void LogMapping(object? source, object? destination)
	{
		_logger.LogDebug("Mapping from {Name} to {Name}", source?.GetType().Name, destination?.GetType().Name);

		LogObject(source);
		LogObject(destination);
	}

	private void LogObject(object? obj)
	{
		_logger.LogDebug("{Name}:", obj?.GetType().Name);
		var sourceJson = JsonSerializer.Serialize(obj, _jsonSerializerOptions);
		_logger.LogDebug(sourceJson);
	}

	private void Validate(object? source, object? destination)
	{
		try
		{
			Validate(destination);
		}
		catch (ValidationException e)
		{
			LogMapping(source, destination);
			_logger.LogError(e.Message);
			throw;
		}
	}
}
