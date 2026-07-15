using System.Collections.Concurrent;
using System.Text.Json;

namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public class JsonQueueMessageDeserializerFactory(JsonSerializerOptions jsonSerializerOptions) : IQueueMessageDeserializerFactory
{
	private readonly ConcurrentDictionary<Type, object> _serializers = new();

	public IQueueMessageDeserializer<TMessage> Create<TMessage>()
		where TMessage : notnull
	{
		return (IQueueMessageDeserializer<TMessage>)_serializers.GetOrAdd(typeof(TMessage), _ => new JsonQueueMessageDeserializer<TMessage>(jsonSerializerOptions));
	}
}
