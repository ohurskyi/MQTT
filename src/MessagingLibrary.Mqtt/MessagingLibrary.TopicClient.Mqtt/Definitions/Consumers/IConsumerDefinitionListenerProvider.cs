namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinitionListenerProvider
{
    IEnumerable<IDefinitionListener> Listeners { get; }
}