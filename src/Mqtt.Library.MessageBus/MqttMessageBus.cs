using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;

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
}