using CommunityToolkit.Diagnostics;

namespace Fand.Runtime;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class ExtensionsForIEnumerable
{
	/// <summary>
	/// Fast and low-memory algorithm to sorts items by dependencies (topological sort).
	/// </summary>
	/// <typeparam name="T">Item type.</typeparam>
	/// <param name="items">Items to sort.</param>
	/// <param name="dependencies">Function delegate to get item dependecies.</param>
	/// <returns><see cref="IEnumerable{T}" /> with items sorted by dependecies.</returns>
	/// <exception cref="CircularDependenciesException">Circular dependency exists between items</exception>
	/// <remarks>
	/// Definition: http://en.wikipedia.org/wiki/Topological_sorting
	/// Inspiration: http://tawani.blogspot.com/2009/02/topological-sorting-and-cyclic.html
	/// Original Java implementation: http://www.java2s.com/Code/Java/Collections-Data-Structure/Topologicalsorting.htm
	/// </remarks>
	public static IEnumerable<T> SortByDependencies<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> dependencies)
		where T : notnull
	{
		Guard.IsNotNull(items);
		Guard.IsNotNull(dependencies);

		var matrix = new Dictionary<T, HashSet<T>>();
		foreach (var item in items)
		{
			var dependsOn = dependencies(item);
			matrix.Add(item, [.. dependsOn]);
		}

		var result = new List<T>(matrix.Count);
		while (matrix.Count > 0)
		{
			var independentItem = default(T)!;
			var foundIndependent = false;

			foreach (var pair in matrix)
			{
				if (pair.Value.Count > 0)
					continue;

				independentItem = pair.Key;
				foundIndependent = true;

				break;
			}

			if (!foundIndependent)
				// TODO: Improve error message - add items with circular dependencies
				throw new CircularDependenciesException($"Circular dependency found during sorting items by dependencies!");

			result.Add(independentItem!);
			_ = matrix.Remove(independentItem!);

			foreach (var pair in matrix)
				_ = pair.Value.Remove(independentItem!);
		}

		return [.. result];
	}
}
