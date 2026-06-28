using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Create command for <see cref="IModel"/>.
/// </summary>
/// <typeparam name="TCreateModel"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IModelCreateCommand<TCreateModel, out TModel> : ICommand<TModel>
	where TCreateModel : class
	where TModel : class, IModel
{
	TCreateModel CreateModel { get; set; }
}
