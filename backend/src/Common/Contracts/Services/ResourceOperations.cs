namespace thc.HotKnobs.Contracts;

public interface IResourceCreateOrUpdateRequest { }

public interface IResourceCreateRequest { }

public interface IResourceResponse
{
	string Id { get; set; }

	bool IsActive { get; set; }
}

public interface IResourceUpdateRequest { }

/// <summary>
/// Base resource operation contract.
/// </summary>
public interface IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
{
}

/// <summary>
/// Resource Create operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateRequest"></typeparam>
public interface IResourceCreateOperation<TResponse, TCreateRequest> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
{
	/// <summary>
	/// Creates a new resource.
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> CreateAsync(TCreateRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource Create or Update operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateOrUpdateRequest"></typeparam>
public interface IResourceCreateOrUpdateOperation<TResponse, TCreateOrUpdateRequest> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
	where TCreateOrUpdateRequest : class, IResourceCreateOrUpdateRequest
{
	/// <summary>
	/// Creates a new resource or updates the resource with new values.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> CreateOrUpdateAsync(string id, TCreateOrUpdateRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource Delete operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IResourceDeleteOperation<TResponse> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
{
	/// <summary>
	/// Deletes the resource by Id.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource Get operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IResourceGetOperation<TResponse> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
{
	/// <summary>
	/// Gets the resource by Id.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="onlyActive"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> GetAsync(string id, bool onlyActive = true, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource List operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IResourceListOperation<TResponse> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
{
	/// <summary>
	/// Retrieves a list of resources based on the specified criteria.
	/// </summary>
	/// <param name="modifiedSince">Limits the resulting list to only resources that have been modified since.</param>
	/// <param name="onlyActive"></param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of resources that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource Update operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
public interface IResourceUpdateOperation<TResponse, TUpdateRequest> : IResourceOperation<TResponse>
	where TResponse : class, IResourceResponse
	where TUpdateRequest : class, IResourceUpdateRequest
{
	/// <summary>
	/// Updates the resource with new values.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> UpdateAsync(string id, TUpdateRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Resource (with both Create and Update) collection operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateRequest"></typeparam>
public interface IResourceCollectionOperationsWithCreateAndUpdate<TResponse, TCreateRequest> : IResourceCreateOperation<TResponse, TCreateRequest>, IResourceListOperation<TResponse>
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
{
}

/// <summary>
/// Resource (with Create or Update) collection operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IResourceCollectionOperationsWithCreateOrUpdate<TResponse> : IResourceListOperation<TResponse>
	where TResponse : class, IResourceResponse
{
}

/// <summary>
/// Resource (with both Create and Update) instance operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateRequest"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
public interface IResourceInstanceOperationsWithCreateAndUpdate<TResponse, TCreateRequest, TUpdateRequest> : IResourceDeleteOperation<TResponse>, IResourceGetOperation<TResponse>, IResourceUpdateOperation<TResponse, TUpdateRequest>
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
	where TUpdateRequest : class, IResourceUpdateRequest
{
}

/// <summary>
/// Resource (with Create or Update) instance operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateOrUpdateRequest"></typeparam>
public interface IResourceInstanceOperationsWithCreateOrUpdate<TResponse, TCreateOrUpdateRequest> : IResourceCreateOrUpdateOperation<TResponse, TCreateOrUpdateRequest>, IResourceDeleteOperation<TResponse>, IResourceGetOperation<TResponse>
	where TResponse : class, IResourceResponse
	where TCreateOrUpdateRequest : class, IResourceCreateOrUpdateRequest
{
}

/// <summary>
/// Resource (with both Create and Update) operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateRequest"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
public interface IResourceOperationsWithCreateAndUpdate<TResponse, TCreateRequest, TUpdateRequest> : IResourceCollectionOperationsWithCreateAndUpdate<TResponse, TCreateRequest>, IResourceInstanceOperationsWithCreateAndUpdate<TResponse, TCreateRequest, TUpdateRequest>
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
	where TUpdateRequest : class, IResourceUpdateRequest
{
}

/// <summary>
/// Resource (with Create or Update) operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateOrUpdateRequest"></typeparam>
public interface IResourceOperationsWithCreateOrUpdate<TResponse, TCreateOrUpdateRequest> : IResourceCollectionOperationsWithCreateOrUpdate<TResponse>, IResourceInstanceOperationsWithCreateOrUpdate<TResponse, TCreateOrUpdateRequest>
	where TResponse : class, IResourceResponse
	where TCreateOrUpdateRequest : class, IResourceCreateOrUpdateRequest
{
}

/// <summary>
/// Resource (with Update only) operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
public interface IResourceOperationsWithUpdateOnly<TResponse, TUpdateRequest> : IResourceCollectionOperationsWithCreateOrUpdate<TResponse>, IResourceInstanceOperationsWithCreateAndUpdate<TResponse, IResourceCreateRequest, TUpdateRequest>
	where TResponse : class, IResourceResponse
	where TUpdateRequest : class, IResourceUpdateRequest
{
}
