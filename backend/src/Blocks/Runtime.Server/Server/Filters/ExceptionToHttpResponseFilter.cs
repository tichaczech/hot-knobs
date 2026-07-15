using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <summary>
/// Exception handling filter.
/// </summary>
public class ExceptionToHttpResponseFilter : BaseFilter, IExceptionFilter
{
	private readonly ILogger<ExceptionToHttpResponseFilter> _logger;
	private readonly IEnumerable<IExceptionToHttpResponseHandler> _handlers;
	private readonly ProblemDetailsFactory _detailsFactory;

	public ExceptionToHttpResponseFilter(ILogger<ExceptionToHttpResponseFilter> logger, IEnumerable<IExceptionToHttpResponseHandler> handlers, ProblemDetailsFactory detailsFactory)
	{
		_logger = logger;
		_handlers = handlers;
		_detailsFactory = detailsFactory;
	}

	/// <inheritdoc />
	public void OnException(ExceptionContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		ProblemDetails? problem = null;
		foreach (var handler in _handlers)
		{
			if (!handler.HandleException(context, out var handleContext) || handleContext == null)
				continue;

			problem = _detailsFactory.CreateProblemDetails(context.HttpContext, handleContext.StatusCode, detail: handleContext.Detail /*env.IsLocal() ? handleContext.Detail : null*/); // todo not in prod
			break;
		}

		context.Result = new ObjectResult(problem);

		if (problem?.Detail == null)
			_logger.LogError(context.Exception, "Unhandled exception: {Message}", context.Exception.Message);

#pragma warning disable CA2254 // Template should be a static expression
		_logger.LogError(context.Exception, problem?.Detail);
#pragma warning restore CA2254 // Template should be a static expression

		// This is default exception handling, we assume that exception is handled by us
		context.ExceptionHandled = true;
	}
}
