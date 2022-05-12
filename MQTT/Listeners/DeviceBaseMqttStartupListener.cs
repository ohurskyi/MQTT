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
    
    public override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.SubscribeNew<HandlerForDeviceNumber1>($"{DeviceTopics.DeviceTopic}/{1}"),
            TopicClient.SubscribeNew<HandlerForDeviceNumber2>($"{DeviceTopics.DeviceTopic}/{2}"),
            TopicClient.SubscribeNew<HandlerForAllDeviceNumbers>($"{DeviceTopics.DeviceTopic}/#"),
        };
        return subscriptions;
    }
}