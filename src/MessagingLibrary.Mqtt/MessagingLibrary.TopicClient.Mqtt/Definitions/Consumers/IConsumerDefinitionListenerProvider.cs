namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinitionListenerProvider
{
    IEnumerable<IConsumerListener> Listeners { get; }
}