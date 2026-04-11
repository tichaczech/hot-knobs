using System.ComponentModel.DataAnnotations;
using mojeEUC.Contracts;
using Refit;

namespace mojeEUC.Shared.Configuration.Contracts.v1;

public abstract class ItemByTypeContract
{
	public IDictionary<string, object>? Data { get; set; }
}

public class ItemByTypeCreateOrUpdateRequest : ItemByTypeContract, ICreateOrUpdateRequest { }

public class ItemByTypeResponse : ItemByTypeContract, IResponse
{
	[Required]
	public string Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public bool IsActive { get; set; }
}

/// <summary>
/// Provides methods for managing configuration items.
/// </summary>
public interface IItemByTypeService
{
	///	<summary>
	/// Creates or updates a configuration item by type.
	/// </summary>
	[Put("/v1/{type}/{name}")]
	Task<ItemByTypeResponse> CreateOrUpdateAsync(string type, string name, ItemByTypeCreateOrUpdateRequest request, CancellationToken cancellationToken = default);

	///	<summary>
	/// Deletes a configuration item by type and name.
	/// </summary>
	[Delete("/v1/{type}/{name}")]
	Task DeleteAsync(string type, string name, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets configuration item by type and name.
	/// </summary>
	[Get("/v1/{type}/{name}")]
    Task<ItemByTypeResponse> GetAsync(string type, string name, bool onlyActive = true, CancellationToken cancellationToken = default);

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
