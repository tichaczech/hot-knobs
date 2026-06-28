using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Create command for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
public interface IEntityCreateCommand<TCreateModel, out TEntity> : IModelCreateCommand<TCreateModel, TEntity>
	where TCreateModel : class
	where TEntity : Entity
{
	/// <summary>
	/// Gets or sets the Id of the entity to be created. If null, a new Id will be generated.
	/// </summary>
	string? Id { get; set; }
}
