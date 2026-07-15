namespace Fand.Runtime.Queues.AzureQueueStorage.Serialization;

public interface IQueueMessageSerializer<in TMessage>
	where TMessage : notnull
{
	string SerializeMessage(TMessage message);
}
