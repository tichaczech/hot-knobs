using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Domains.Dummy.Queries;

public record PatronGetByIdQuery : EntityGetByIdQuery<Patron>;

public record PatronListQuery : EntityListQuery<Patron>;
