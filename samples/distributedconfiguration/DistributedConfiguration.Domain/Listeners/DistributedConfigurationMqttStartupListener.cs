using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using MessagingLibrary.Processing.Mqtt.Listeners;
using MessagingLibrary.TopicClient.Mqtt;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain.Listeners;

public class DistributedConfigurationMqttStartupListener : BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> Subscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<PairDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.PairDevice}"),
            TopicClient.Subscribe<GetPairedDeviceMessageHandler>($"{DistributedConfigurationTopicConstants.RequestUpdate}"),
        };
        return subscriptions;
    }

    public DistributedConfigurationMqttStartupListener(
            IMqttTopicClient<InfrastructureMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}