using Xunit;

namespace Fand.Runtime;

public partial class ExtensionsForIEnumerableTests
{
	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenEnumrableIsEmpty()
	{
		// Arrange
		var items = Array.Empty<string>();

		// Act
		var result = items.SortByDependencies(_ => Array.Empty<string>());

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenEnumerableHasSingleItem()
	{
		// Arrange
		var items = new[] { "a" };

		// Act
		var result = items.SortByDependencies(_ => Array.Empty<string>());

		// Assert
		Assert.Equal(items, result);
	}

	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenNoDependenciesExist()
	{
		// Arrange
		var items = new[] { "a", "b", "c", "d" };

		// Act
		var result = items.SortByDependencies(_ => []);

		// Assert
		Assert.Equivalent(items, result);
	}

	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenLinearDependenciesExist()
	{
		// Arrange
		var items = new[] { "d", "c", "b", "a" };
		var dependencies = new Dictionary<string, string[]>
		{
			["a"] = [],
			["b"] = ["a"],
			["c"] = ["b"],
			["d"] = ["c"]
		};

		// Act
		var result = items.SortByDependencies(x => dependencies[x]).ToList();

		// Assert
		Assert.Equal(0, result.IndexOf("a"));
		Assert.True(result.IndexOf("a") < result.IndexOf("b"));
		Assert.True(result.IndexOf("b") < result.IndexOf("c"));
		Assert.True(result.IndexOf("c") < result.IndexOf("d"));
	}

	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenGraphDependenciesExist()
	{
		// Arrange
		var items = new[] { "g", "f", "e", "d", "c", "b", "a" };
		var dependencies = new Dictionary<string, string[]>
		{
			["a"] = [],
			["b"] = ["a"],
			["c"] = ["a"],
			["d"] = ["b", "c"],
			["e"] = ["d"],
			["f"] = ["d"],
			["g"] = ["e", "f"]
		};

		// Act
		var result = items.SortByDependencies(x => dependencies[x]).ToList();

		// Assert
		Assert.Equal(0, result.IndexOf("a"));
		Assert.True(result.IndexOf("a") < result.IndexOf("b"));
		Assert.True(result.IndexOf("a") < result.IndexOf("c"));
		Assert.True(result.IndexOf("b") < result.IndexOf("d"));
		Assert.True(result.IndexOf("c") < result.IndexOf("d"));
		Assert.True(result.IndexOf("d") < result.IndexOf("e"));
		Assert.True(result.IndexOf("d") < result.IndexOf("f"));
		Assert.True(result.IndexOf("e") < result.IndexOf("g"));
		Assert.True(result.IndexOf("f") < result.IndexOf("g"));
	}

	[Fact]
	public void SortByDependencies_ShouldSucceed_WhenMultipleIndependentRootsExists()
	{
		// Arrange
		var items = new[] { "a", "b", "c", "d" };
		var dependencies = new Dictionary<string, string[]>
		{
			["a"] = [],
			["b"] = [],
			["c"] = ["a"],
			["d"] = ["b"]
		};

		// Act
		var result = items.SortByDependencies(x => dependencies[x]).ToList();

		// Assert
		Assert.True(result.IndexOf("a") < result.IndexOf("c"));
		Assert.True(result.IndexOf("b") < result.IndexOf("d"));
	}

	[Fact]
	public void SortByDependencies_ShouldThrowCircularDependenciesException_WhenDirectCircularDependencyExists()
	{
		// Arrange
		var items = new[] { "a", "b" };
		var dependencies = new Dictionary<string, string[]>
		{
			["a"] = ["b"],
			["b"] = ["a"]
		};

		// Act & Assert
		Assert.Throws<CircularDependenciesException>(() => items.SortByDependencies(x => dependencies[x]).ToList());
	}

	[Fact]
	public void SortByDependencies_ShouldThrowCircularDependenciesException_WhenIndirectCircularDependencyExists()
	{
		// Arrange
		var items = new[] { "a", "b", "c", "d", "e" };
		var dependencies = new Dictionary<string, string[]>
		{
			["a"] = ["e"],
			["b"] = ["a"],
			["c"] = ["b"],
			["d"] = ["c"],
			["e"] = ["d"]
		};

		// Act & Assert
		Assert.Throws<CircularDependenciesException>(() => items.SortByDependencies(x => dependencies[x]).ToList());
	}
}
