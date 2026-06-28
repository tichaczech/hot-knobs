using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IEntityGetQuery{TEntity}"/> query.
/// </summary>
/// <typeparam name="TEntityGetQuery"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityGetQueryHandler<in TEntityGetQuery, TEntity> : IModelGetQueryHandler<TEntityGetQuery, TEntity>
	where TEntityGetQuery : IEntityGetQuery<TEntity>
	where TEntity : Entity;
