using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using Newtonsoft.Json;

namespace Mqtt.Library.MessageBus;

public class MqttMessageBus<TMessagingClientOptions> : IMqttMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;

    public MqttMessageBus(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient)
    {
        _mqttMessagingClient = mqttMessagingClient;
    }

    public async Task Publish(IMessage message)
    {
        var mqttApplicationMessage = message.ToMqttMessage();
        await _mqttMessagingClient.PublishAsync(mqttApplicationMessage);
    }

    public async Task Publish<TPayload>(TPayload payload, string topic) where TPayload : IMessagePayload
    {
        var message = new Message { Topic = topic, Body = payload };
        await Publish(message);
    }
}