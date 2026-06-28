using thc.HotKnobs.Models;

namespace thc.HotKnobs.Domains.Dummy.Models;

public record Loan : Entity
{
	public string BookId { get; set; } = Guid.NewGuid().ToString();

	public DateTime DueOn { get; set; } = DateTime.UtcNow.AddDays(14);

	public DateTime LoanedOn { get; set; } = DateTime.UtcNow;

	public string PatronId { get; set; } = Guid.NewGuid().ToString();

	public string? ReservationId { get; set; }

	public DateTime? ReturnedOn { get; set; }
}
