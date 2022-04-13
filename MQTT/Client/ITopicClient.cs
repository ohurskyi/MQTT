using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Processing;

namespace Mqtt.Library.Test.Client
{
    public interface ITopicClient<TMessagingClientOptions>
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe<T>(string topic) where T : IMessageHandler;
    }
}