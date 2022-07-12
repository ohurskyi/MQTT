using MessagingLibrary.Core.Definitions;
using MessagingLibrary.Core.Definitions.Consumers;
using MessagingLibrary.Processing.Listeners;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesDefinitionProvider : IConsumerDefinitionProvider
{
    public IEnumerable<IConsumerDefinition> Definitions => new List<IConsumerDefinition>
    {
        new PairedDevicesConsumerDefinition()
    };
}