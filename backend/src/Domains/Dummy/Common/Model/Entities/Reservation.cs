using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Domains.Dummy.Model.Entities;

public class Reservation : Entity
{
	public string BookId { get; set; } = Guid.NewGuid().ToString();

	public string PatronId { get; set; } = Guid.NewGuid().ToString();

	public DateTime StartsOn { get; set; } = DateTime.UtcNow;

	public DateTime EndsOn { get; set; } = DateTime.UtcNow;
}
