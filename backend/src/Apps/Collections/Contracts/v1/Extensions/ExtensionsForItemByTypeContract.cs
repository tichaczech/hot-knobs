using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace mojeEUC.Shared.Collections.Contracts.v1.Extensions;

public static class ItemByTypeContractExtensions
{
	public static void SetProperty<TItem>(this ItemByTypeContract contract, Expression<Func<TItem, object?>> propertyExpression, object value)
	{
		string propertyName = propertyExpression.Body switch
		{
			MemberExpression memberExpression => memberExpression.Member.Name,
			UnaryExpression { Operand: MemberExpression memberExpr } => memberExpr.Member.Name,
			_ => throw new ArgumentException("Invalid expression. Please pass a property.")
		};
		propertyName = ToCamelCase(propertyName);
		contract.Data![propertyName] = value;
	}

	public static bool TryGetProperty<TItem, TResult>(this ItemByTypeContract contract, Expression<Func<TItem, TResult>> propertyExpression, [MaybeNullWhen(false)] out TResult result)
	{
		result = default;

		if (propertyExpression.Body is not MemberExpression memberExpression)
		{
			return false;
		}

		string propertyName = ToCamelCase(memberExpression.Member.Name);
		if (contract.Data == null || !contract.Data.TryGetValue(propertyName, out var value) || value is not TResult typedValue)
		{
			return false;
		}

		result = typedValue;
		return true;
	}

	private static string ToCamelCase(string input) =>
		char.ToLower(input[0]) + input[1..];
}
