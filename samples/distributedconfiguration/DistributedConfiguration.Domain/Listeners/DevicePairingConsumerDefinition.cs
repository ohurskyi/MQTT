using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using MessagingLibrary.Core.Definitions;
using MessagingLibrary.Core.Definitions.Consumers;
using MessagingLibrary.Core.Definitions.Subscriptions;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

namespace DistributedConfiguration.Domain.Listeners;

public class DevicePairingConsumerDefinition : IConsumerDefinition
{
    public IEnumerable<ISubscription> Definitions()
    {
        return new List<ISubscription>
        {
            new Subscription<PairDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.PairDevice}"),
            new Subscription<GetPairedDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.RequestUpdate}")
        };
    }
}