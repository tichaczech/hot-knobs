using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// Get query for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IModelGetQuery<out TModel> : IQuery<TModel>
	where TModel : class, IModel;
