using System.Diagnostics.CodeAnalysis;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Model;

/// <summary>
/// Represents an error that occurs when an operation is attempted on an entity that is not active.
/// This exception is typically thrown when attempting to retrieve or manipulate an entity that
/// has been logically deleted or deactivated.
/// </summary>
public class EntityNotActiveException : EntityException
{
	/// <inheritdoc />
	public EntityNotActiveException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityNotActiveException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Throws an <see cref="EntityNotActiveException"/> if the specified entity's <see cref="Entity.IsActive"/> property is false.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <param name="entity">The entity to check for active status.</param>
	/// <exception cref="EntityNotActiveException">Thrown if <paramref name="entity"/>.IsActive is false.</exception>
	public static void ThrowIfNotActive<TEntity>(TEntity entity)
		where TEntity : Entity
	{
		if (entity is null || entity.IsActive)
			return;

		Throw(entity);
	}

	/// <summary>
	/// Throws an <see cref="EntityNotActiveException"/> if the specified boolean expression evaluates to false for the given entity.
	/// This is typically used to assert a condition that implies the entity should be active or usable.
	/// The exception message will indicate that the entity has been deleted.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <param name="entity">The entity instance to evaluate the expression against.</param>
	/// <param name="expression">A function that takes the entity and returns a boolean. If this function returns false, an exception is thrown.</param>
	/// <exception cref="EntityNotActiveException">Thrown if the <paramref name="expression"/> returns false for the given <paramref name="entity"/>.</exception>
	public static void ThrowIfNotTrue<TEntity>(TEntity entity, Func<TEntity, bool> expression)
		where TEntity : Entity
	{
		if (entity is null || expression is null || expression(entity))
			return;

		Throw(entity);
	}

	[DoesNotReturn]
	internal static void Throw<TEntity>(TEntity entity)
		where TEntity : Entity
	{
		throw new EntityNotActiveException($"Entity of type '{typeof(TEntity).Name}' with id '{entity.Id}' has been already deleted!");
	}
}
