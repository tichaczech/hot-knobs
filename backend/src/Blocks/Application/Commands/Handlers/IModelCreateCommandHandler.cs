using Mediator;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IModelCreateCommand{TModel}"/> command.
/// </summary>
/// <typeparam name="TModelCreateCommand">Type of the create command.</typeparam>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public interface IModelCreateCommandHandler<in TModelCreateCommand, TModel> : ICommandHandler<TModelCreateCommand, TModel>
	where TModelCreateCommand : IModelCreateCommand<TModel>
	where TModel : class, IModel;

/// <summary>
/// Handler for <see cref="IModelCreateCommand{TCreateModel, TModel}"/> command.
/// </summary>
/// <typeparam name="TModelCreateCommand">Type of the create command.</typeparam>
/// <typeparam name="TCreateModel">Type of the create model.</typeparam>
/// <typeparam name="TModel">Type of the resulting model.</typeparam>
public interface IModelCreateCommandHandler<in TModelCreateCommand, TCreateModel, TModel> : IModelCreateCommandHandler<TModelCreateCommand, TModel>, ICommandHandler<TModelCreateCommand, TModel>
	where TModelCreateCommand : IModelCreateCommand<TCreateModel, TModel>
	where TCreateModel : class
	where TModel : class, IModel;
