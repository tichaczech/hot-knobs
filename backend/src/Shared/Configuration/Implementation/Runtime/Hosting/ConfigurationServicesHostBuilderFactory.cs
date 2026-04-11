using Fand.Runtime.Configuration;
using Fand.Runtime.Hosting;
using Fand.Runtime.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mojeEUC.Shared.Configuration.Services;

namespace mojeEUC.Shared.Configuration.Runtime.Hosting;

public class ConfigurationServicesHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder hostBuilder, HostBuilderOptions options)
	{
		hostBuilder.Services.AddScoped<IItemService, ItemService>();

		return hostBuilder;
	}
}
