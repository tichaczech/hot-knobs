using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IModelUpdateCommand{TModel}"/> command.
/// </summary>
/// <typeparam name="TModelUpdateCommand">Type of the update command.</typeparam>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public interface IModelUpdateCommandHandler<in TModelUpdateCommand, TModel> : ICommandHandler<TModelUpdateCommand, TModel>
	where TModelUpdateCommand : IModelUpdateCommand<TModel>
	where TModel : class, IModel;

/// <summary>
/// Handler for <see cref="IModelUpdateCommand{TUpdateModel, TModel}"/> command.
/// </summary>
/// <typeparam name="TModelUpdateCommand">Type of the update command.</typeparam>
/// <typeparam name="TUpdateModel">Type of the update model.</typeparam>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public interface IModelUpdateCommandHandler<in TModelUpdateCommand, TUpdateModel, TModel> : IModelUpdateCommandHandler<TModelUpdateCommand, TModel>, ICommandHandler<TModelUpdateCommand, TModel>
	where TModelUpdateCommand : IModelUpdateCommand<TUpdateModel, TModel>
	where TUpdateModel : class
	where TModel : class, IModel;
