namespace thc.HotKnobs.Contracts;

public class ServiceDiscoveryConfiguration
{
	public int RetryCount { get; set; } = 2;

	public int RetrySeconds { get; set; } = 1;

#pragma warning disable CA2227 // Collection properties should be read only
	public IDictionary<string, string> Services { get; set; } = new Dictionary<string, string>();
#pragma warning restore CA2227 // Collection properties should be read only

	public bool TryGetServiceAddress(string service, out string? address)
	{
		return Services.TryGetValue(service, out address);
	}
}
