using System.ComponentModel.DataAnnotations;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record AuthorGetByIdQuery : EntityGetByIdQuery<Author>;

public record AuthorGetByExternalIdQuery : EntityGetQuery<Author>
{

	[Required]
	public string ExternalId { get; set; } = default!;
}

public record AuthorListQuery : EntityListQuery<Author>;
