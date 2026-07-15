using CommunityToolkit.Diagnostics;

using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Mapping.Mapster;

public class MapsterMapperImpl : IMapper
{
	private readonly ILogger _logger;

	private readonly MapsterMapper.IMapper _mapper;

	public MapsterMapperImpl(ILogger<MapsterMapperImpl> logger, MapsterMapper.IMapper mapper)
	{
		Guard.IsNotNull(logger, nameof(logger));
		Guard.IsNotNull(mapper, nameof(mapper));

		_logger = logger;
		_mapper = mapper;
	}

	/// <inheritdoc />
	public TDestination Map<TDestination>(object source)
	{
		Guard.IsNotNull(source, nameof(source));

		try
		{
			return _mapper.Map<TDestination>(source);
		}
		catch (Exception ex)
		{
			throw CreateException(ex, source.GetType(), typeof(TDestination));
		}
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source)
	{
		Guard.IsNotNull(source, nameof(source));

		try
		{
			return _mapper.Map<TSource, TDestination>(source);
		}
		catch (Exception ex)
		{
			throw CreateException(ex, typeof(TSource), typeof(TDestination));
		}
	}

	/// <inheritdoc />
	public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(destination, nameof(destination));

		try
		{
#pragma warning disable IDE0001 // Simplify Names
			return _mapper.Map<TSource, TDestination>(source, destination);
#pragma warning restore IDE0001 // Simplify Names
		}
		catch (Exception ex)
		{
			throw CreateException(ex, typeof(TSource), typeof(TDestination));
		}
	}

	/// <inheritdoc />
	public object Map(object source, Type sourceType, Type destinationType)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(sourceType, nameof(sourceType));
		Guard.IsNotNull(destinationType, nameof(destinationType));

		try
		{
			return _mapper.Map(source, sourceType, destinationType);
		}
		catch (Exception ex)
		{
			throw CreateException(ex, sourceType, destinationType);
		}
	}

	/// <inheritdoc />
	public object Map(object source, object destination, Type sourceType, Type destinationType)
	{
		Guard.IsNotNull(source, nameof(source));
		Guard.IsNotNull(destination, nameof(destination));
		Guard.IsNotNull(sourceType, nameof(sourceType));
		Guard.IsNotNull(destinationType, nameof(destinationType));

		try
		{
			return _mapper.Map(source, destination, sourceType, destinationType);
		}
		catch (Exception ex)
		{
			throw CreateException(ex, sourceType, destinationType);
		}
	}

	private static MappingException CreateException(Exception ex, Type sourceType, Type destinationType) =>
		new($"Error occurred while mapping from {sourceType.FullName} to {destinationType.FullName}.", ex);
}
