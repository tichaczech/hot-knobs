using Microsoft.AspNetCore.Http;

namespace thc.HotKnobs.Runtime.Server.Filters;

/// <summary>
/// Implementation of the <see cref="IConcurrencyTokenContext"/> interface. It uses the <see cref="IHttpContextAccessor"/> as value storage.
/// </summary>
public class ConcurrencyTokenContext : IConcurrencyTokenContext
{
	private const string HTTP_CONTEXT_ITEM_NAME = "ConcurrencyTokenContextItems";

	private readonly IHttpContextAccessor _httpContextAccessor;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConcurrencyTokenContext"/> class.
	/// This constructor requires an <see cref="IHttpContextAccessor"/> to access the current HTTP context.
	/// </summary>
	public ConcurrencyTokenContext(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

	private record ValueAccessorKey(string Id, ConcurrencyTokenValueSource source);

	/// <inheritdoc />
	public ConcurrencyToken? GetValue(string entityId, ConcurrencyTokenValueSource source)
	{
		if (GetItems().TryGetValue(new ValueAccessorKey(entityId, source), out var value))
			return value;

		return null;
	}

	/// <inheritdoc />
	public void SetValue(string entityId, ConcurrencyTokenValueSource source, ConcurrencyToken token)
	{
		GetItems()[new ValueAccessorKey(entityId, source)] = token;
	}

	/// <inheritdoc />
	public bool TryGetValue(string entityId, ConcurrencyTokenValueSource source, out ConcurrencyToken token)
	{
		return GetItems().TryGetValue(new ValueAccessorKey(entityId, source), out token!);
	}

	private Dictionary<ValueAccessorKey, ConcurrencyToken> GetItems()
	{
		if (_httpContextAccessor.HttpContext!.Items.TryGetValue(HTTP_CONTEXT_ITEM_NAME, out var value))
			return (value as Dictionary<ValueAccessorKey, ConcurrencyToken>)!;

		var items = new Dictionary<ValueAccessorKey, ConcurrencyToken>();
		_httpContextAccessor.HttpContext!.Items[HTTP_CONTEXT_ITEM_NAME] = items;

		return items;
	}
}
