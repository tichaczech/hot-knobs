using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

public abstract record EntityCreateCommand<TCreateModel, TEntity> : ModelCreateCommand<TCreateModel, TEntity>, IEntityCreateCommand<TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity
{
	public string? Id { get; set; }
}
