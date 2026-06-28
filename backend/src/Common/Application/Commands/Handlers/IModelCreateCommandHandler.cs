using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IModelCreateCommand{TCreateModel, TModel}"/> command.
/// </summary>
/// <typeparam name="TModelCreateCommand"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IModelCreateCommandHandler<in TModelCreateCommand, TCreateModel, TModel> : ICommandHandler<TModelCreateCommand, TModel>
	where TModelCreateCommand : IModelCreateCommand<TCreateModel, TModel>
	where TCreateModel : class
	where TModel : class, IModel;
