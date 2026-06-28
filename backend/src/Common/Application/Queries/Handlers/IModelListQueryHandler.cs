using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries.Handlers;

/// <summary>
/// Handler for <see cref="IModelListQuery{TModel}"/> query.
/// </summary>
/// <typeparam name="TModelListCommand"></typeparam>
/// <typeparam name="TRepresentation"></typeparam>
public interface IModelListQueryHandler<in TModelListCommand, TRepresentation> : IQueryHandler<TModelListCommand, ListResponse<TRepresentation>>
	where TModelListCommand : IModelListQuery<TRepresentation>
	where TRepresentation : class, IModel;
