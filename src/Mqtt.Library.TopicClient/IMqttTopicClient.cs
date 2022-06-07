using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Handlers;

namespace Mqtt.Library.TopicClient
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe(ISubscription subscription);
    }
}