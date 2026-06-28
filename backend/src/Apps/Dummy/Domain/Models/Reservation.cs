using thc.HotKnobs.Models;

namespace thc.HotKnobs.Domains.Dummy.Models;

public record Reservation : Entity
{
	public string BookId { get; set; } = Guid.NewGuid().ToString();

	public string PatronId { get; set; } = Guid.NewGuid().ToString();

	public DateTime StartsOn { get; set; } = DateTime.UtcNow;

	public DateTime EndsOn { get; set; } = DateTime.UtcNow;
}
