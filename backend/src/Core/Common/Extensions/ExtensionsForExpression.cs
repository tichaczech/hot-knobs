using System.Linq.Expressions;

using CommunityToolkit.Diagnostics;

namespace Fand.Runtime;

/// <summary>
/// Extension methods for <see cref="Expression"/>.
/// </summary>
public static class ExtensionsForExpression
{
	/// <summary>
	/// Combines two expressions using AND operator.
	/// </summary>
	/// <typeparam name="T">Expression parameter type.</typeparam>
	/// <param name="left">Left expression.</param>
	/// <param name="right">Right expression.</param>
	/// <returns>New <see cref="Expression{Func{T, Boolean}}" /> with combined operands.</returns>
	public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
	{
		Guard.IsNotNull(left);
		Guard.IsNotNull(right);

		var parameter = left.Parameters[0];
		var visitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
		var visitedBody = visitor.Visit(right.Body);

		return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, visitedBody), parameter);
	}

	private sealed class ReplaceParameterVisitor : ExpressionVisitor
	{
		private readonly ParameterExpression _oldParameter;

		private readonly ParameterExpression _newParameter;

		public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
		{
			_oldParameter = oldParameter;
			_newParameter = newParameter;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (ReferenceEquals(node, _oldParameter))
				return _newParameter;

			return base.VisitParameter(node);
		}
	}
}
