using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.GenericTest;

namespace Mqtt.Library.MessageBus.GenericTest;

public class MqttMessageBusGen<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;

    public MqttMessageBusGen(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient)
    {
        _mqttMessagingClient = mqttMessagingClient;
    }

    public async Task Publish(IMessage message)
    {
        var mqttApplicationMessage = message.ToMqttMessage();
        await _mqttMessagingClient.PublishAsync(mqttApplicationMessage);
    }
}