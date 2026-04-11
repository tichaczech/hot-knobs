using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Domains.Dummy.Model.Entities;

public class Book : Entity
{
	public string AuthorId { get; set; } = Guid.NewGuid().ToString();

	public string ExternalId { get; set; } = default!;

	public double OriginalPrice { get; set; }

	public int PublishYear { get; set; } = DateTimeOffset.Now.Year;

	public DateTimeOffset? PurchasedOn { get; set; }

	public string Name { get; set; } = default!;
}
