using System.Diagnostics.CodeAnalysis;

namespace thc.HotKnobs.Models;

/// <summary>
/// Represents an error that occurs when an operation is attempted on an entity that cannot be found.
/// This exception is typically thrown when an entity with a specified identifier does not exist in the data store.
/// </summary>
public class EntityNotFoundException : EntityException
{
	/// <inheritdoc />
	public EntityNotFoundException(string message) : base(message)
	{
	}

	/// <inheritdoc />
	public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Throws <see cref="EntityNotFoundException"/> if <paramref name="entity"/> is null.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="entity"></param>
	/// <param name="id"></param>
	/// <exception cref="EntityNotFoundException"></exception>
	public static void ThrowIfNull<TEntity>(TEntity? entity, string id)
		where TEntity : Entity
	{
		if (entity == null)
			Throw(entity, id);
	}

	[DoesNotReturn]
	internal static void Throw<TEntity>(TEntity? entity, string id)
		where TEntity : Entity
	{
		throw new EntityNotFoundException($"Entity of type '{typeof(TEntity).Name}' with id '{entity?.Id ?? id}' was not found!");
	}
}
