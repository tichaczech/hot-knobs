// using mojeEUC.Contracts;

// namespace mojeEUC.Shared.Collections.Contracts.v1;

// public abstract class CollectionTypeContract
// {
// 	public string DisplayName { get; set; }

// 	public string Name { get; set; }
// }

// public class CollectionTypeCreateRequest : CollectionItemContract, ICreateRequest { }

// public abstract class CollectionTypeUpdateRequest : CollectionItemContract, IUpdateRequest { }

// public class CollectionTypeResponse : CollectionItemContract, IResponse
// {
// 	public string Id { get; set; }

// 	public bool IsActive { get; set; }
// }

// public interface ICollectionTypeService : IService<CollectionTypeCreateRequest, CollectionTypeUpdateRequest, CollectionTypeResponse>
// {
//     Task<CollectionItemResponse> GetByNameAsync(string name, CancellationToken cancellationToken = default);
// }
