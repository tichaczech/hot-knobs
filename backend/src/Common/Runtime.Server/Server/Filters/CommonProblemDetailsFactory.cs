using System.Globalization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using thc.HotKnobs.Runtime.Extensions;
using thc.HotKnobs.Runtime.Resources;

namespace thc.HotKnobs.Runtime.Server.Filters;

public class CommonProblemDetailsFactory : ProblemDetailsFactory
{
	private readonly ApiBehaviorOptions _options;
	private readonly Action<ProblemDetailsContext>? _configure;

	public CommonProblemDetailsFactory(IOptions<ApiBehaviorOptions> options, IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
	{
		_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
		_configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
	}

	public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
	{
		ArgumentNullException.ThrowIfNull(httpContext);

		statusCode ??= 500;
		_ = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();
		var problemDetails = new ProblemDetails
		{
			Status = statusCode,
			Title = title,
			Type = type,
			Instance = instance,
			Detail = detail // todo: not in prod: env.IsLocal()? detail: null
		};

		ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

		return problemDetails;
	}

	public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
	{
		ArgumentNullException.ThrowIfNull(modelStateDictionary);

		statusCode ??= 400;

		var problemDetails = new ValidationProblemDetails(modelStateDictionary)
		{
			Status = statusCode,
			Type = type,
			Instance = instance,
		};

		if (title != null)
		{
			// For validation problem details, don't overwrite the default title with null.
			problemDetails.Title = title;
		}

		ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

		return problemDetails;
	}

	private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
	{
		ArgumentNullException.ThrowIfNull(httpContext);
		ArgumentNullException.ThrowIfNull(problemDetails);

		problemDetails.Status ??= statusCode;

		if (problemDetails.Detail is null && "detail".TryGetLocalized(ResourceCache.ResourceType.ErrorMessages, out var detail))
			problemDetails.Detail = detail;

		if (problemDetails.Title is null && statusCode.ToString(CultureInfo.CurrentCulture).TryGetLocalized(ResourceCache.ResourceType.ErrorMessages, out var title))
			problemDetails.Title = title;

		if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
		{
			problemDetails.Title ??= clientErrorData.Title;
			problemDetails.Type ??= clientErrorData.Link;
		}

		// var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
		// if (traceId != null)
		// {
		//     problemDetails.Extensions["traceId"] = traceId;
		// }

		problemDetails.Extensions.Add("code", "");

		_configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
	}
}
