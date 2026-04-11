using System.Diagnostics;

namespace thc.HotKnobs.Runtime;

/// <summary>
/// Inspired by https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/examples/AspNetCore/Instrumentation.cs
/// </summary>
public class Instrumentation : IDisposable
{
	private bool _disposed;

	public ActivitySource ActivitySource { get; }

	public Instrumentation(string activitySourceName, string version) => ActivitySource = new ActivitySource(activitySourceName, version);

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				ActivitySource.Dispose();
			}

			_disposed = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
