using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IModelUpdateCommand{TUpdateModel, TModel}"/> command.
/// </summary>
/// <typeparam name="TModelUpdateCommand"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IModelUpdateCommandHandler<in TModelUpdateCommand, TUpdateModel, TModel> : ICommandHandler<TModelUpdateCommand, TModel>
	where TModelUpdateCommand : IModelUpdateCommand<TUpdateModel, TModel>
	where TUpdateModel : class
	where TModel : class, IModel;
