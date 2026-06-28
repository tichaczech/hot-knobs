using System.Linq.Expressions;

using AutoMapper;

using Fand.Runtime;

using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Repository;
using thc.HotKnobs.Shared.Collections.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Collections.Services;

/// <inheritdoc />
public class ItemUseCases : EntityUseCases<Item, ItemCreateModel, ItemUpdateModel>, IItemUseCases
{
	public ItemUseCases(ILogger<ItemUseCases> Logger, IMapper Mapper, IRepository<Item> Repository) : base(Logger, Mapper, Repository) { }

	/// <inheritdoc />
	public async Task<Item> GetAsync(string type, string code, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrEmpty(type, nameof(type));
		ArgumentException.ThrowIfNullOrEmpty(code, nameof(code));

		var entity = await Repository.FindAsync(x => x.Type.ToLower() == type.ToLower() && x.Code.ToLower() == code.ToLower(), cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, code);

		if (onlyActive)
			EntityNotActiveException.ThrowIfNotActive(entity!);

		return entity!;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string type, string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = null, int? limit = null, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrEmpty(type, nameof(type));
		ArgumentOutOfRangeException.ThrowIfNegative(skip.GetValueOrDefault(), nameof(skip));
		ArgumentOutOfRangeException.ThrowIfNegative(limit.GetValueOrDefault(), nameof(limit));

		Expression<Func<Item, bool>>? where = item => item.Type.ToLower() == type.ToLower();
		if (!String.IsNullOrEmpty(query))
			where = where.AndAlso(device => device.Name.ToLower().Contains(query.ToLower()));

		return ListAsync(where, item => item.Code, modifiedSince, onlyActive, skip, limit, cancellationToken);
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(ItemCreateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}

	/// <inheritdoc />
	protected override Task ValidateAsync(ItemUpdateModel model, CancellationToken cancellationToken = default)
	{
		// TODO: Implement validation
		return Task.CompletedTask;
	}
}
