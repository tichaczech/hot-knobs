using Fand.Runtime.Configuration;
using Fand.Runtime.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Runtime.Configuration;

using StackExchange.Redis;

namespace thc.HotKnobs.Runtime.Hosting;

public class RedisHostBuilderFactory : IHostApplicationBuilderFactory
{
	private const string CONFIGURATION_SECTION_NAME = "HotKnobs:Runtime:Cache";

	/// <inheritdoc />
	public string Name => "default";

	/// <inheritdoc />
	public HostBuilderFactoryTarget Target => HostBuilderFactoryTarget.Services;

	/// <inheritdoc />
	public IHostApplicationBuilder ConfigureBuilder(IHostApplicationBuilder builder, HostBuilderOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		var configuration = builder.Configuration.GetSection(CONFIGURATION_SECTION_NAME).Get<RedisConfiguration>()!;
		// TODO:
		// if (!String.IsNullOrEmpty(builder.Configuration.GetConnectionStringEx(configuration.ConnectionString)) || builder.Environment.IsAzureHosted())
		if (!String.IsNullOrEmpty(builder.Configuration.GetConnectionStringEx(configuration.ConnectionString)))
		{
			// https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.StackExchangeRedis
			IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionStringEx(configuration.ConnectionString)!);
			_ = builder.Services.AddSingleton(connectionMultiplexer);

			options.Logger?.LogInformation("Configuring Redis using connection string...");
			_ = builder.Services.AddStackExchangeRedisCache(options =>
			{
				options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
				options.InstanceName = configuration.InstanceName;

				// TODO: fix redis configuration
				// options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
				// {
				//     AbortOnConnectFail = true,
				//     IncludeDetailInExceptions = true,
				//     CheckCertificateRevocation = false,
				//     ConnectRetry = 0,
				//     SslClientAuthenticationOptions = (options) =>
				//     {
				//         return null;
				//     },
				// };
			});
		}
		else
		{
			options.Logger?.LogInformation("Configuring Redis using memory cache...");
			_ = builder.Services.AddDistributedMemoryCache();
		}

		return builder;
	}
}
