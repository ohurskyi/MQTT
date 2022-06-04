using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Pairing.Contracts.Topics;
using MqttLibrary.Examples.Pairing.Domain.Handlers;

namespace MqttLibrary.Examples.Pairing.Domain.Listeners;

public class PairDeviceMqttStartupListener : BaseMqttStartupListener<LocalMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<PairDeviceMessageHandler>($"{TopicConstants.RequestUpdate}"),
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