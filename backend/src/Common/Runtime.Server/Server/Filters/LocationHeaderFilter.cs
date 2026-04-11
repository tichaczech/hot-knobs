using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Contracts;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <summary>
/// Implements the result filter to deal with Location header.
/// </summary>
/// <remarks>
/// Location header specification: https://datatracker.ietf.org/doc/html/rfc2616%23section-14.30 (unescape URL before using, there is a bug in SauceControl.InheritDoc plugin)
/// </remarks>
[HttpMethodFilter("POST")]
public class LocationHeaderFilter : BaseFilter, IOrderedFilter, IResultFilter
{
	/// <inheritdoc />
	public int Order => 100;

	private readonly LinkGenerator _linkGenerator;

	private readonly ILogger _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="LocationHeaderFilter"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="linkGenerator"></param>
	public LocationHeaderFilter(ILogger<LocationHeaderFilter> logger, LinkGenerator linkGenerator)
	{
		_logger = logger;
		_linkGenerator = linkGenerator;
	}

	/// <inheritdoc />
	public void OnResultExecuting(ResultExecutingContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		var result = context.Result as ObjectResult;
		if (result?.Value is IResourceResponse response)
		{
			var headers = context.HttpContext.Response.GetTypedHeaders();

			// todo: fix the link generator
			var link = _linkGenerator.GetUriByAction(context.HttpContext, "Get", null, new { id = response.Id }, context.HttpContext.Request.Scheme);
			if (String.IsNullOrEmpty(link))
			{
				_logger.LogWarning("Location header can not not set because generated link is empty!");
				return;
			}

			headers.Location = new Uri(link);
		}
	}

	/// <inheritdoc />
	public void OnResultExecuted(ResultExecutedContext context) { }
}
