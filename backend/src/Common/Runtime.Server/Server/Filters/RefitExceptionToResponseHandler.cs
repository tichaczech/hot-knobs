using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Refit;

namespace thc.HotKnobs.Runtime.Server.Filters;

public class RefitExceptionToResponseHandler : IExceptionToHttpResponseHandler
{
	public RefitExceptionToResponseHandler(ILogger<RefitExceptionToResponseHandler> logger)
	{
	}

	public bool HandleException(ExceptionContext exceptionContext, out ExceptionToResponseContext? context)
	{
		ArgumentNullException.ThrowIfNull(exceptionContext);

		context = null;
		string? detail;
		switch (exceptionContext.Exception)
		{
			case ValidationApiException apiException:
				{
					detail = apiException.Content?.Detail ?? ((ApiException)apiException).Content ?? apiException.Message;
					break;
				}
			case ApiException apiException:
				{
					detail = apiException.Content ?? apiException.Message;
					break;
				}
			default:
				return false;
		}

		context = new ExceptionToResponseContext(StatusCodes.Status500InternalServerError, detail);
		return true;
	}
}
