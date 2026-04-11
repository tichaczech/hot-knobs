using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace thc.HotKnobs.Runtime.Server.Filters;

public class IfMatchHeaderFilter : BaseFilter, IActionFilter, IOrderedFilter
{
	private readonly IConcurrencyTokenContext _ctContext;

	public IfMatchHeaderFilter(IConcurrencyTokenContext ctContext) => _ctContext = ctContext;

	///<inheritdoc />
	public int Order => 10_000;

	/// <inheritdoc />
	public void OnActionExecuted(ActionExecutedContext context) { }

	/// <inheritdoc />
	public void OnActionExecuting(ActionExecutingContext context)
	{
		if (TryGetValueFromRoute(context, "id", out var id) && TryGetValueFromHeader(context, HeaderNames.IfMatch, out var etag))
			_ctContext.SetValue(id, ConcurrencyTokenValueSource.External, new ETag(etag.Trim('"')));
	}
}
