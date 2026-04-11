using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using mojeEUC.Model;
using mojeEUC.Model.Repository;
using mojeEUC.Services;
using mojeEUC.Shared.Configuration.Model.Entities;

namespace mojeEUC.Shared.Configuration.Services;

/// <inheritdoc />
public class ItemService(ILogger<ItemService> _logger, IMapper _mapper, IRepository<Item> _repository)
	: ServiceWithRepository<Item, ItemCreateModel, ItemUpdateModel>(_logger, _mapper, _repository), IItemService
{
	/// <inheritdoc />
	public async Task<Item> GetAsync(string type, string name, bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var entity = await Repository.FindAsync(x => x.Type.ToLower() == type.ToLower() && x.Name.ToLower() == name.ToLower(), cancellationToken);
		EntityNotFoundException.ThrowIfNull(entity, name);

		if (onlyActive)
			EntityNotActiveException.ThrowIfNotActive(entity!);

		return entity!;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<string> ListAsync(string? type = null, Expression<Func<Item, string>>? selector = default, DateTimeOffset? modifiedSince = null, bool onlyActive = true, int? skip = null, int? limit = null, CancellationToken cancellationToken = default)
	{
		Expression<Func<Item, bool>>? where = default;

		if (!String.IsNullOrEmpty(type))
			where = item => item.Type.ToLower() == type.ToLower();

		return base.ListAsync(where, selector, modifiedSince, onlyActive, skip, limit, cancellationToken);
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
