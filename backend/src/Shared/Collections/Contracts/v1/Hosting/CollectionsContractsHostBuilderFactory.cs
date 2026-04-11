using Fand.Runtime.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using mojeEUC.Contracts;
using mojeEUC.Contracts.Hosting;

namespace mojeEUC.Shared.Collections.Contracts.v1.Hosting;

public class CollectionsContractsHostBuilderFactory : IHostApplicationBuilderFactory
{
	const string COLLECTIONS_SERVICE_NAME = "shared-collections";

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder hostBuilder, HostBuilderOptions options)
	{
		var serviceDiscoveryConfig = hostBuilder.Configuration.GetSection("mojeEUC:ServiceDiscovery").Get<ServiceDiscoveryConfiguration>()!;

		if (!serviceDiscoveryConfig.TryGetServiceAddress(COLLECTIONS_SERVICE_NAME, out var address))
			return hostBuilder;

		var retryPolicy = hostBuilder.Services.CreateRetryPolicy(serviceDiscoveryConfig);

		hostBuilder.Services.AddConfiguredRefitClient<IItemByTypeService>(address, retryPolicy);
		hostBuilder.Services.AddConfiguredRefitClient<IItemService>(address, retryPolicy);

		return hostBuilder;
	}
}
