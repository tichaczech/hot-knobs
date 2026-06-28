using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record AuthorGetByIdQuery : EntityGetByIdQuery<Author>;

public record AuthorGetByExternalIdQuery : EntityGetQuery<Author>
{
	public required string ExternalId { get; set; }
}

public record AuthorListQuery<TRepresentation> : EntityListQuery<Author, TRepresentation>
	where TRepresentation : class, IModel
{
	public AuthorListQuery()
	{
		FilteringEnabled = false;
	}
}
