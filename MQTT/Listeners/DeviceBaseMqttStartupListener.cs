using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Topics;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test.Listeners;

// how to properly close generic
public class DeviceBaseMqttStartupListener<TMessagingClientOptions> : BaseMqttStartupListener<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    public DeviceBaseMqttStartupListener(IMqttTopicClient<TMessagingClientOptions> topicClient) : base(topicClient)
    {
    }
    
    public override async Task CreateSubscriptions()
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