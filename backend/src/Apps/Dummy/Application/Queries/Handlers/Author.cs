using MapsterMapper;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Domains.Dummy.Models;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries.Handlers;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Domains.Dummy.Queries.Handlers;

public class AuthorGetByIdQueryHandler : EntityGetByIdQueryHandler<AuthorGetByIdQuery, Author>
{
	public AuthorGetByIdQueryHandler(ILogger<AuthorGetByIdQueryHandler> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }
}

public class AuthorGetByExternalIdQueryHandler : EntityQueryHandler<Author>, IEntityGetQueryHandler<AuthorGetByExternalIdQuery, Author>
{
	public AuthorGetByExternalIdQueryHandler(ILogger<AuthorGetByExternalIdQueryHandler> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }

	public async ValueTask<Author> Handle(AuthorGetByExternalIdQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query, nameof(query));

		var author = await Repository.FindOne(author => author.ExternalId == query.ExternalId, cancellationToken);
		EntityNotFoundException.ThrowIfNull(author, query.ExternalId);
		EntityNotActiveException.ThrowIfNotTrue(author!, a => query.OnlyActive && a.IsActive || !query.OnlyActive);

		return author!;
	}
}

public class AuthorListQueryHandler<TRepresentation> : EntityListQueryHandler<AuthorListQuery<TRepresentation>, Author, TRepresentation>
	where TRepresentation : class, IModel
{
	public AuthorListQueryHandler(ILogger<AuthorListQueryHandler<TRepresentation>> logger, IMapper mapper, IRepository<Author> repository) : base(logger, mapper, repository) { }
}
