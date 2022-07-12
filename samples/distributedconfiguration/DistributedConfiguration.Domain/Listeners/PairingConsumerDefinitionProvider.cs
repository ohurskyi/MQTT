using MessagingLibrary.Core.Definitions;
using MessagingLibrary.Core.Definitions.Consumers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

namespace DistributedConfiguration.Domain.Listeners;

public class PairingConsumerDefinitionProvider : IConsumerDefinitionProvider
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new DevicePairingConsumerDefinition()
    };
}