namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public interface IQueueMessageDeserializerFactory
{
	IQueueMessageDeserializer<TMessage> Create<TMessage>()
		where TMessage : notnull;
}
