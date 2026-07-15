namespace Fand.Runtime.Mapping;

/// <summary>
/// Defines a contract for a mapper that can map objects from one type to another.
/// </summary>
public interface IMapper
{
	/// <summary>
	/// Perform mapping from <paramref name="source"/> to new instance of type <typeparamref name="TDestination"/>.
	/// </summary>
	/// <remarks>
	/// Destination instance will be created using the default constructor of <typeparamref name="TDestination"/>.
	/// </remarks>
	/// <typeparam name="TDestination">Destination type to map to.</typeparam>
	/// <param name="source">Source object to map from.</param>
	/// <returns>Instance of <typeparamref name="TDestination"/> with values mapped from <paramref name="source"/>.</returns>
	TDestination Map<TDestination>(object source);

	/// <summary>
	/// Perform mapping from <paramref name="source"/> to new instance of type <typeparamref name="TDestination"/>.
	/// </summary>
	/// <remarks>
	/// Destination instance will be created using the default constructor of <typeparamref name="TDestination"/>.
	/// </remarks>
	/// <typeparam name="TSource">Source type to map from.</typeparam>
	/// <typeparam name="TDestination">Destination type to map to.</typeparam>
	/// <param name="source">Source object to map from.</param>
	/// <returns>Instance of <typeparamref name="TDestination"/> with values mapped from <paramref name="source"/>.</returns>
	TDestination Map<TSource, TDestination>(TSource source);

	/// <summary>
	/// Perform mapping from <paramref name="source"/> to <paramref name="destination"/>.
	/// </summary>
	/// <typeparam name="TSource">Source type to map from.</typeparam>
	/// <typeparam name="TDestination">Destination type to map to.</typeparam>
	/// <param name="source">Source object to map from.</param>
	/// <param name="destination">Destination object to map to.</param>
	/// <returns>Original <paramref name="destination"/> with values mapped from <paramref name="source"/>.</returns>
	TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

	/// <summary>
	/// Perform mapping from <paramref name="source"/> to new instance of type <paramref name="destinationType"/>.
	/// </summary>
	/// <remarks>
	/// Destination instance will be created using the default constructor of <paramref name="destinationType"/>.
	/// </remarks>
	/// <param name="source">Source object to map from.</param>
	/// <param name="sourceType">Type of the source object.</param>
	/// <param name="destinationType">Type of the destination object.</param>
	/// <returns>Instance of <paramref name="destinationType"/> with values mapped from <paramref name="source"/>.</returns>
	object Map(object source, Type sourceType, Type destinationType);

	/// <summary>
	/// Perform mapping from <paramref name="source"/> to <paramref name="destination"/>.
	/// </summary>
	/// <param name="source">Source object to map from.</param>
	/// <param name="destination">Destination object to map to.</param>
	/// <param name="sourceType">Type of the source object.</param>
	/// <param name="destinationType">Type of the destination object.</param>
	/// <returns>Original <paramref name="destination"/> with values mapped from <paramref name="source"/>.</returns>
	object Map(object source, object destination, Type sourceType, Type destinationType);
}
