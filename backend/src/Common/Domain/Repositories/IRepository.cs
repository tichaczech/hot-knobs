using System.Linq.Expressions;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Repositories;

/// <summary>
/// Generic entity repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IDisposable, IAsyncDisposable
	where TEntity : Entity
{
	// /// <summary>
	// /// Returns an <see cref="IQueryable{TEntity}"/> for querying entities of type <typeparamref name="TEntity"/>.
	// /// </summary>
	// /// <remarks>
	// /// Methods for data manipulation (e.g. IQueryable{T}.Append{T}) are considered as unsafe and should not be used.
	// /// </remarks>
	// /// <returns>An <see cref="IQueryable{TEntity}"/> for querying entities of type <typeparamref name="TEntity"/>.</returns>
	// IQueryable<TEntity> AsQueryable();

	/// <summary>
	/// Creates a new <typeparamref name="TEntity"/> in the repository.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the created entity.</returns>
	ValueTask<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	ValueTask Delete(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all <typeparamref name="TEntity"/> that match the given predicate and projects them to <typeparamref name="TRepresentation"/>.
	/// </summary>
	/// <typeparam name="TRepresentation">The type to project the entities to.</typeparam>
	/// <param name="predicate">The predicate to filter the entities.</param>
	/// <param name="selector">The selector to project the entities to <typeparamref name="TRepresentation"/>.</param>
	/// <param name="pagingToken">The token for pagination.</param>
	/// <param name="syncToken">The token for synchronization.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An asynchronous stream of <typeparamref name="TRepresentation"/>.</returns>
	IAsyncEnumerable<TRepresentation> FindAll<TRepresentation>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TRepresentation>> selector, string? pagingToken = default, string? syncToken = default, CancellationToken cancellationToken = default)
		where TRepresentation : class;

	/// <summary>
	/// Gets <typeparamref name="TEntity"/> by id.
	/// </summary>
	/// <param name="id">The id of the entity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The entity if found; otherwise, null.</returns>
	ValueTask<TEntity?> FindOne(string id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets <typeparamref name="TEntity"/> by predicate.
	/// </summary>
	/// <param name="predicate">The predicate to filter the entities.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The entity if found; otherwise, null.</returns>
	ValueTask<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

	// /// <summary>
	// /// Saves all changes made in this context to the underlying database asynchronously.
	// /// </summary>
	// /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
	// /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
	// Task<int> SaveChanges(CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated entity.</returns>
	ValueTask<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
}
