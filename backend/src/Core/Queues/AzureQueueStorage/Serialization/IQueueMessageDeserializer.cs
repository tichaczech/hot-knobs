namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public interface IQueueMessageDeserializer<out TMessage>
	where TMessage : notnull
{
	TMessage DeserializeMessage(string message);
}
