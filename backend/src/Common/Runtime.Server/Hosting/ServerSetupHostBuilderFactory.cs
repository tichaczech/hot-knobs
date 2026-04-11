using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Fand.Runtime.Hosting;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Security;
using thc.HotKnobs.Runtime.Server.Controllers;
using thc.HotKnobs.Runtime.Server.Filters;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// <see cref="IHostApplicationBuilder"/> implementation for Runtime.Server.
/// </summary>
public class ServerSetupHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		options.Logger?.LogInformation("Configuring server setup...");

		// Add common services
		_ = builder.Services.AddHttpContextAccessor();
		_ = builder.Services.AddSingleton<IUserProvider, HttpContextUserProvider>();
		_ = builder.Services.AddSingleton<IConcurrencyTokenContext, ConcurrencyTokenContext>();
		_ = builder.Services.AddSingleton<IConcurrencyTokenContextAccessor>(x => x.GetRequiredService<IConcurrencyTokenContext>());

		// Set route options
		_ = builder.Services.Configure<RouteOptions>(options =>
		{
			options.LowercaseUrls = true;
			options.LowercaseQueryStrings = true;

		});

		// Add API Controllers
		var filters = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsAssignableTo(typeof(IFilterMetadata)) && type.FullName!.StartsWith("thc.HotKnobs", StringComparison.InvariantCultureIgnoreCase)).ToList();
		filters.ForEach(filter => builder.Services.AddScoped(filter));

		var mvcBuilder = builder.Services.AddControllers(options =>
		{
			var globalFilters = filters.Where(t => t.GetCustomAttribute<HttpMethodFilterAttribute>() == null);
			foreach (var filter in globalFilters)
				_ = options.Filters.Add(filter);

			var httpMethodFilters = filters.Where(t => t.GetCustomAttribute<HttpMethodFilterAttribute>() != null);
			options.Conventions.Add(new FilterConvention(httpMethodFilters));
		});
		_ = mvcBuilder.ConfigureApplicationPartManager(manager =>
		{
			// Add custom controller feature provider to support EntityController
			manager.FeatureProviders.Add(new EntityControllerFeatureProvider());
		});
		_ = mvcBuilder.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});

		// Add custom ProblemDetailsFactory
		_ = builder.Services.AddSingleton<ProblemDetailsFactory, CommonProblemDetailsFactory>();

		// Add ExceptionToHttpResponseHandlers
		var exceptionHandlers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsAssignableTo(typeof(IExceptionToHttpResponseHandler)) && !type.IsAssignableTo(typeof(IDefaultExceptionToHttpResponseHandler)) && type.IsClass).ToList();
		foreach (var handler in exceptionHandlers)
			_ = builder.Services.AddTransient(typeof(IExceptionToHttpResponseHandler), handler);
		_ = builder.Services.AddTransient<IExceptionToHttpResponseHandler, CommonExceptionToResponseHandler>();

		return builder;
	}
}
