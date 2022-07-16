using MessagingLibrary.Core.Definitions.Consumers;
using MessagingLibrary.Processing.Listeners;

namespace DistributedConfiguration.Domain.Listeners;

public class PairingConsumerDefinitionProvider : IConsumerDefinitionProvider
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new DevicePairingConsumerDefinition()
    };
}