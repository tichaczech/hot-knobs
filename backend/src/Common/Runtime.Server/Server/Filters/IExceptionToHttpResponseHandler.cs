using Microsoft.AspNetCore.Mvc.Filters;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <summary>
/// Context that contains the HTTP status code.
/// </summary>
/// <param name="StatusCode"></param>
/// <param name="Detail"></param>
public record ExceptionToResponseContext(int StatusCode, string? Detail = null);

/// <summary>
/// Handles exceptions by mapping them to HTTP status codes.
/// </summary>
public interface IExceptionToHttpResponseHandler
{
	/// <summary>
	/// Handles the given exception and outputs the corresponding HTTP status code.
	/// </summary>
	/// <param name="exceptionContext"></param>
	/// <param name="context">The context that contains the HTTP status code.</param>
	/// <returns>Always returns true.</returns>
	bool HandleException(ExceptionContext exceptionContext, out ExceptionToResponseContext? context);
}

/// <summary>
/// Default exception handler that maps all exceptions to HTTP status code 500.
/// </summary>
public interface IDefaultExceptionToHttpResponseHandler;
