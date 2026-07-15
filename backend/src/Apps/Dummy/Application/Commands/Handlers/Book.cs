using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Commands.Handlers;
using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Commands.Handlers;

public class BookCreateCommandHandler : EntityCreateCommandHandler<BookCreateCommand, BookCreateModel, Book>
{
	public BookCreateCommandHandler(ILogger<BookCreateCommandHandler> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }
}

public class BookDeleteCommandHandler : EntityDeleteCommandHandler<BookDeleteCommand, Book>
{
	public BookDeleteCommandHandler(ILogger<BookDeleteCommandHandler> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }
}

public class BookUpdateCommandHandler : EntityUpdateCommandHandler<BookUpdateCommand, BookUpdateModel, Book>
{
	public BookUpdateCommandHandler(ILogger<BookUpdateCommandHandler> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }
}
