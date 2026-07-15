namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public interface IQueueMessageSerializerFactory
{
	IQueueMessageSerializer<TMessage> Create<TMessage>()
		where TMessage : notnull;
}
