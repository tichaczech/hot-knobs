namespace thc.HotKnobs.Contracts;

public interface ISingletonResourceCreateOrUpdateRequest { }

public interface ISingletonResourceResponse { }

public interface ISingletonResourceUpdateRequest { }

/// <summary>
/// Base singleton resource operation contract.
/// </summary>
public interface ISingletonResourceOperation<TResponse>
	where TResponse : class, ISingletonResourceResponse
{
}

/// <summary>
/// Singleton resource Create or Update operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateOrUpdateRequest"></typeparam>
public interface ISingletonResourceCreateOrUpdateOperation<TResponse, TCreateOrUpdateRequest> : ISingletonResourceOperation<TResponse>
	where TResponse : class, ISingletonResourceResponse
	where TCreateOrUpdateRequest : class, ISingletonResourceCreateOrUpdateRequest
{
	/// <summary>
	/// Creates a new singleton resource or updates the singleton resource with new values.
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> CreateOrUpdateAsync(TCreateOrUpdateRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Singleton resource Get operation contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ISingletonResourceGetOperation<TResponse> : ISingletonResourceOperation<TResponse>
	where TResponse : class, ISingletonResourceResponse
{
	/// <summary>
	/// Gets the singleton resource.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TResponse> GetAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Singleton resource operations contract.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TCreateOrUpdateRequest"></typeparam>
public interface ISingletonResourceOperations<TResponse, TCreateOrUpdateRequest> : ISingletonResourceCreateOrUpdateOperation<TResponse, TCreateOrUpdateRequest>, ISingletonResourceGetOperation<TResponse>
	where TResponse : class, ISingletonResourceResponse
	where TCreateOrUpdateRequest : class, ISingletonResourceCreateOrUpdateRequest
{
}
