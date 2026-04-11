namespace thc.HotKnobs.Extensions;

/// <summary>
/// Extensions for string operations, particularly for CosmosDB connection strings.
/// </summary>
public static class ExtensionsForString
{
	/// <summary>
	/// Get element from CosmosDB connection string.
	/// </summary>
	/// <param name="connectionString"></param>
	/// <param name="elementName"></param>
	/// <param name="elementValue"></param>
	/// <returns></returns>
	public static bool TryGetElementFromCosmosDbConnectionString(this string connectionString, string elementName, out string? elementValue)
	{
		ArgumentNullException.ThrowIfNull(connectionString);
		ArgumentNullException.ThrowIfNull(elementName);

		try
		{
			var parts = connectionString.Split([';'], StringSplitOptions.RemoveEmptyEntries)
				.Select(part =>
				{
					var equalIndex = part.IndexOf('=', StringComparison.Ordinal);
					if (equalIndex <= 0 || equalIndex == part.Length - 1)
						throw new InvalidOperationException($"Invalid connection string part: '{part}'");

					var key = part[..equalIndex].Trim();
					var value = part[(equalIndex + 1)..].Trim();

					return new KeyValuePair<string, string>(key, value);
				})
				.ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.InvariantCultureIgnoreCase);
			return parts.TryGetValue(elementName, out elementValue);
		}
#pragma warning disable CA1031 // Do not catch general exception types
		catch (Exception)
#pragma warning restore CA1031 // Do not catch general exception types
		{
			elementValue = null;
			return false;
		}
	}
}
