using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <summary>
/// Implements the result filter to deal with Prefer header.
/// </summary>
/// <remarks>
/// In this implementation, it's assumed that the by default the result is according to the return=representation value.
///
/// Prefer header specification: https://tools.ietf.org/html/rfc7240
/// </remarks>
[HttpMethodFilter("POST", "PUT")]
public class PreferHeaderFilter : BaseFilter, IOrderedFilter, IResultFilter
{
	/// <inheritdoc />r
	public int Order => 1000;

	private readonly ILogger _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="PreferHeaderFilter"/> class.
	/// </summary>
	/// <param name="logger"></param>
	public PreferHeaderFilter(ILogger<PreferHeaderFilter> logger) => _logger = logger;

	/// <inheritdoc />
	public void OnResultExecuting(ResultExecutingContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		if (!HasPreferHeaderWithReturnRepresentation(context.HttpContext.Request))
		{

			var result = context.Result as ObjectResult;
			if (result?.Value is IResourceResponse)
			{
				context.HttpContext.Response.Headers.Append("Preference-Applied", "return=minimal");

				result.ContentTypes.Clear();
				result.Value = null;
			}

			return;
		}

		context.HttpContext.Response.Headers.Append("Preference-Applied", "return=representation");
	}

	/// <inheritdoc />
	public void OnResultExecuted(ResultExecutedContext context) { }

	private bool HasPreferHeaderWithReturnRepresentation(HttpRequest request) =>
		!request.Headers.TryGetValue("Prefer", out var header) || header.Any(IsReturnRepresentation);

	private bool IsReturnRepresentation(string? value) =>
		value?.Split(';').Any(v => v.Equals("return=representation", StringComparison.OrdinalIgnoreCase)) ?? false;
}
