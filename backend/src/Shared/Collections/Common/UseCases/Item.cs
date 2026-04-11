using thc.HotKnobs.Model;
using thc.HotKnobs.Shared.Collections.Model.Entities;
using thc.HotKnobs.UseCases;

namespace thc.HotKnobs.Shared.Collections.Services;

public abstract class ItemModel : ModelWithName
{
	public required string Code { get; set; }

	public IDictionary<string, object>? Data { get; set; }

	public required string Type { get; set; }
}

public class ItemCreateModel : ItemModel, IEntityCreateModel { }

public class ItemUpdateModel : ItemModel, IEntityUpdateModel { }


public interface IItemUseCases : IEntityUseCases<Item, ItemCreateModel, ItemUpdateModel>
{
	Task<Item> GetAsync(string type, string code, bool onlyActive = true, CancellationToken cancellationToken = default);

	IAsyncEnumerable<string> ListAsync(string type, string? query = default, DateTimeOffset? modifiedSince = default, bool onlyActive = true, int? skip = null, int? limit = null, CancellationToken cancellationToken = default);
}
