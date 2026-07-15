using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries.Handlers;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Queries.Handlers;

public class LoanGetByIdQueryHandler : EntityGetByIdQueryHandler<LoanGetByIdQuery, Loan>
{
	public LoanGetByIdQueryHandler(ILogger<LoanGetByIdQueryHandler> logger, IMapper mapper, IRepository<Loan> repository) : base(logger, mapper, repository) { }
}

public class LoanListQueryHandler<TRepresentation> : EntityListQueryHandler<LoanListQuery, Loan, TRepresentation>
	where TRepresentation : class, IModel
{
	public LoanListQueryHandler(ILogger<LoanListQueryHandler<TRepresentation>> logger, IMapper mapper, IRepository<Loan> repository) : base(logger, mapper, repository) { }
}
