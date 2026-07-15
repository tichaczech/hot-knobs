using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IEntityUpdateCommand{TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityUpdateCommand"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityUpdateCommandHandler<TEntityUpdateCommand, TEntity> : EntityCommandHandler<TEntity>, IEntityUpdateCommandHandler<TEntityUpdateCommand, TEntity>
	where TEntity : Entity
	where TEntityUpdateCommand : IEntityUpdateCommand<TEntity>
{
	protected EntityUpdateCommandHandler(ILogger<EntityUpdateCommandHandler<TEntityUpdateCommand, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public abstract ValueTask<TEntity> Handle(TEntityUpdateCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Handler for <see cref="IEntityUpdateCommand{TUpdateModel, TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityUpdateCommand"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity> : EntityUpdateCommandHandler<TEntityUpdateCommand, TEntity>, IEntityUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity>
	where TEntityUpdateCommand : IEntityUpdateCommand<TUpdateModel, TEntity>
	where TUpdateModel : class
	where TEntity : Entity
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityUpdateCommandHandler{TEntityUpdateCommand, TUpdateModel, TEntity}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityUpdateCommandHandler(ILogger<EntityUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public override async ValueTask<TEntity> Handle(TEntityUpdateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command, nameof(command));
		ArgumentException.ThrowIfNullOrEmpty(command.ETag, nameof(command.ETag));
		ArgumentException.ThrowIfNullOrEmpty(command.Id, nameof(command.Id));

		var entity = await Repository.FindOne(command.Id, cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, command.Id);
		EntityNotActiveException.ThrowIfNotActive(entity!);
		EntityChangedInBackgroundException.ThrowIfETagMismatch(entity!, command.ETag);

		entity = Mapper.Map(command.UpdateModel, entity!);

		_ = await Repository.Update(entity, command.ETag, cancellationToken);

		return entity!;
	}
}
