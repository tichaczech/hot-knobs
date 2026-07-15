namespace thc.HotKnobs.Runtime.Worker;

/// <summary>
/// Implementation of the <see cref="IConcurrencyTokenContext"/> interface. It uses the single, internal instance of <see cref="Dictionary{T,T}"/> as value storage.
/// </summary>
public class ConcurrencyTokenContext() : IConcurrencyTokenContext
{
	private readonly Dictionary<ValueAccessorKey, ConcurrencyToken> _items = [];

	private record ValueAccessorKey(string Id, ConcurrencyTokenValueSource source);

	/// <inheritdoc />
	public ConcurrencyToken? GetValue(string entityId, ConcurrencyTokenValueSource source)
	{
		if (_items.TryGetValue(new ValueAccessorKey(entityId, source), out var value))
			return value;

		return null;
	}

	/// <inheritdoc />
	public void SetValue(string entityId, ConcurrencyTokenValueSource source, ConcurrencyToken token)
	{
		_items[new ValueAccessorKey(entityId, source)] = token;
	}

	/// <inheritdoc />
	public bool TryGetValue(string entityId, ConcurrencyTokenValueSource source, out ConcurrencyToken token)
	{
		return _items.TryGetValue(new ValueAccessorKey(entityId, source), out token!);
	}
}
