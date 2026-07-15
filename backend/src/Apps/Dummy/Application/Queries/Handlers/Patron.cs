using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries.Handlers;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Queries.Handlers;

public class PatronGetByIdQueryHandler : EntityGetByIdQueryHandler<PatronGetByIdQuery, Patron>
{
	public PatronGetByIdQueryHandler(ILogger<PatronGetByIdQueryHandler> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository) { }
}

public class PatronListQueryHandler<TRepresentation> : EntityListQueryHandler<PatronListQuery, Patron, TRepresentation>
	where TRepresentation : class, IModel
{
	public PatronListQueryHandler(ILogger<PatronListQueryHandler<TRepresentation>> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository) { }
}
