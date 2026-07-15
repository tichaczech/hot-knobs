using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IModelListQuery{TModel}"/> query.
/// </summary>
/// <typeparam name="TModelListCommand"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IModelListQueryHandler<in TModelListCommand, TModel> : IQueryHandler<TModelListCommand, ListResponse>
	where TModelListCommand : IModelListQuery<TModel>
	where TModel : class, IModel;
