using MessagingLibrary.Core.Handlers;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.TopicClient
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe(ISubscription subscription);
    }
}