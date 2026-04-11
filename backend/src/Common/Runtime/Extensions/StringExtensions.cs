using System.Globalization;

using thc.HotKnobs.Runtime.Resources;

namespace thc.HotKnobs.Runtime.Extensions;

/// <summary>
/// Extensions for <see cref="String"/>.
/// It is used for localization, and it is used in <see cref="ResourceCache"/>.
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// Get localized error message by resource name.
	/// </summary>
	/// <param name="resourceName"></param>
	/// <param name="resourceType"></param>
	/// <param name="localizedMessage"></param>
	/// <param name="culture"></param>
	/// <param name="args"></param>
	/// <returns></returns>
	public static bool TryGetLocalized(this string resourceName, ResourceCache.ResourceType resourceType, out string? localizedMessage, CultureInfo? culture = default, params object[] args)
	{
		localizedMessage = ResourceCache.GetResourceValue(resourceName, resourceType, culture);
		localizedMessage = PlaceArgumentsInString(localizedMessage, args);
		return localizedMessage != null;
	}
	private static string? PlaceArgumentsInString(string? message, params object[] args)
	{
		return message != null ? String.Format(CultureInfo.CurrentCulture, message, args) : null;
	}
}
