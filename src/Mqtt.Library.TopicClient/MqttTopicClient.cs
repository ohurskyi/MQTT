using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;

namespace Mqtt.Library.TopicClient;

public class MqttTopicClient<TMessagingClientOptions> : IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;
    private readonly IMessageHandlerFactory _messageHandlerFactory;

    public MqttTopicClient(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient, IMessageHandlerFactory messageHandlerFactory)
    {
        _mqttMessagingClient = mqttMessagingClient;
        _messageHandlerFactory = messageHandlerFactory;
    }

    public async Task Subscribe<T>(string topic) where T : IMessageHandler
    {
        if (_messageHandlerFactory.RegisterHandler<T>(topic) == 1)
        {
            await _mqttMessagingClient.SubscribeAsync(topic);
        }
    }

    public async Task Unsubscribe<T>(string topic) where T : IMessageHandler
    {
        await _mqttMessagingClient.UnsubscribeAsync(topic);
    }
}