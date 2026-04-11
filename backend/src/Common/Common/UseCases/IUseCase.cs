using thc.HotKnobs.Model;

namespace thc.HotKnobs.UseCases;

/// <summary>
/// Create model interface.
/// </summary>
public interface ICreateModel { }

/// <summary>
/// Update model interface.
/// </summary>
public interface IUpdateModel { }

/// <summary>
/// Base use case interface.
/// </summary>
public interface IUseCase { }

/// <summary>
/// Base entity service.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IUseCase<TModel> : IUseCase
	where TModel : class, IModel
{
}

/// <summary>
/// Base entity service with create.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TCreateModel"></typeparam>
public interface ICreateUseCase<TModel, TCreateModel> : IUseCase<TModel>
	where TModel : class, IModel
	where TCreateModel : class, ICreateModel
{
	/// <summary>
	/// Creates a new entity.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="model"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TModel> CreateAsync(string? id, TCreateModel model, CancellationToken cancellationToken = default);
}

/// <summary>
/// Base entity service with delete.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IDeleteUseCase<TModel> : IUseCase<TModel>
	where TModel : class, IModel
{
	/// <summary>
	/// Deletes the entity by Id.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

/// <summary>
/// Base entity service with get-by-id.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IGetUseCase<TModel> : IUseCase<TModel>
	where TModel : class, IModel
{
	/// <summary>
	/// Gets the entity by Id.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="onlyActive"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TModel> GetAsync(string id, bool onlyActive = true, CancellationToken cancellationToken = default);
}

/// <summary>
/// Base entity service with list.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IListUseCase<TModel> : IUseCase<TModel>
	where TModel : class, IModel
{
	/// <summary>
	/// Retrieves a list of entities based on the specified criteria.
	/// </summary>
	/// <param name="modifiedSince">Limits the resulting list to only entities that have been modified since.</param>
	/// <param name="onlyActive">Limits the resulting list to only active entities.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>A collection representing the ids of entities that match the specified criteria.</returns>
	IAsyncEnumerable<string> ListAsync(DateTimeOffset? modifiedSince = default, bool onlyActive = true, CancellationToken cancellationToken = default);
}

/// <summary>
/// Base entity service with update.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TUpdateModel"></typeparam>
public interface IUpdateUseCase<TModel, TUpdateModel> : IUseCase<TModel>
	where TModel : class, IModel
	where TUpdateModel : class, IUpdateModel
{
	/// <summary>
	/// Updates the entity with new values.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="model"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<TModel> UpdateAsync(string id, TUpdateModel model, CancellationToken cancellationToken = default);
}

public interface IModelUseCases<TModel, TCreateModel, TUpdateModel> : ICreateUseCase<TModel, TCreateModel>, IDeleteUseCase<TModel>, IGetUseCase<TModel>, IListUseCase<TModel>, IUpdateUseCase<TModel, TUpdateModel>
	where TModel : class, IModel
	where TCreateModel : class, ICreateModel
	where TUpdateModel : class, IUpdateModel
{
}
