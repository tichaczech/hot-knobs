namespace thc.HotKnobs.Runtime;

/// <summary>
/// Enumeration representing the source of an ETag value.
/// The source can be either internal, meaning the ETag is managed by the application itself,
/// or external, meaning the ETag is provided by an external system or service.
/// </summary>
public enum ConcurrencyTokenValueSource
{
	/// <summary>
	/// The Concurrency Token value is provided internally by the application.
	/// </summary>
	Internal,

	/// <summary>
	/// The Concurrency Token value is provided by an external system or service.
	/// </summary>
	External
}

public enum ConcurrencyTokenType
{
	/// <summary>
	/// The Concurrency Token is represented as a ETag (string value).
	/// </summary>
	ETag,

	/// <summary>
	/// The Concurrency Token is represented as a timestamp.
	/// </summary>
	LastModified
}

public record ConcurrencyToken(object Value, ConcurrencyTokenType Type)
{
	/// <summary>
	/// The value of the Concurrency Token.
	/// </summary>
	public object Value { get; init; } = Value;

	/// <summary>
	/// The type of the Concurrency Token.
	/// </summary>
	public ConcurrencyTokenType Type { get; init; } = Type;
}

public record ETag(string Value) : ConcurrencyToken(Value, ConcurrencyTokenType.ETag)
{
	/// <summary>
	/// The value of the ETag.
	/// </summary>
	public new string Value { get; init; } = Value;
}

public record LastModified(DateTimeOffset Value) : ConcurrencyToken(Value, ConcurrencyTokenType.LastModified)
{
	/// <summary>
	/// The value of the LastModified timestamp.
	/// </summary>
	public new DateTimeOffset Value { get; init; } = Value;
}

/// <summary>
/// Defines an accessor for managing the Concurrency Token values of entities.
/// This interface provides methods to retrieve Concurrency Token values based on the entity ID and the source of the Concurrency Token value.
/// </summary>
public interface IConcurrencyTokenContextAccessor
{
	/// <summary>
	/// Retrieves the Concurrency Token value for a specific entity ID, token type and source.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity.</param>
	/// <param name="source">The source of the Concurrency Token value.</param>
	/// <returns>The Concurrency Token value for the specified entity ID, token type and source, or null if not found.</returns>
	ConcurrencyToken? GetValue(string entityId, ConcurrencyTokenValueSource source);

	/// <summary>
	/// Attempts to retrieve the Concurrency Token value for a specific entity ID, token type and source.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity.</param>
	/// <param name="source">The source of the Concurrency Token value.</param>
	/// <param name="token">When this method returns, contains the Concurrency Token value if found; otherwise, null.</param>
	/// <returns>True if the Concurrency Token value was found; otherwise, false.</returns>
	bool TryGetValue(string entityId, ConcurrencyTokenValueSource source, out ConcurrencyToken token);
}

/// <summary>
/// Defines a context for managing the Concurrency Token of an entity within a specific scope, such as an HTTP request.
/// This interface extends the accessor capabilities of <see cref="IConcurrencyTokenContextAccessor"/> by providing a method to set the Concurrency Token value.
/// </summary>
/// <remarks>
/// This context is typically scoped to a single operation or request to track the Concurrency Token value as it's processed.
/// </remarks>
public interface IConcurrencyTokenContext : IConcurrencyTokenContextAccessor
{
	/// <summary>
	/// Sets the Concurrency Token value for a specific entity ID and source.
	/// This method allows the application to update or initialize the Concurrency Token value for an entity based on its ID and the source of the Concurrency Token value.
	/// The source indicates whether the Concurrency Token is managed internally by the application or provided by an external system.
	/// </summary>
	/// <param name="entityId"></param>
	/// <param name="source"></param>
	/// <param name="token"></param>
	void SetValue(string entityId, ConcurrencyTokenValueSource source, ConcurrencyToken token);
}
