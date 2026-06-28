using thc.HotKnobs.Models;

namespace thc.HotKnobs.Domains.Dummy.Models;

public record Author : Entity
{
	public string? ExternalId { get; set; }

	public string FirstName { get; set; } = default!;

	public string LastName { get; set; } = default!;

	public string? Nationality { get; set; }
}
