using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Shared.Collections.Model.Entities;

public class Item : EntityWithName
{
	public required string Code { get; set; }

	public IDictionary<string, object>? Data { get; set; }

	[PartitionKey]
	public required string Type { get; set; }
}
