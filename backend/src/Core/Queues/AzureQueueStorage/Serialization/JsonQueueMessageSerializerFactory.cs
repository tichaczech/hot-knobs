using System.Collections.Concurrent;
using System.Text.Json;

namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public class JsonQueueMessageSerializerFactory(JsonSerializerOptions jsonSerializerOptions) : IQueueMessageSerializerFactory
{
	private readonly ConcurrentDictionary<Type, object> _serializers = new();

	public IQueueMessageSerializer<TMessage> Create<TMessage>()
		where TMessage : notnull
	{
		return (IQueueMessageSerializer<TMessage>)_serializers.GetOrAdd(typeof(TMessage), _ => new JsonQueueMessageSerializer<TMessage>(jsonSerializerOptions));
	}
}
