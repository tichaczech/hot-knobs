using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries.Handlers;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Queries.Handlers;

public class BookGetByIdQueryHandler : EntityGetByIdQueryHandler<BookGetByIdQuery, Book>
{
	public BookGetByIdQueryHandler(ILogger<BookGetByIdQueryHandler> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }
}

public class BookGetByExternalIdQueryHandler : EntityQueryHandler<Book>, IEntityGetQueryHandler<BookGetByExternalIdQuery, Book>
{
	public BookGetByExternalIdQueryHandler(ILogger<BookGetByExternalIdQueryHandler> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }

	public async ValueTask<Book> Handle(BookGetByExternalIdQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query, nameof(query));

		var book = await Repository.FindOne(book => book.ExternalId == query.ExternalId, cancellationToken);
		EntityNotFoundException.ThrowIfNull(book, query.ExternalId);
		EntityNotActiveException.ThrowIfNotTrue(book!, b => query.OnlyActive && b.IsActive || !query.OnlyActive);

		return book!;
	}
}

public class BookListQueryHandler<TRepresentation> : EntityListQueryHandler<BookListQuery, Book, TRepresentation>
	where TRepresentation : class, IModel
{
	public BookListQueryHandler(ILogger<BookListQueryHandler<TRepresentation>> logger, IMapper mapper, IRepository<Book> repository) : base(logger, mapper, repository) { }
}
