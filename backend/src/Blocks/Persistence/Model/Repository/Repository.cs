using System.Linq.Expressions;

using CommunityToolkit.Diagnostics;

using Microsoft.EntityFrameworkCore;

using thc.HotKnobs.Models;
using thc.HotKnobs.Repositories;
using thc.HotKnobs.Runtime;
using thc.HotKnobs.Runtime.Persistence;
using thc.HotKnobs.Runtime.Security;

namespace thc.HotKnobs.Model.Repository;

#pragma warning disable CA1724 // Type names should not match namespaces
public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
#pragma warning restore CA1724 // Type names should not match namespaces
	where TEntity : Entity
	where TContext : RepositoryContext
{
	private readonly IConcurrencyTokenContext _ctContext;

	protected TContext Context;

	protected IUserProvider UserProvider;

	private bool _disposed;

	protected Repository(TContext context, IConcurrencyTokenContext ctContext, IUserProvider userProvider)
	{
		_ctContext = ctContext;
		Context = context;
		UserProvider = userProvider;
	}

	/// <inheritdoc />
	public virtual IQueryable<TEntity> AsQueryable()
	{
		return Context.Set<TEntity>().AsQueryable();
	}

	/// <inheritdoc />
	public virtual ValueTask<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
	{
		return ValueTask.FromResult(Context.Set<TEntity>().Add(entity).Entity);
	}

	/// <inheritdoc />
	public virtual ValueTask Delete(TEntity entity, CancellationToken cancellationToken = default)
	{
		CheckConcurrencyToken(entity);

		_ = Context.Set<TEntity>().Remove(entity).Entity;

		return ValueTask.CompletedTask;
	}

	public ValueTask Delete(string id, string etag, CancellationToken cancellationToken = default) => throw new NotImplementedException();

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync()
	{
		if (!_disposed)
		{
			await DisposeAsyncCore();
			Dispose(disposing: false);
		}

		GC.SuppressFinalize(this);
	}

	public IAsyncEnumerable<TRepresentation> FindAll<TRepresentation>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TRepresentation>> selector, string? pagingToken = default, string? syncToken = default, CancellationToken cancellationToken = default)
		where TRepresentation : class
	{
		return Context.Set<TEntity>().Where(predicate).Select(selector).AsAsyncEnumerable();
	}

	/// <inheritdoc />
	public virtual async ValueTask<TEntity?> FindOne(string id, CancellationToken cancellationToken = default)
	{
		var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);
		if (entity is not null)
			_ctContext.SetValue(entity.Id, ConcurrencyTokenValueSource.Internal, new ETag(entity.ETag));

		return entity;
	}

	/// <inheritdoc />
	public virtual async ValueTask<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
	{
		var entity = await Context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken);
		if (entity is not null)
			_ctContext.SetValue(entity.Id, ConcurrencyTokenValueSource.Internal, new ETag(entity.ETag));

		return entity;
	}

	// /// <inheritdoc />
	// public virtual async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	// {
	// 	var timestamp = DateTimeOffset.UtcNow;

	// 	Context.ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified).ToList().ForEach(e =>
	// 	{
	// 		if (e.Entity is Entity entity)
	// 		{
	// 			if (e.State == EntityState.Added)
	// 			{
	// 				entity.CreatedAt = timestamp;
	// 				entity.CreatedBy = UserProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("Current user is not set!");
	// 			}

	// 			entity.ETag = Guid.NewGuid().ToString("D");
	// 			entity.UpdatedAt = timestamp;
	// 			entity.UpdatedBy = UserProvider.GetCurrentUser()?.Identity?.Name ?? throw new InvalidOperationException("Current user is not set!");
	// 		}
	// 	});

	// 	var count = await Context.SaveChangesAsync(cancellationToken);
	// 	await SyncConcurrencyTokens(cancellationToken);

	// 	return count;
	// }

	/// <inheritdoc />
	public virtual ValueTask<TEntity> Update(TEntity entity, string etag, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(entity);
		Guard.IsNotNullOrEmpty(etag);

		CheckConcurrencyToken(entity);

		return ValueTask.FromResult(Context.Set<TEntity>().Update(entity).Entity);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				Context.Dispose();
				Context = null!;
			}

			_disposed = true;
		}
	}

	protected virtual async ValueTask DisposeAsyncCore()
	{
		await Context.DisposeAsync().ConfigureAwait(false);
	}

	protected virtual Task SyncConcurrencyTokens(CancellationToken cancellationToken = default)
	{
		var entries = Context.ChangeTracker.Entries<TEntity>().ToList();
		foreach (var entry in entries) // TODO: Make parallel
		{
			// await entry.ReloadAsync(cancellationToken);
			var entity = entry.Entity;

			_ctContext.SetValue(entity.Id, ConcurrencyTokenValueSource.Internal, new ETag(entity.ETag));
		}

		return Task.CompletedTask;
	}

	private void CheckConcurrencyToken(TEntity entity)
	{
		ArgumentNullException.ThrowIfNull(entity);

		if (!_ctContext.TryGetValue(entity.Id, ConcurrencyTokenValueSource.External, out var token))
			throw new InvalidOperationException($"ETag has to be set for both Update/Delete operation!");

		bool changedInBackground;
		switch (token)
		{
			case ETag etag:
				changedInBackground = entity.ETag != etag.Value;
				entity.ETag = etag.Value; // Update the entity's ETag to the external value.
				break;
			default:
				throw new InvalidOperationException($"Unsupported Concurrency Token type: {token.GetType().Name}. Expected ETag, LastModified is not supported.");
		}

		if (changedInBackground)
			throw new EntityChangedInBackgroundException($"Entity with Id: '{entity.Id}' has been changed in the background!");
	}
}
