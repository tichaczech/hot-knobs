using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Commands.Handlers;

/// <summary>
/// Handler for <see cref="IEntityCreateCommand{TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityCreateCommand"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityCreateCommandHandler<TEntityCreateCommand, TEntity> : EntityCommandHandler<TEntity>, IEntityCreateCommandHandler<TEntityCreateCommand, TEntity>
	where TEntity : Entity
	where TEntityCreateCommand : IEntityCreateCommand<TEntity>
{
	protected EntityCreateCommandHandler(ILogger<EntityCreateCommandHandler<TEntityCreateCommand, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public abstract ValueTask<TEntity> Handle(TEntityCreateCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Handler for <see cref="IEntityCreateCommand{TCreateModel, TEntity}"/> command.
/// </summary>
/// <typeparam name="TEntityCreateCommand"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityCreateCommandHandler<TEntityCreateCommand, TCreateModel, TEntity> : EntityCreateCommandHandler<TEntityCreateCommand, TEntity>, IEntityCreateCommandHandler<TEntityCreateCommand, TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity
	where TEntityCreateCommand : IEntityCreateCommand<TCreateModel, TEntity>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EntityCreateCommandHandler{TEntityCreateCommand, TCreateModel, TEntity}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityCreateCommandHandler(ILogger<EntityCreateCommandHandler<TEntityCreateCommand, TCreateModel, TEntity>> logger, IMapper mapper, IRepository<TEntity> repository) : base(logger, mapper, repository) { }

	public override async ValueTask<TEntity> Handle(TEntityCreateCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command, nameof(command));

		var entity = Mapper.Map<TEntity>(command.CreateModel);
		if (command.Id != default)
			entity.Id = command.Id;

		_ = await Repository.Create(entity, cancellationToken);

		return entity;
	}
}
