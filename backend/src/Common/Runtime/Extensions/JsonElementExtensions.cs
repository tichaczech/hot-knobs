using System.Text.Json;

namespace thc.HotKnobs.Runtime.Extensions;

public static class JsonElementExtensions
{
	/// <summary>
	/// Converts <see cref="JsonElement"/> to native .NET object.
	/// </summary>
	public static object? ToNativeObject(this JsonElement element)
	{
		return element.ValueKind switch
		{
			JsonValueKind.Null => null,
			JsonValueKind.True => true,
			JsonValueKind.False => false,
			JsonValueKind.Number => ConvertNumber(element),
			JsonValueKind.String => element.GetString(),
			JsonValueKind.Array => element.EnumerateArray().Select(x => x.ToNativeObject()).ToList(),
			JsonValueKind.Object => element.ToDictionary(),
			JsonValueKind.Undefined => throw new NotImplementedException(),
			_ => element.GetRawText(),
		};
	}

	/// <summary>
	/// Converts <see cref="JsonElement"/> to <see cref="Dictionary{TKey,TValue}"/>.
	/// </summary>
	public static Dictionary<string, object?> ToDictionary(this JsonElement element)
	{
		var dict = new Dictionary<string, object?>();
		if (element.ValueKind != JsonValueKind.Object)
			return dict;

		foreach (var prop in element.EnumerateObject())
		{
			dict[prop.Name] = prop.Value.ToNativeObject();
		}
		return dict;
	}

	private static object ConvertNumber(JsonElement element)
	{
		if (element.TryGetInt32(out var intVal))
			return intVal;
		if (element.TryGetInt64(out var longVal))
			return longVal;
		if (element.TryGetDecimal(out var decVal))
			return decVal;

		// fallback: double
		return element.GetDouble();
	}
}
