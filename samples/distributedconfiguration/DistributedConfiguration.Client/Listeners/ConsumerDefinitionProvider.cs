using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client.Listeners;

public class ConsumerDefinitionProvider : IConsumerDefinitionProvider<InfrastructureMqttMessagingClientOptions>
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new PairedDevicesConsumerDefinition()
    };
}