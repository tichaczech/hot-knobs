using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Domains.Dummy.Model.Entities;

public class Author : Entity
{
	public string? ExternalId { get; set; }

	public string FirstName { get; set; } = default!;

	public string LastName { get; set; } = default!;

	public string? Nationality { get; set; }
}
