using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Runtime.Server.Filters;

public class IfNoneMatchHeaderFilter : BaseFilter, IActionFilter, IOrderedFilter
{
	private readonly IConcurrencyTokenContextAccessor _ctContextAccessor;

	public IfNoneMatchHeaderFilter(ILogger<IfNoneMatchHeaderFilter> logger, IConcurrencyTokenContextAccessor ctContextAccessor) => _ctContextAccessor = ctContextAccessor;

	/// <inheritdoc />
	public int Order => 10_001;

	/// <inheritdoc />
	public void OnActionExecuted(ActionExecutedContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		var result = context.Result as ObjectResult;
		string? id;
		if (result?.Value is IResourceResponse entityResponse)
			id = entityResponse.Id;
		// FIXME: Check fallback need
		// else if (!TryGetValueFromRoute(context, "id", out id))
		// 	return;
		else
			return;

		if (!_ctContextAccessor.TryGetValue(id, ConcurrencyTokenValueSource.Internal, out var token))
			return;

		if (token is not ETag etagInternal)
			throw new InvalidOperationException("Concurrency token value in context is not of type ETag.");

		context.HttpContext.Response.Headers.ETag = etagInternal.Value;

		if (!TryGetValueFromHeader(context, HeaderNames.IfNoneMatch, out var etagExternal))
			return;

		if (etagExternal.Trim('"') == etagInternal.Value && result is not null)
		{
			_ = context.HttpContext.Response.Headers.Remove(HeaderNames.ETag);
			result.StatusCode = (int)HttpStatusCode.NotModified;
			result.Value = null;
		}
	}

	/// <inheritdoc />
	public void OnActionExecuting(ActionExecutingContext context) { }
}
