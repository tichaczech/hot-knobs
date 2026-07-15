using System.Text.Json;

namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

internal class JsonQueueMessageDeserializer<TMessage>(JsonSerializerOptions jsonSerializerOptions) : IQueueMessageDeserializer<TMessage>
	where TMessage : notnull
{
	public TMessage DeserializeMessage(string message)
	{
		var result = JsonSerializer.Deserialize<TMessage>(message, jsonSerializerOptions) ?? throw new InvalidOperationException($"Failed to deserialize message of type {typeof(TMessage).FullName}.");

		return result;
	}
}
