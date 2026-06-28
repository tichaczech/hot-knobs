using System.Linq.Expressions;
using mojeEUC.Services;
using mojeEUC.Shared.Configuration.Model.Entities;

namespace mojeEUC.Shared.Configuration.Services;

public abstract class ItemModel
{
	public IDictionary<string, object>? Data { get; set; }

	public string? DeviceId { get; set; }

	public string Name { get; set; }

	public string? PatientId { get; set; }

	public string Type { get; set; }

	public string UserId { get; set; }
}

public class ItemCreateModel : ItemModel, ICreateModel { }

public class ItemUpdateModel : ItemModel, IUpdateModel { }

public interface IItemService : IEntityService<Item, ItemCreateModel, ItemUpdateModel>
{
	Task<Item> GetAsync(string type, string name, bool onlyActive = true, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> ListAsync(string? type = default, Expression<Func<Item, string>>? selector = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = default, int? limit = default, CancellationToken cancellationToken = default);
}
