using Mqtt.Library.Client.Configuration;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Processing.Listeners;

public abstract class BaseMqttStartupListener<TMessagingClientOptions> : IMqttStartupListener<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    protected readonly IMqttTopicClient<TMessagingClientOptions> TopicClient;

    protected BaseMqttStartupListener(IMqttTopicClient<TMessagingClientOptions> topicClient)
    {
        TopicClient = topicClient;
    }

    public abstract Task CreateSubscriptions();
}