using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;

namespace DistributedConfiguration.Domain.Listeners;

public class DistributedConfigurationConsumerDefinition : IConsumerDefinition
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