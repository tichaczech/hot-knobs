using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fand.Runtime.Hosting;

/// <summary>
/// Options for configuring a host builder.
/// </summary>
public class HostBuilderOptions
{
	/// <summary>
	/// Gets or sets the configuration for the host builder.
	/// </summary>
	public IConfiguration? Configuration { get; set; } = null;

	/// <summary>
	/// Gets or sets the logger for the host builder.
	/// </summary>
	public ILogger? Logger { get; set; } = null;
}

/// <summary>
/// Represents the target of a host builder factory.
/// </summary>
public enum HostBuilderFactoryTarget
{
	Configuration,
	Logging,
	Services
}

/// <summary>
/// Default contract for host builder factory.
/// </summary>
public interface IHostBuilderFactory : IFactory
{
	/// <summary>
	/// Gets the target of the host builder factory.
	/// </summary>
	HostBuilderFactoryTarget Target { get; }

	/// <summary>
	///
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	IHostBuilder ConfigureBuilder(IHostBuilder builder, HostBuilderOptions options);
}

public interface IHostApplicationBuilderFactory : IFactory
{
	/// <summary>
	/// Gets the target of the host builder factory.
	/// </summary>
	HostBuilderFactoryTarget Target { get; }

	/// <summary>
	///
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="options"></param>
	/// <returns></returns>
	IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options);
}

