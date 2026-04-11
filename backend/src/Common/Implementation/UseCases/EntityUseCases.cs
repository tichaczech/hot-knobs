using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using AutoMapper;

using Fand.Runtime;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Entities;
using thc.HotKnobs.Model.Repository;

namespace thc.HotKnobs.UseCases;

/// <summary>
/// Base service with underlining repository.
/// </summary>
/// <typeparam name="TEntity">Type of Entity which is being managed by the service.</typeparam>
/// <typeparam name="TCreateModel">Type of Model which is used for creating new entities.</typeparam>
/// <typeparam name="TUpdateModel">Type of Model which is used for updating existing entities.</typeparam>
public abstract class EntityUseCases<TEntity, TCreateModel, TUpdateModel> : IEntityUseCases<TEntity, TCreateModel, TUpdateModel>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
{
	private readonly ILogger _logger;

	/// <summary>
	/// Instance of <see cref="Mapper"/>, which can be used in derived classes.
	/// </summary>
	protected readonly IMapper Mapper;

	/// <summary>
	/// Instance of <see cref="IRepository{T}"/>, which can be used in derived classes.
	/// </summary>
	protected readonly IRepository<TEntity> Repository;

	protected EntityUseCases(ILogger<EntityUseCases<TEntity, TCreateModel, TUpdateModel>> logger, IMapper mapper, IRepository<TEntity> repository)
	{
		_logger = logger;
		Mapper = mapper;
		Repository = repository;
	}

	/// <inheritdoc />
	public virtual async Task<TEntity> CreateAsync(string? id, TCreateModel model, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(model, nameof(model));

		await ValidateAsync(model, cancellationToken);

		var entity = Mapper.Map<TEntity>(model);
		if (id != default)
			entity.Id = id;

		_ = await Repository.CreateAsync(entity, cancellationToken);
		_ = await Repository.SaveChangesAsync(cancellationToken);

		return entity;
	}

	/// <inheritdoc />
	public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

		var entity = await GetAsync(id, true, cancellationToken);

		entity!.IsActive = false;

		_ = await Repository.UpdateAsync(entity!, cancellationToken);
		_ = await Repository.SaveChangesAsync(cancellationToken);
	}

	/// <inheritdoc />
	public virtual async Task<TEntity> GetAsync(string id, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

		var entity = await Repository.FindAsync(id, cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, id);
		EntityNotActiveException.ThrowIfNotTrue(entity!, a => onlyActive && a.IsActive || onlyActive);

		return entity!;
	}

	/// <inheritdoc />
	public virtual IAsyncEnumerable<string> ListAsync(DateTimeOffset? modifiedSince = null, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return ListAsync(default, default, modifiedSince, onlyActive, null, null, cancellationToken);
	}

	/// <inheritdoc />
	public virtual async Task<TEntity> UpdateAsync(string id, TUpdateModel model, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
		ArgumentNullException.ThrowIfNull(model, nameof(model));

		await ValidateAsync(model, cancellationToken);

		var entity = await GetAsync(id, true, cancellationToken);

		entity = Mapper.Map(model, entity);

		_ = await Repository.UpdateAsync(entity!, cancellationToken);
		_ = await Repository.SaveChangesAsync(cancellationToken);

		return entity!;
	}

	/// <summary>
	/// Basic list operation.
	/// </summary>
	protected async IAsyncEnumerable<string> ListAsync(Expression<Func<TEntity, bool>>? where = default, Expression<Func<TEntity, string>>? selector = default, DateTimeOffset? modifiedSince = null, bool onlyActive = true, int? skip = null, int? limit = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(skip.GetValueOrDefault(), nameof(skip));
		ArgumentOutOfRangeException.ThrowIfNegative(limit.GetValueOrDefault(), nameof(limit));

		where ??= (entity => true);
		if (modifiedSince.HasValue)
			where = where.AndAlso(entity => entity.UpdatedAt > modifiedSince.Value);

		if (onlyActive)
#pragma warning disable IDE0100 // Remove redundant equality
			// NOTE: This is a workaround for EF Core for Cosmos bug where it does not translate boolean expression `IsActive` to SQL correctly.
			where = where.AndAlso(entity => entity.IsActive == true);
#pragma warning restore IDE0100 // Remove redundant equality

		var query = Repository.AsQueryable().Where(where);
		if (skip.HasValue)
			query = query.Skip(skip.Value);
		if (limit.HasValue)
			query = query.Take(limit.Value);

		if (selector == default)
			selector = entity => entity.Id;

		await foreach (var item in query.Select(selector).AsAsyncEnumerable().WithCancellation(cancellationToken))
			yield return item;
	}

	/// <summary>
	/// Validates create request.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected abstract Task ValidateAsync(TCreateModel model, CancellationToken cancellationToken = default);

	/// <summary>
	/// Validates update request.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected abstract Task ValidateAsync(TUpdateModel model, CancellationToken cancellationToken = default);
}
