using Fand.Runtime;

using thc.HotKnobs.Models;

namespace thc.HotKnobs.Queries;

/// <summary>
/// Represents an error that occurs when filtering is disabled for a query.
/// This exception is typically thrown when an operation requires filtering to be enabled, but the query has filtering disabled.
/// </summary>
public class FilteringDisabledException : NonCriticalException
{
	/// <inheritdoc />
	public FilteringDisabledException(string message) : base(message)
	{
	}

	/// <summary>
	/// Throws a <see cref="FilteringDisabledException"/> if filtering is disabled but a filter expression is specified for the query.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="query"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="FilteringDisabledException"></exception>
	public static void ThrowIfFilterSpecified<TEntity>(IEntityListQuery<TEntity> query)
		where TEntity : Entity
	{
		ArgumentNullException.ThrowIfNull(query, nameof(query));

		// TODO: Implement filtering enabled/disabled logic if needed in the future.
		// if (!query.FilteringEnabled && query.Filter is not null)
		// 	throw new FilteringDisabledException($"Filtering is disabled for the query of type: '{typeof(TEntity).Name}', but a filter expression was specified.");
	}
}
