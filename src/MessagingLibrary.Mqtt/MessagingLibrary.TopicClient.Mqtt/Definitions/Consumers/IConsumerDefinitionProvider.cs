namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinitionProvider
{
    IEnumerable<IConsumerDefinition> Definitions { get; }
}