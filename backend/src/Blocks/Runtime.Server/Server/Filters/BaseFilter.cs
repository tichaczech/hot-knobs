using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Mvc;

namespace thc.HotKnobs.Runtime.Server.Filters;

public abstract class BaseFilter
{
	/// <summary>
	/// Returns parameter value from request route.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="parameterName"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryGetValueFromRoute(ActionContext context, string parameterName, out string value)
	{
		ArgumentNullException.ThrowIfNull(context);
		ArgumentException.ThrowIfNullOrEmpty(parameterName);

		value = String.Empty;

		if (context.RouteData.Values.TryGetValue(parameterName, out var obj))
			value = obj!.ToString()!;

		return !String.IsNullOrEmpty(value);
	}

	/// <summary>
	/// Returns value of header (if exists) from request.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="headerName"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryGetValueFromHeader(ActionContext context, string headerName, out string value)
	{
		ArgumentNullException.ThrowIfNull(context);
		ArgumentException.ThrowIfNullOrEmpty(headerName);

		value = String.Empty;

		if (context.HttpContext.Request.Headers.TryGetValue(headerName, out var values) && values.Count != 0)
			value = values.First()!;

		return !String.IsNullOrEmpty(value);
	}

	/// <summary>
	/// Returns value of header (if exists) from request as DateTimeOffset (when parsed).
	/// </summary>
	/// <param name="context"></param>
	/// <param name="headerName"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryGetValueFromHeaderAsDateTimeOffset(ActionContext context, string headerName, out DateTimeOffset value)
	{
		ArgumentNullException.ThrowIfNull(context);
		ArgumentException.ThrowIfNullOrEmpty(headerName);

		value = DateTimeOffset.MinValue;

		if (TryGetValueFromHeader(context, headerName, out var str))
		{
			if (DateTimeOffset.TryParse(str, out var parsed))
				value = parsed;
		}

		return value != DateTimeOffset.MinValue;
	}
}
