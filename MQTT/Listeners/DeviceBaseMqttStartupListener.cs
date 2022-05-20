using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Topics;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test.Listeners;

public class DeviceBaseMqttStartupListener : BaseMqttStartupListener<LocalMqttMessagingClientOptions>
{

    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<HandlerForDeviceNumber1>($"{DeviceTopics.DeviceTopic}/{1}"),
            TopicClient.Subscribe<HandlerForDeviceNumber2>($"{DeviceTopics.DeviceTopic}/{2}"),
            //TopicClient.Subscribe<HandlerForAllDeviceNumbers>($"{DeviceTopics.DeviceTopic}/#"),
        };
        return subscriptions;
    }

    public DeviceBaseMqttStartupListener(
            IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<LocalMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}