using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Commands.Handlers;

public class AuthorCreateCommandHandler : EntityCreateCommandHandler<AuthorCreateCommand, AuthorCreateModel, Author>
{
	public AuthorCreateCommandHandler(ILogger<AuthorCreateCommandHandler> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }
}

public class AuthorDeleteCommandHandler : EntityDeleteCommandHandler<AuthorDeleteCommand, Author>
{
	public AuthorDeleteCommandHandler(ILogger<AuthorDeleteCommandHandler> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }
}

public class AuthorUpdateCommandHandler : EntityUpdateCommandHandler<AuthorUpdateCommand, AuthorUpdateModel, Author>
{
	public AuthorUpdateCommandHandler(ILogger<AuthorUpdateCommandHandler> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }
}
