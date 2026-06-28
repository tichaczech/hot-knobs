using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// List query for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TRepresentation"></typeparam>
public interface IModelListQuery<TRepresentation> : IQuery<ListResponse<TRepresentation>>
	where TRepresentation : class, IModel;
