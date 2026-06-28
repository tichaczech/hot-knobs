using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IEntityListQuery{TEntity, TRepresentation}"/> query.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityListQuery"></typeparam>
/// <typeparam name="TRepresentation"></typeparam>
public interface IEntityListQueryHandler<in TEntityListQuery, TEntity, TRepresentation> : IModelListQueryHandler<TEntityListQuery, TRepresentation>
	where TEntityListQuery : IEntityListQuery<TEntity, TRepresentation>
	where TRepresentation : class, IModel
	where TEntity : Entity;
