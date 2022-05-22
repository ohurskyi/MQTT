using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Contracts.Topics;
using MqttLibrary.Examples.Domain.Handlers;

namespace Mqtt.Library.Test.Listeners;

public class DeviceBaseMqttStartupListenerDevConfig : BaseMqttStartupListener<TestMqttMessagingClientOptions>
{

    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<HandlerForDeviceNumber1DevConfig>($"{TopicConstants.DeviceTopic}/{1}"),
        };
        return subscriptions;
    }

    public DeviceBaseMqttStartupListenerDevConfig(
        IMqttTopicClient<TestMqttMessagingClientOptions> topicClient, 
        ILogger<BaseMqttStartupListener<TestMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}