namespace thc.HotKnobs.Models;

/// <summary>
/// Represents an exception that is thrown when an entity has been changed in the background
/// while an operation was being performed on it. This typically indicates a concurrency conflict.
/// </summary>
public class EntityChangedInBackgroundException : EntityException
{
	/// <inheritdoc />
	public EntityChangedInBackgroundException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityChangedInBackgroundException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Throws an <see cref="EntityChangedInBackgroundException"/> if the ETag of the entity does not match the expected ETag.
	/// This is typically used to detect concurrency conflicts when updating or deleting an entity.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <param name="entity">The entity to check.</param>
	/// <param name="etag">The expected ETag of the entity.</param>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null.</exception>
	/// <exception cref="EntityChangedInBackgroundException">Thrown if the <paramref name="entity"/>.ETag does not match the expected ETag.</exception>
	public static void ThrowIfETagMismatch<TEntity>(TEntity entity, string etag)
		where TEntity : Entity
	{
		ArgumentNullException.ThrowIfNull(entity, nameof(entity));

		if (entity.ETag != etag)
		{
			throw new EntityChangedInBackgroundException($"Entity with Id '{entity.Id}' has been changed in the background. ETag mismatch: expected '{entity.ETag}', but found '{etag}'.");
		}
	}
}
