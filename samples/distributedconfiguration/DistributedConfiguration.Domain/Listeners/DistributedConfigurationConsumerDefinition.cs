using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using MessagingLibrary.TopicClient.Mqtt;
using MessagingLibrary.TopicClient.Mqtt.Definitions;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain.Listeners;

public class DistributedConfigurationConsumerDefinition : IConsumerDefinition<InfrastructureMqttMessagingClientOptions>
{
    public IEnumerable<Task<ISubscription>> Subscriptions(IMqttTopicClient<InfrastructureMqttMessagingClientOptions> topicClient)
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            topicClient.Subscribe<PairDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.PairDevice}"),
            topicClient.Subscribe<GetPairedDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.RequestUpdate}"),
        };
        return subscriptions;
    }
}