using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record PatronGetByIdQuery : EntityGetByIdQuery<Patron>;

public record PatronListQuery<TRepresentation> : EntityListQuery<Patron, TRepresentation>
	where TRepresentation : class, IModel
{
	public PatronListQuery()
	{
		FilteringEnabled = false;
	}
}
