using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using MQTTnet;

namespace Mqtt.Library.MessageBus;

public class MqttMqttMessageBus<TMessagingClientOptions> : IMqttMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;

    public MqttMqttMessageBus(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient)
    {
        _mqttMessagingClient = mqttMessagingClient;
    }

    public async Task Publish(MqttApplicationMessage mqttApplicationMessage)
    {
        await _mqttMessagingClient.PublishAsync(mqttApplicationMessage);
    }
}