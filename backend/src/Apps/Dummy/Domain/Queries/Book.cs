using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record BookGetByIdQuery : EntityGetByIdQuery<Book>;

public record BookGetByExternalIdQuery : EntityGetQuery<Book>
{
	public required string ExternalId { get; set; }
}

public record BookListQuery<TRepresentation> : EntityListQuery<Book, TRepresentation>
	where TRepresentation : class, IModel
{
	public BookListQuery()
	{
		FilteringEnabled = false;
	}
}
