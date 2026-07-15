using Fand.Runtime.Mapping;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;

namespace thc.HotKnobs.Commands.Handlers;

public abstract class EntityCommandHandler<TEntity>
	where TEntity : Entity
{
	protected readonly ILogger Logger;

	protected readonly IMapper Mapper;

	protected readonly IRepository<TEntity> Repository;

	/// <summary>
	/// Initializes a new instance of the <see cref="EntityCommandHandler{TEntity}"/> class.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="mapper"></param>
	/// <param name="repository"></param>
	protected EntityCommandHandler(ILogger<EntityCommandHandler<TEntity>> logger, IMapper mapper, IRepository<TEntity> repository)
	{
		Logger = logger;
		Mapper = mapper;
		Repository = repository;
	}
}
