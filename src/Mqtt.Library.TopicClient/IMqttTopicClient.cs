using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;

namespace Mqtt.Library.TopicClient
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe(ISubscription subscription);
    }
}