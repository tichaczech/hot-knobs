using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IModelGetQuery{TModel}"/> query.
/// </summary>
/// <typeparam name="TGetModelQuery"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IModelGetQueryHandler<in TGetModelQuery, TModel> : IQueryHandler<TGetModelQuery, TModel>
	where TGetModelQuery : IModelGetQuery<TModel>
	where TModel : class, IModel;
