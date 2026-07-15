using System.Linq.Expressions;

using Fand.Runtime;
using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Queries.Handlers;

public abstract class EntityListQueryHandler<TEntityListQuery, TEntity, TRepresentation> : EntityQueryHandler<TEntity>, IEntityListQueryHandler<TEntityListQuery, TEntity>
	where TEntityListQuery : IEntityListQuery<TEntity>
	where TEntity : Entity
	where TRepresentation : class, IModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityListQueryHandler{TEntityListQuery, TEntity, TRepresentation}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityListQueryHandler(ILogger<EntityListQueryHandler<TEntityListQuery, TEntity, TRepresentation>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public async ValueTask<ListResponse> Handle(TEntityListQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query, nameof(query));
		FilteringDisabledException.ThrowIfFilterSpecified(query);

		Expression<Func<TEntity, bool>>? where = (entity => true);
		if (query.Filter is not null)
			where = where.AndAlso(query.Filter);

		if (query.OnlyActive)
#pragma warning disable IDE0100 // Remove redundant equality
			// NOTE: This is a workaround for EF Core for Cosmos bug where it does not translate boolean expression `IsActive` to SQL correctly.
			where = where.AndAlso(entity => entity.IsActive == true);
#pragma warning restore IDE0100 // Remove redundant equality

		Expression<Func<TEntity, dynamic>> selector = entity => Mapper.Map<TRepresentation>(entity);
		if (query.Selector != default)
			selector = query.Selector;

		var items = Repository.FindAll(where, selector, query.PaginationToken, query.SynchronizationToken, cancellationToken);
		return new ListResponse(items, "new-pagination-token-here", "new-sync-token-here");

		// 		ArgumentOutOfRangeException.ThrowIfNegative(skip.GetValueOrDefault(), nameof(skip));
		// 		ArgumentOutOfRangeException.ThrowIfNegative(limit.GetValueOrDefault(), nameof(limit));

		// 		if (skip.HasValue)
		// 			query = query.Skip(skip.Value);
		// 		if (limit.HasValue)
		// 			query = query.Take(limit.Value);
	}
}
