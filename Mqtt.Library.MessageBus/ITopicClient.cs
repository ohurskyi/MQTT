using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;

namespace Mqtt.Library.MessageBus
{
    public interface ITopicClient<TMessagingClientOptions>
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe<T>(string topic) where T : IMessageHandler;
    }
}