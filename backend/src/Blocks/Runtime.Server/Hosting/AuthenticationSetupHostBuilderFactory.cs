using Fand.Runtime.Hosting;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using thc.HotKnobs.Runtime.Configuration;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// Implements the host builder factory to deal with authentication.
/// </summary>
public class AuthenticationSetupHostBuilderFactory : IHostApplicationBuilderFactory
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

		options.Logger?.LogInformation("Configuring authentication...");

		var configuration = builder.Configuration.GetSection(AuthenticationConfigurationHostBuilderFactory.CONFIGURATION_SECTION_NAME).Get<AuthenticationConfiguration>()
			?? throw new InvalidOperationException($"Configuration section '{AuthenticationConfigurationHostBuilderFactory.CONFIGURATION_SECTION_NAME}' is not found or is invalid.");

		_ = builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
		{
			options.Audience = configuration.Audience;
			options.Authority = configuration.Authority;
			options.IncludeErrorDetails = true;
			options.MetadataAddress = configuration.MetadataUrl!;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				NameClaimType = "user_id",
				// TODO: Refactor later
				RequireAudience = configuration.RequireAudience,
				RequireExpirationTime = configuration.RequireExpirationTime,
				RequireSignedTokens = configuration.RequireSignedTokens,
				ValidateActor = configuration.ValidateActor,
				ValidateAudience = configuration.ValidateAudience,
				ValidateIssuer = configuration.ValidateIssuer,
				ValidIssuers = [configuration.Issuer],
				ValidateIssuerSigningKey = configuration.ValidateIssuerSigningKey,
				ValidateLifetime = configuration.ValidateLifetime,
				ValidIssuer = configuration.Issuer
			};

			// if (hostBuilder.Environment.IsLocalHosted())
			// {
			// 	options.TokenValidationParameters.RequireAudience = configuration.RequireAudience;
			// 	options.TokenValidationParameters.RequireExpirationTime = configuration.RequireExpirationTime;
			// 	options.TokenValidationParameters.RequireSignedTokens = configuration.RequireSignedTokens;
			// 	options.TokenValidationParameters.ValidateActor = configuration.ValidateActor;
			// 	options.TokenValidationParameters.ValidateAudience = configuration.ValidateAudience;
			// 	options.TokenValidationParameters.ValidateIssuer = configuration.ValidateIssuer;
			// 	options.TokenValidationParameters.ValidateIssuerSigningKey = configuration.ValidateIssuerSigningKey;
			// 	options.TokenValidationParameters.ValidateLifetime = configuration.ValidateLifetime;
			// 	options.TokenValidationParameters.ValidateAudience = configuration.ValidateAudience;
			// }
		});

		_ = builder.Services.AddAuthorization(options =>
		{
			var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
			defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

			options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
			foreach (var scope in configuration.Scopes)
			{
				options.AddPolicy(scope.Key, policy =>
				{
					_ = policy.RequireClaim("scope", scope.Value);
				});
			}
		});
		_ = builder.Services.AddTransient<IClaimsTransformation, FirebaseClaimsPrincipalTransformation>();

		return builder;
	}
}
