using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IEntityGetByIdQuery{TEntity}"/> query.
/// </summary>
/// <typeparam name="TEntityGetQuery"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityGetByIdQueryHandler<in TEntityGetQuery, TEntity> : IEntityGetQueryHandler<TEntityGetQuery, TEntity>
	where TEntityGetQuery : IEntityGetByIdQuery<TEntity>
	where TEntity : Entity;
