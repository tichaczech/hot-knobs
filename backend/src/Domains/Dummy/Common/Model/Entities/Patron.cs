using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Domains.Dummy.Model.Entities;

public class Patron : Entity
{
	public DateTime DateOfBirth { get; set; }

	public string FirstName { get; set; } = default!;

	public string LastName { get; set; } = default!;
}
