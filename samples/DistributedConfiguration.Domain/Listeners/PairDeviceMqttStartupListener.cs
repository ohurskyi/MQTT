using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.TopicClient;

namespace DistributedConfiguration.Domain.Listeners;

public class PairDeviceMqttStartupListener : BaseMqttStartupListener<LocalMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<PairDeviceMessageHandler>($"{TopicConstants.PairDevice}"),
            TopicClient.Subscribe<GetPairedDeviceMessageHandler>($"{TopicConstants.RequestUpdate}"),
        };
        return subscriptions;
    }

    public PairDeviceMqttStartupListener(
            IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<LocalMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}