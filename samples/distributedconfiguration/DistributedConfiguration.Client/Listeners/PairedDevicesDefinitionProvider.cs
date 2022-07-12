using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesDefinitionProvider : IConsumerDefinitionProvider
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new PairedDevicesConsumerDefinition()
    };
}