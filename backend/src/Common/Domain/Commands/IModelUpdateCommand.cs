using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Update command for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
public interface IModelUpdateCommand<TUpdateModel, out TModel> : ICommand<TModel>
	where TModel : class, IModel
	where TUpdateModel : class
{
	TUpdateModel UpdateModel { get; set; }
}
