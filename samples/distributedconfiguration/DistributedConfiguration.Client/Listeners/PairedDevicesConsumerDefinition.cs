using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesConsumerDefinition : IConsumerDefinition
{
    public IEnumerable<ISubscription> Definitions()
    {
        return new List<ISubscription>
        {
            new Subscription<UpdateLocalConfigurationMessageHandler>($"{DistributedConfigurationTopicConstants.CurrentConfiguration}"),
            new Subscription<NotifyUsersMessageHandler>($"{DistributedConfigurationTopicConstants.CurrentConfiguration}")
        };
    }
}