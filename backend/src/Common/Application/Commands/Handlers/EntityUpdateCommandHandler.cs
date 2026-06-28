using MapsterMapper;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Commands.Handlers;

public abstract class EntityUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity> : EntityCommandHandler<TEntity>, IEntityUpdateCommandHandler<TEntityUpdateCommand, TUpdateModel, TEntity>
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

	public async ValueTask<TEntity> Handle(TEntityUpdateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command, nameof(command));
		ArgumentException.ThrowIfNullOrEmpty(command.ETag, nameof(command.ETag));
		ArgumentException.ThrowIfNullOrEmpty(command.Id, nameof(command.Id));

		var entity = await Repository.FindOne(command.Id, cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, command.Id);
		EntityNotActiveException.ThrowIfNotActive(entity!);
		EntityChangedInBackgroundException.ThrowIfETagMismatch(entity!, command.ETag);

		entity = Mapper.Map(command.UpdateModel, entity!);

		_ = await Repository.Update(entity!, cancellationToken);
		// _ = await Repository.SaveChangesAsync(cancellationToken);

		return entity!;
	}
}
