using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;

namespace Mqtt.Library.TopicClient
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task Subscribe<T>(string topic) where T : IMessageHandler;
        Task<ISubscription> SubscribeNew<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe<T>(string topic) where T : IMessageHandler;
    }
}