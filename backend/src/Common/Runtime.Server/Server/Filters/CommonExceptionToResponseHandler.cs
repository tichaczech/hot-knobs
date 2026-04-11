using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using thc.HotKnobs.Model;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <inheritdoc cref="IExceptionToHttpResponseHandler" />
public class CommonExceptionToResponseHandler : IExceptionToHttpResponseHandler, IDefaultExceptionToHttpResponseHandler
{
	/// <inheritdoc />
	public bool HandleException(ExceptionContext exceptionContext, out ExceptionToResponseContext? context)
	{
		ArgumentNullException.ThrowIfNull(exceptionContext);

		var statusCode = exceptionContext.Exception switch
		{
			EntityAlreadyExistsException => StatusCodes.Status409Conflict,
			EntityChangedInBackgroundException => StatusCodes.Status412PreconditionFailed,
			EntityNotActiveException => StatusCodes.Status409Conflict,
			EntityNotFoundException => StatusCodes.Status404NotFound,
			EntityNotFoundOrNotActiveException => StatusCodes.Status410Gone,
			EntityStateNotValidException => StatusCodes.Status409Conflict,
			NotImplementedException => StatusCodes.Status501NotImplemented,
			UnauthorizedAccessException => StatusCodes.Status403Forbidden,
			_ => StatusCodes.Status500InternalServerError
		};

		context = new ExceptionToResponseContext(statusCode, exceptionContext.Exception.Message);

		return true;
	}
}
