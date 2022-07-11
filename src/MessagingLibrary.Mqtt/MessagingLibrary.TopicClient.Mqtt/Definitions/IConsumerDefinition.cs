namespace MessagingLibrary.TopicClient.Mqtt.Definitions;

public interface IConsumerDefinition
{
    IEnumerable<ISubscription> Definitions();
}