using MessagingLibrary.Client.Mqtt.Configuration;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinitionProvider<TMessagingClientOptions>
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    IEnumerable<IConsumerDefinition> Definitions { get; }
}