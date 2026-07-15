using Microsoft.Extensions.Configuration;

namespace Fand.Runtime.Tests;

public abstract class BaseFixture : IDisposable
{
	public IConfiguration Configuration { get; private set; }

	protected BaseFixture()
	{
		var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Local";
		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.AddJsonFile($"appsettings.{environmentName}.json", optional: true);

		Configuration = builder.Build();
	}

	protected virtual void Dispose(bool disposing) { }

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
