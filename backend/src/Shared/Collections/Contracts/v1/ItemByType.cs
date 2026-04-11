using System.ComponentModel.DataAnnotations;
using mojeEUC.Contracts;
using Refit;

namespace mojeEUC.Shared.Collections.Contracts.v1;

public abstract class ItemByTypeContract
{
	public IDictionary<string, object>? Data { get; set; }

	[Required]
	public string Name { get; set; } = default!;
}

public class ItemByTypeCreateOrUpdateRequest : ItemByTypeContract, ICreateOrUpdateRequest { }

public class ItemByTypeResponse : ItemByTypeContract, IResponse
{
	[Required]
	public string Code { get; set; }
	[Required]
	public string Id { get; set; }
	[Required]
	public bool IsActive { get; set; }
}

/// <summary>
/// Provides methods for managing collection items.
/// </summary>
public interface IItemByTypeService
{
	///	<summary>
	/// Creates or updates a collection item by type.
	/// </summary>
	[Put("/v1/{type}/{code}")]
	Task<ItemByTypeResponse> CreateOrUpdateAsync(string type, string code, ItemByTypeCreateOrUpdateRequest request, CancellationToken cancellationToken = default);

	///	<summary>
	/// Deletes a collection item by type and code.
	/// </summary>
	[Delete("/v1/{type}/{code}")]
	Task DeleteAsync(string type, string code, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets collection item by type and code.
	/// </summary>
	[Get("/v1/{type}/{code}")]
    Task<ItemByTypeResponse> GetAsync(string type, string code, bool onlyActive = true, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a list of collection items by type and the specified criteria.
	/// </summary>
	[Get("/v1/{type}")]
#if ASYNC_ENUMERABLE
    IAsyncEnumerable<string> ListAsync(string type, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = default, int? limit = default, CancellationToken cancellationToken = default);
#else
    Task<IEnumerable<string>> ListAsync(string type, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = default, int? limit = default, CancellationToken cancellationToken = default);
#endif
}
