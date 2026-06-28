using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using thc.HotKnobs.Shared.Collections.Services;

namespace thc.HotKnobs.Shared.Collections.Runtime.Hosting;

/// <inheritdoc />
public class ServicesHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.Services.AddScoped<IItemUseCases, ItemUseCases>();

		return builder;
	}
}
