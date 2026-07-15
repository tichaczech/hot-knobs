using System.Linq.Expressions;
using Xunit;

namespace Fand.Runtime;

public class ExtensionsForExpressionUnitTests
{
	[Fact]
	public void AndAlso_ShouldSucceed_WhenCombiningBooleanExpressions()
	{
		// Arrange
		Expression<Func<bool, bool>> expr1 = x => x;
		Expression<Func<bool, bool>> expr2 = x => x;

		// Act
		var combined = expr1.AndAlso(expr2);
		var compiled = combined.Compile();

		// Assert
		Assert.True(compiled(true));
		Assert.False(compiled(false));
	}

	[Fact]
	public void AndAlso_ShouldSucceed_WhenCombiningDecimalExpressions()
	{
		// Arrange
		Expression<Func<decimal, bool>> expr1 = x => (x - Math.Truncate(x)) > 0.3m;
		Expression<Func<decimal, bool>> expr2 = x => (x - Math.Truncate(x)) < 0.7m;

		// Act
		var combined = expr1.AndAlso(expr2);
		var compiled = combined.Compile();

		// Assert
		Assert.True(compiled(6.375m));
		Assert.True(compiled(2.521m));
		Assert.False(compiled(3.12m));
		Assert.False(compiled(1.87m));
	}

	[Fact]
	public void AndAlso_ShouldSucceed_WhenCombiningIntegerExpressions()
	{
		// Arrange
		Expression<Func<int, bool>> expr1 = x => x > 5;
		Expression<Func<int, bool>> expr2 = x => x < 10;

		// Act
		var combined = expr1.AndAlso(expr2);
		var compiled = combined.Compile();

		// Assert
		Assert.True(compiled(6));
		Assert.True(compiled(9));
		Assert.False(compiled(-3));
		Assert.False(compiled(15));
	}

	[Fact]
	public void AndAlso_ShouldSucceed_WhenCombiningStringExpressions()
	{
		// Arrange
		Expression<Func<string, bool>> expr1 = s => s.Length > 3;
		Expression<Func<string, bool>> expr2 = s => s.StartsWith("test");

		// Act
		var combined = expr1.AndAlso(expr2);
		var compiled = combined.Compile();

		// Assert
		Assert.True(compiled("test"));
		Assert.True(compiled("tester"));
		Assert.False(compiled("tes"));
		Assert.False(compiled("foo"));
	}

	[Fact]
	public void AndAlso_ShouldThrowArgumentNullException_WhenLeftExpressionIsNull()
	{
		// Arrange
		Expression<Func<int, bool>> expr1 = null!;
		Expression<Func<int, bool>> expr2 = x => x < 10;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => expr1.AndAlso(expr2));
	}

	[Fact]
	public void AndAlso_ShouldThrowArgumentNullException_WhenRightExpressionIsNull()
	{
		// Arrange
		Expression<Func<int, bool>> expr1 = x => x < 10;
		Expression<Func<int, bool>> expr2 = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => expr1.AndAlso(expr2));
	}
}
