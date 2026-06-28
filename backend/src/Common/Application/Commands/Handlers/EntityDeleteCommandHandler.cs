using MapsterMapper;

using Mediator;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Commands.Handlers;

public abstract class EntityDeleteCommandHandler<TEntityDeleteCommand, TEntity> : EntityCommandHandler<TEntity>, IEntityDeleteCommandHandler<TEntityDeleteCommand>
	where TEntityDeleteCommand : IEntityDeleteCommand
	where TEntity : Entity
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityDeleteCommandHandler{TEntityDeleteCommand, TEntity}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityDeleteCommandHandler(ILogger<EntityDeleteCommandHandler<TEntityDeleteCommand, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public async ValueTask<Unit> Handle(TEntityDeleteCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command, nameof(command));
		ArgumentNullException.ThrowIfNull(command.Id, nameof(command.Id));
		ArgumentException.ThrowIfNullOrEmpty(command.ETag, nameof(command.ETag));

		var entity = await Repository.FindOne(command.Id, cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, command.Id);
		EntityNotActiveException.ThrowIfNotActive(entity!);
		EntityChangedInBackgroundException.ThrowIfETagMismatch(entity!, command.ETag);

		entity!.IsActive = false;

		_ = await Repository.Update(entity, cancellationToken);
		// _ = await Repository.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
