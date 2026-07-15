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
	/// <param name="id">The id of the entity to delete.</param>
	/// <param name="etag">The etag of the entity to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	ValueTask Delete(string id, string etag, CancellationToken cancellationToken = default);

	/// <summary>
	/// Finds all <typeparamref name="TEntity"/> that match the given predicate and projects them to <typeparamref name="TRepresentation"/>.
	/// </summary>
	/// <typeparam name="TRepresentation">The type to project the entities to.</typeparam>
	/// <param name="predicate">The predicate to filter the entities.</param>
	/// <param name="selector">The selector to project the entities to <typeparamref name="TRepresentation"/>.</param>
	/// <param name="paginationToken">The token for pagination.</param>
	/// <param name="synchronizationToken">The token for synchronization.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An asynchronous stream of <typeparamref name="TRepresentation"/>.</returns>
	IAsyncEnumerable<TRepresentation> FindAll<TRepresentation>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TRepresentation>> selector, string? paginationToken = default, string? synchronizationToken = default, CancellationToken cancellationToken = default)
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

	/// <summary>
	/// Updates <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <param name="etag">The etag of the entity to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated entity.</returns>
	ValueTask<TEntity> Update(TEntity entity, string etag, CancellationToken cancellationToken = default);
}
