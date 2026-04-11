using Asp.Versioning;

using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace thc.HotKnobs.Runtime.Hosting;

public class ApiVersioningSetupHostBuilderFactory : IHostApplicationBuilderFactory
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

		options.Logger?.LogInformation("Configuring API Versioning...");

		// Initialize API Versioning
		var versioningBuilder = builder.Services.AddApiVersioning(options =>
		{
			options.ApiVersionReader = new UrlSegmentApiVersionReader();
			options.AssumeDefaultVersionWhenUnspecified = false;
			options.ReportApiVersions = true;
		});

		_ = versioningBuilder.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});

		return builder;
	}
}
