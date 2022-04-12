using MessagingClient.Mqtt;
using Mqtt.Library.Test.Client.Configuration;
using Mqtt.Library.Test.Core;
using MQTTnet;

namespace Mqtt.Library.Test.Client;

public class TopicClient<TMessagingClientOptions> : ITopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;
    private readonly IMessageHandlerFactory _messageHandlerFactory;

    public TopicClient(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient, IMessageHandlerFactory messageHandlerFactory)
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

    public async Task Publish(MqttApplicationMessage mqttApplicationMessage)
    {
        await _mqttMessagingClient.PublishAsync(mqttApplicationMessage);
    }
}