using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IEntityCreateCommand{TCreateModel, TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityCreateCommand"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityCreateCommandHandler<in TEntityCreateCommand, TEntity> : IModelCreateCommandHandler<TEntityCreateCommand, TEntity>
	where TEntityCreateCommand : IEntityCreateCommand<TEntity>
	where TEntity : Entity;

/// <summary>
/// Handler for <see cref="IEntityCreateCommand{TCreateModel, TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityCreateCommand"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityCreateCommandHandler<in TEntityCreateCommand, TCreateModel, TEntity> : IEntityCreateCommandHandler<TEntityCreateCommand, TEntity>, IModelCreateCommandHandler<TEntityCreateCommand, TCreateModel, TEntity>
	where TEntityCreateCommand : IEntityCreateCommand<TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity;
