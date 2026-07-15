using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Commands.Handlers;

public class PatronCreateCommandHandler : EntityCreateCommandHandler<PatronCreateCommand, PatronCreateModel, Patron>
{
	public PatronCreateCommandHandler(ILogger<PatronCreateCommandHandler> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository) { }
}

public class PatronDeleteCommandHandler : EntityDeleteCommandHandler<PatronDeleteCommand, Patron>
{
	public PatronDeleteCommandHandler(ILogger<PatronDeleteCommandHandler> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository) { }
}

public class PatronUpdateCommandHandler : EntityUpdateCommandHandler<PatronUpdateCommand, PatronUpdateModel, Patron>
{
	public PatronUpdateCommandHandler(ILogger<PatronUpdateCommandHandler> logger, IMapper mapper, IRepository<Patron> repository) : base(logger, mapper, repository) { }
}
