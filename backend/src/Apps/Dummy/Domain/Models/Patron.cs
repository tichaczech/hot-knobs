using thc.HotKnobs.Models;

namespace thc.HotKnobs.Domains.Dummy.Models;

public record Patron : Entity
{
	public DateTime DateOfBirth { get; set; }

	public string FirstName { get; set; } = default!;

	public string LastName { get; set; } = default!;
}
