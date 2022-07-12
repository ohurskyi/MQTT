using MessagingLibrary.Core.Definitions;
using MessagingLibrary.Core.Definitions.Consumers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesDefinitionProvider : IConsumerDefinitionProvider
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new PairedDevicesConsumerDefinition()
    };
}