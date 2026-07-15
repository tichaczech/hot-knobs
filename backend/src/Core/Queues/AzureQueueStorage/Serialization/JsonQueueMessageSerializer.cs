using System.Text.Json;

namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

internal class JsonQueueMessageSerializer<TMessage>(JsonSerializerOptions jsonSerializerOptions) : IQueueMessageSerializer<TMessage>
	where TMessage : notnull
{
	public string SerializeMessage(TMessage message)
	{
		var json = JsonSerializer.Serialize(message, jsonSerializerOptions);

		return json;
	}
}
