using System.Linq.Expressions;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// List query for <see cref="Entity"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TRepresentation"></typeparam>
public interface IEntityListQuery<TEntity, TRepresentation> : IModelListQuery<TRepresentation>
	where TEntity : Entity
	where TRepresentation : class, IModel
{
	// Order

	/// <summary>
	/// Gets or sets the filter expression to apply to the query.
	/// </summary>
	/// <remarks>
	/// This property allows you to specify a filter expression that will be used to filter the entities returned by the query. The expression should be a valid LINQ expression that can be translated to the underlying data source.
	/// </remarks>
	Expression<Func<TEntity, bool>>? Filter { get; set; }

	/// <summary>
	/// Gets a value indicating whether filtering of the entities returned by the query is enabled.
	/// </summary>
	/// <remarks>
	/// When set to true, the query will apply the filter expression specified in the <see cref="Filter"/> property to the entities returned by the query; otherwise, the query will throw an <see cref="FilteringDisabledException"/> exception.
	/// </remarks>
	bool FilteringEnabled { get; }

	/// <summary>
	/// Gets or sets a value indicating whether to retrieve only active entities.
	/// </summary>
	/// <remarks>
	/// When set to true, the query will only retrieve active entities; otherwise, it will retrieve all entities regardless of their active status.
	/// </remarks>
	bool OnlyActive { get; set; }

	/// <summary>
	/// Gets or sets the paging token to use for incremental synchronization.
	/// </summary>
	/// <remarks>
	/// This property allows you to specify a paging token that will be used to retrieve the next set of entities in an incremental synchronization scenario. The token should be obtained from the previous query result and passed back to the query to retrieve the next set of entities.
	/// </remarks>
	string? PagingToken { get; set; }

	/// <summary>
	/// Gets or sets the search string to use for searching entities using full-text search or other search mechanisms.
	/// </summary>
	/// <remarks>
	/// This property allows you to specify a search string that will be used to search for entities using full-text search or other search mechanisms. The search string should be a valid search query that can be interpreted by the underlying data source.
	/// </remarks>
	string? Search { get; set; }

	/// <summary>
	/// Gets or sets the selector expression to project the entities into a different representation.
	/// </summary>
	/// <remarks>
	/// This property allows you to specify a selector expression that will be used to project the entities into a different representation. The expression should be a valid LINQ expression that can be translated to the underlying data source.
	/// </remarks>
	Expression<Func<TEntity, TRepresentation>>? Selector { get; set; }

	/// <summary>
	/// Gets or sets the synchronization token to use for incremental synchronization.
	/// </summary>
	/// <remarks>
	/// This property allows you to specify a synchronization token that will be used to retrieve the next set of entities in an incremental synchronization scenario. The token should be obtained from the previous query result and passed back to the query to retrieve the next set of entities.
	/// </remarks>
	string? SynchronizationToken { get; set; }
}
