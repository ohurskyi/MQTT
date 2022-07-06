using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Handlers;

namespace Mqtt.Library.TopicClient
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : class, IMessageHandler;
        Task Unsubscribe(ISubscription subscription);
    }
}