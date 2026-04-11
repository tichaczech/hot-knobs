using mojeEUC.Contracts;
using Refit;

namespace mojeEUC.Shared.Collections.Contracts.v1;

public abstract class ItemContract
{
	public string Code { get; set; }

	public IDictionary<string, object>? Data { get; set; }

	public string Name { get; set; }

	public string Type { get; set; }
}

public class ItemCreateOrUpdateRequest : ItemContract, ICreateOrUpdateRequest { }

public class ItemResponse : ItemContract, IResponse
{
	public string Id { get; set; }

	public bool IsActive { get; set; }
}

/// <summary>
/// Provides methods for managing collection items.
/// </summary>
public interface IItemService : IEntityServiceWithCreateOrUpdate<ItemResponse, ItemCreateOrUpdateRequest>
{
	/// <inheritdoc />
	[Put("/v1/items/{id}")]
	Task<ItemResponse> CreateOrUpdateAsync(string id, [Body] ItemCreateOrUpdateRequest request, CancellationToken cancellationToken = default);

	/// <inheritdoc />
	[Delete("/v1/items/{id}")]
	Task DeleteAsync(string id, CancellationToken cancellationToken = default);

	/// <inheritdoc />
	[Get("/v1/items/{id}")]
	Task<ItemResponse> GetAsync(string id, bool onlyActive = true, CancellationToken cancellationToken = default);

	/// <inheritdoc />
	[Get("/v1/items")]
	Task<IEnumerable<string>> ListAsync(DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of collection items by the specified criteria.
	/// </summary>
#if ASYNC_ENUMERABLE
	IAsyncEnumerable<string> ListAsync(string? type = null, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = default, int? limit = default, CancellationToken cancellationToken = default);
#else
	Task<IEnumerable<string>> ListAsync(string? type = null, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = default, int? limit = default, CancellationToken cancellationToken = default);
#endif
}
