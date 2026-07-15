using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// List query for <see cref="IModel"/>.
/// </summary>
public interface IModelListQuery<TModel> : IQuery<ListResponse>
	where TModel : class, IModel;
