using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Processing.Mqtt.Listeners;
using MessagingLibrary.TopicClient.Mqtt;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesConfigurationChangedMqttStartupListener : BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> Subscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<UpdateLocalConfigurationMessageHandler>($"{DistributedConfigurationTopicConstants.CurrentConfiguration}"),
            TopicClient.Subscribe<NotifyUsersMessageHandler>($"{DistributedConfigurationTopicConstants.CurrentConfiguration}")
        };
        return subscriptions;
    }

    public PairedDevicesConfigurationChangedMqttStartupListener(
            IMqttTopicClient<InfrastructureMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}