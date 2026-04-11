using System.Diagnostics.CodeAnalysis;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Model;

/// <summary>
/// Represents an error that occurs when an operation is attempted on an entity that is not found or is not active.
/// This exception is typically thrown when attempting to retrieve or manipulate an entity that does not exist in the data store
/// or has been logically deleted or deactivated.
/// </summary>
public class EntityNotFoundOrNotActiveException : EntityException
{
	/// <inheritdoc />
	public EntityNotFoundOrNotActiveException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityNotFoundOrNotActiveException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Throws an <see cref="EntityNotFoundOrNotActiveException"/> if entity is null or not active.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="entity"></param>
	/// <param name="id"></param>
	/// <exception cref="EntityNotFoundOrNotActiveException"></exception>
	public static void ThrowIfNullOrNotActive<TEntity>(TEntity? entity, string id)
		where TEntity : Entity
	{
		if (entity == null || !entity.IsActive)
			Throw(entity, id);
	}

	[DoesNotReturn]
	public static void Throw<TEntity>(TEntity? entity, string id)
		where TEntity : Entity
	{
		throw new EntityNotFoundOrNotActiveException($"Entity of type '{typeof(TEntity).Name}' with id '{entity?.Id ?? id}' was not found or has been already deleted!");
	}
}
