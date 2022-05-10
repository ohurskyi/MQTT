using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Topics;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test.Listeners;

public class DeviceStartupListener : StartupListener
{
    public DeviceStartupListener(IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient) : base(topicClient)
    {
    }

    protected override async Task CreateSubscriptions()
    {
        var subscriptions = new List<Task>
        {
            TopicClient.Subscribe<HandlerForDeviceNumber1>($"{DeviceTopics.DeviceTopic}/{1}"),
            TopicClient.Subscribe<HandlerForDeviceNumber2>($"{DeviceTopics.DeviceTopic}/{2}"),
            TopicClient.Subscribe<HandlerForAllDeviceNumbers>($"{DeviceTopics.DeviceTopic}/#"),
        };

        await Task.WhenAll(subscriptions);
    }
}