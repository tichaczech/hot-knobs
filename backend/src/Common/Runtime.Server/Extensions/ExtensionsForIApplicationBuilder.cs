using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

using thc.HotKnobs.Runtime.Resources;

namespace thc.HotKnobs.Runtime.Hosting;

/// <summary>
/// Extensions for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ExtensionsForIApplicationBuilder
{
	/// <summary>
	/// Initializes the Web application for Runtime.Server.
	/// </summary>
	/// <param name="app"></param>
	/// <returns></returns>
	public static IApplicationBuilder InitializeApplication(this WebApplication app)
	{
		ArgumentNullException.ThrowIfNull(app);

		_ = app.MapControllers();

		var supportedCultures = new[] { "en-US", "cs-CZ" };
		var localizationOptions = new RequestLocalizationOptions()
			.SetDefaultCulture(supportedCultures[0])
			.AddSupportedCultures(supportedCultures)
			.AddSupportedUICultures(supportedCultures);
		_ = app.UseRequestLocalization(localizationOptions);
		ResourceCache.InitializeResourceTypes(supportedCultures[0]);

		_ = app.UseAuthentication();
		_ = app.UseAuthorization();

		_ = app.UseCors(options =>
		{
			_ = options.AllowAnyOrigin();
			_ = options.AllowAnyMethod();
			_ = options.AllowAnyHeader();
			_ = options.SetIsOriginAllowedToAllowWildcardSubdomains();
		});

		// TODO: Uncomment when logging is implemented.
		// app.UseHttpLogging();

		// Configure the Swagger UI.
		if (app.Environment.IsLocal() || app.Environment.IsDevelopment() || app.Environment.IsStaging())
		{
			_ = app.UseSwagger();
			_ = app.UseSwaggerUI(options =>
			{
				var versions = app.DescribeApiVersions();
				foreach (var version in versions)
				{
					var url = $"/swagger/{version.GroupName}/swagger.json";
					var name = version.GroupName;

					options.SwaggerEndpoint(url, name);
				}
			});
		}

		return app;
	}
}
