using System.Text.Json;
using System.Text.Json.Serialization;

using Fand.Runtime.Queues.AzureQueueStorage.Serialization;

namespace Fand.Runtime.Queues.AzureQueueStorage;

/// <summary>
/// Represents the options for an Azure Storage Account queue consumer.
/// </summary>
public class AzureQueueStorageConsumerOptions
{
	private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
	{
		Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if DEBUG
		WriteIndented = true
#endif
	};

	private static readonly IQueueMessageDeserializerFactory _defaultDeserializerFactory = new JsonQueueMessageDeserializerFactory(_defaultJsonSerializerOptions);

	/// <summary>
	/// Gets or sets the deserializer factory used to create queue message deserializer.
	/// </summary>
	public IQueueMessageDeserializerFactory DeserializerFactory { get; set; } = _defaultDeserializerFactory;

	public TimeSpan VisibilityTimeout { get; set; } = TimeSpan.FromSeconds(30);
}
