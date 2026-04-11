using Fand.Runtime.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using thc.HotKnobs.Shared.Users.UseCases;

namespace thc.HotKnobs.Shared.Users.Runtime.Hosting;

/// <summary>
/// Host Builder Factory for User Services.
/// </summary>
public class UsersServicesHostBuilderFactory : IHostApplicationBuilderFactory
{
	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.Services.AddScoped<IDeviceUseCases, DeviceUseCases>();
		_ = builder.Services.AddScoped<IProfileUseCases, ProfileUseCases>();

		return builder;
	}
}
