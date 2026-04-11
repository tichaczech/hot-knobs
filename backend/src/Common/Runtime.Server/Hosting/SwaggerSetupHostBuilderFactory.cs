using Asp.Versioning.ApiExplorer;

using Fand.Runtime.Hosting;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using thc.HotKnobs.Contracts;
using thc.HotKnobs.Runtime.Configuration;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace thc.HotKnobs.Runtime.Hosting;

public class SwaggerSetupHostBuilderFactory : IHostApplicationBuilderFactory
{
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	public string Name => "default";

	// TODO: https://stackoverflow.com/questions/70555022/swagger-swashbuckle-polymorphism-doesnt-work-with-interface-types
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		_ = builder.Services.AddSwaggerGen(options =>
		{
			options.DescribeAllParametersInCamelCase();

			// TODO: Read scopes from attributes instead from configuration.
			var scopes = new Dictionary<string, string>();
			var configuration = builder.Configuration.GetSection("HotKnobs:Runtime:Authentication").Get<AuthenticationConfiguration>()!;
			foreach (var scope in configuration.Scopes)
			{
				scopes.Add(scope.Value, String.Empty);
			}

			// TODO: Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
			options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows
				{
					Implicit = new OpenApiOAuthFlow
					{
						AuthorizationUrl = new Uri($"{configuration.Authority}/connect/authorize", UriKind.Absolute),
						Scopes = scopes
					}
				}
			});
			options.OperationFilter<SecurityRequirementsOperationFilter>();

			options.SchemaFilter<EnumSchemaFilter>(); // todo: find a way without custom filter.
			options.OperationFilter<AddResponseHeadersFilter>();
			options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

			var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "thc.HotKnobs.*.xml", SearchOption.TopDirectoryOnly).ToList();
			xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

		});

		_ = builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureVersioningSwaggerOptions>();

		return builder;
	}
}

public class EnumSchemaFilter : ISchemaFilter
{
	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		ArgumentNullException.ThrowIfNull(schema);
		ArgumentNullException.ThrowIfNull(context);

		if (context.Type.IsEnum)
		{
			schema.Enum.Clear();
			var enumType = context.Type;
			foreach (var name in Enum.GetNames(enumType))
			{
				schema.Enum.Add(new OpenApiString(name));
			}
		}
	}
}

public class SecurityRequirementsOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		ArgumentNullException.ThrowIfNull(operation);
		ArgumentNullException.ThrowIfNull(context);

		// Policy names map to scopes
		var requiredScopes = context.ApiDescription
			.CustomAttributes()
			.OfType<AuthorizeAttribute>()
			.Select(attr => attr.Policy)
			.Distinct()
			.ToArray();

		if (requiredScopes.Any())
		{
			operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
			operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

			var oAuthScheme = new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
			};

			operation.Security =
			[
				new() {
					[ oAuthScheme ] = requiredScopes.ToList()
				}
			];
		}
	}
}

// [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
// public class SwaggerGenOptionsAttribute : Attribute
// {
// 	public IDictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>
// 	{
// 		{ "author.read", "Read author(s)" },
// 		{ "author.write", "Write author(s)" },
// 		{ "book.read", "Read book(s)" },
// 		{ "book.write", "Write book(s)" },
// 		{ "loan.read", "Read loan(s)" },
// 		{ "loan.write", "Write loan(s)" },
// 		{ "patron.read", "Read patron(s)" },
// 		{ "patron.write", "Write patron(s)" },
// 		{ "reservation.read", "Read reservation(s)" },
// 		{ "reservation.write", "Write reservation(s)" },
// 	};
// }

// https://mohsen.es/api-versioning-and-swagger-in-asp-net-core-7-0-fe45f67d8419
// https://weblogs.asp.net/ricardoperes/asp-net-core-api-versioning
public class ConfigureVersioningSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
	private readonly IApiVersionDescriptionProvider _provider;
	private readonly IOptions<ServiceConfiguration> _serviceConfiguration;

	public ConfigureVersioningSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<ServiceConfiguration> serviceConfiguration)
	{
		_provider = provider;
		_serviceConfiguration = serviceConfiguration;
	}

	public void Configure(SwaggerGenOptions options)
	{
		foreach (var description in _provider.ApiVersionDescriptions)
			options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
	}

	// TODO: Find and set correct values for the OpenApiInfo object.
	private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
	{
		var serviceName = _serviceConfiguration.Value.DisplayName;

		var info = new OpenApiInfo()
		{
			Contact = new OpenApiContact
			{
				Email = "support@hotknobs.app",
				Name = "Hot Knobs Team",
				Url = new Uri("https://hotknobs.app/")
			},
			Description = "Hot Knobs service interface specification.",
			License = new OpenApiLicense
			{
				Name = "Hot Knobs Closed Source Commercial License (HKCSCL)",
				Url = new Uri("https://license.hotknobs.app/")
			},
			TermsOfService = new Uri("https://hotknobs.app/"),
			Title = $"Hot Knobs API: {serviceName}",
			Version = description.ApiVersion.ToString()
		};

		if (description.IsDeprecated)
			info.Description += " This API version has been deprecated.";

		return info;
	}
}
