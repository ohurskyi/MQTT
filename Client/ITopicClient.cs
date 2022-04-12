using Mqtt.Library.Test.Client.Configuration;
using Mqtt.Library.Test.Core;

namespace Mqtt.Library.Test.Client
{
    public interface ITopicClient<TMessagingClientOptions>
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task Subscribe<T>(string topic) where T : IMessageHandler;
        Task Unsubscribe<T>(string topic) where T : IMessageHandler;
    }
}