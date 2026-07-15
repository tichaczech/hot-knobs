namespace thc.HotKnobs.Contracts;

public class ServiceConfiguration
{
	public string CommonName { get; set; } = default!;

	public string DisplayName { get; set; } = default!;

	public string ClientId { get; set; } = default!;

	public string ClientSecret { get; set; } = default!;

#pragma warning disable CA2227 // Collection properties should be read only
	public Dictionary<string, string> ApplicationBlobContainers { get; set; } = [];

	public Dictionary<string, string> ApplicationKeys { get; set; } = [];

	public Dictionary<string, string> ApplicationQueues { get; set; } = [];
#pragma warning restore CA2227 // Collection properties should be read only
}
