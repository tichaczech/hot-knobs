using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IEntityListQuery{TEntity}"/> query.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityListQuery"></typeparam>
public interface IEntityListQueryHandler<in TEntityListQuery, TEntity> : IModelListQueryHandler<TEntityListQuery, TEntity>
	where TEntityListQuery : IEntityListQuery<TEntity>
	where TEntity : Entity;
