using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public interface IModelUpdateCommand<out TModel> : ICommand<TModel>
	where TModel : class, IModel;

/// <summary>
/// Update command for <see cref="IModel"/> with a specific update model.
/// </summary>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
/// <typeparam name="TUpdateModel">Type of the update model.</typeparam>
public interface IModelUpdateCommand<TUpdateModel, out TModel> : IModelUpdateCommand<TModel>
	where TModel : class, IModel
	where TUpdateModel : class
{
	/// <summary>
	/// Gets the update model used to update the resulting model.
	/// </summary>
	TUpdateModel UpdateModel { get; init; }
}
