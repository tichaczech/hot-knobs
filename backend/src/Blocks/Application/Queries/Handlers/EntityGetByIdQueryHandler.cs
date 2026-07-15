using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IEntityGetByIdQuery{TEntity}"/> query.
/// </summary>
/// <typeparam name="TEntityGetQuery"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityGetByIdQueryHandler<TEntityGetQuery, TEntity> : EntityQueryHandler<TEntity>, IEntityGetQueryHandler<TEntityGetQuery, TEntity>
	where TEntityGetQuery : IEntityGetByIdQuery<TEntity>
	where TEntity : Entity
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityGetByIdQueryHandler{TEntityGetQuery, TEntity}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityGetByIdQueryHandler(ILogger<EntityGetByIdQueryHandler<TEntityGetQuery, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public async ValueTask<TEntity> Handle(TEntityGetQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query, nameof(query));
		ArgumentException.ThrowIfNullOrEmpty(query.Id, nameof(query.Id));

		var entity = await Repository.FindOne(query.Id, cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, query.Id);
		EntityNotActiveException.ThrowIfNotTrue(entity!, a => query.OnlyActive && a.IsActive || !query.OnlyActive);

		return entity!;
	}
}
