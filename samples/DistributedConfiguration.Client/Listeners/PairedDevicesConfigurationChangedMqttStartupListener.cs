using DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Pairing.Contracts.Topics;

namespace DistributedConfiguration.Client.Listeners;

public class PairedDevicesConfigurationChangedMqttStartupListener : BaseMqttStartupListener<LocalMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<UpdateLocalConfigurationMessageHandler>($"{TopicConstants.CurrentConfiguration}"),
        };
        return subscriptions;
    }

    public PairedDevicesConfigurationChangedMqttStartupListener(
            IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<LocalMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}