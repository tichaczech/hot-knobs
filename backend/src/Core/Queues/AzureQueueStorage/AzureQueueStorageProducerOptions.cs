using System.Text.Json;
using System.Text.Json.Serialization;

using Fand.Runtime.Queues.AzureQueueStorage.Serialization;

namespace Fand.Runtime.Queues.AzureQueueStorage;

/// <summary>
/// Represents the options for an Azure Storage Account queue producer.
/// </summary>
public class AzureQueueStorageProducerOptions
{
	private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
	{
		Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if DEBUG
		WriteIndented = true
#endif
	};

	private static readonly IQueueMessageSerializerFactory _defaultSerializerFactory = new JsonQueueMessageSerializerFactory(_defaultJsonSerializerOptions);

	/// <summary>
	/// Gets or sets the serializer factory used to create queue message serializer.
	/// </summary>
	public IQueueMessageSerializerFactory SerializerFactory { get; set; } = _defaultSerializerFactory;
}
