using System.Linq.Expressions;

using thc.HotKnobs.Model.Entities;

namespace thc.HotKnobs.Model.Repository;

/// <summary>
/// Generic entity repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IDisposable, IAsyncDisposable
	where TEntity : Entity
{
	/// <summary>
	/// Returns an <see cref="IQueryable{TEntity}"/> for querying entities of type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <remarks>
	/// Methods for data manipulation (e.g. IQueryable{T}.Append{T}) are considered as unsafe and should not be used.
	/// </remarks>
	/// <returns>An <see cref="IQueryable{TEntity}"/> for querying entities of type <typeparamref name="TEntity"/>.</returns>
	IQueryable<TEntity> AsQueryable();

	/// <summary>
	/// Creates a new <typeparamref name="TEntity"/> in the repository.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the created entity.</returns>
	Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets <typeparamref name="TEntity"/> by id.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TEntity?> FindAsync(string id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets <typeparamref name="TEntity"/> by predicate.
	/// </summary>
	/// <param name="predicate"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

	/// <summary>
	/// Saves all changes made in this context to the underlying database asynchronously.
	/// </summary>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
