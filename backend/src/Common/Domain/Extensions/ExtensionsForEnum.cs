namespace thc.HotKnobs.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Enum"/> types.
/// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public static class ExtensionsForEnum
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
	/// <summary>
	/// Gets an enumerable collection of all individual flags that are set in the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum. Must be a struct and an Enum.</typeparam>
	/// <param name="value">The enum value from which to extract the flags.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> containing all the individual flag values that are set in <paramref name="value"/>.
	/// If <paramref name="value"/> has no flags set or is a non-flags enum with a single value,
	/// the collection will contain that single value. If <paramref name="value"/> is 0 for a flags enum,
	/// it might return the 0 value if defined, or an empty collection depending on the enum definition.
	/// </returns>
	public static IEnumerable<T> GetFlags<T>(this T value) where T : struct, Enum => Enum.GetValues<T>().Where(member => value.HasFlag(member)).ToArray();
}
