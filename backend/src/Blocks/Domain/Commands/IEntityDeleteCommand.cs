using thc.HotKnobs.Models;

namespace thc.HotKnobs.Commands;

/// <summary>
/// Delete command for <see cref="Entity"/>.
/// </summary>
public interface IEntityDeleteCommand : IModelDeleteCommand
{
	/// <summary>
	/// Gets the Id of the entity to be deleted.
	/// </summary>
	string Id { get; init; }

	/// <summary>
	/// Gets the ETag of the entity to be deleted.
	/// </summary>
	/// <remarks>
	/// This is used for concurrency control to ensure that the entity has not been modified since it was last retrieved.
	/// </remarks>
	string ETag { get; init; }
}
