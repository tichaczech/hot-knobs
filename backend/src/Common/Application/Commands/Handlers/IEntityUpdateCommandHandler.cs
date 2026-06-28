using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IEntityUpdateCommand{TUpdateModel, TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityUpdateCommand"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityUpdateCommandHandler<in TEntityUpdateCommand, TUpdateModel, TEntity> : IModelUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity>
	where TEntityUpdateCommand : IEntityUpdateCommand<TUpdateModel, TEntity>
	where TUpdateModel : class
	where TEntity : Entity;
