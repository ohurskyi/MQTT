using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Processing.Mqtt.Listeners;
using MessagingLibrary.TopicClient.Mqtt;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesConfigurationChangedMqttStartupListener : BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<UpdateLocalConfigurationMessageHandler>($"{TopicConstants.CurrentConfiguration}"),
            TopicClient.Subscribe<NotifyUsersMessageHandler>($"{TopicConstants.CurrentConfiguration}")
        };
        return subscriptions;
    }

    public PairedDevicesConfigurationChangedMqttStartupListener(
            IMqttTopicClient<InfrastructureMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}