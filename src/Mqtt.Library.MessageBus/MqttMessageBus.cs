using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Newtonsoft.Json.Linq;

namespace Mqtt.Library.MessageBus;

public class MqttMessageBus<TMessagingClientOptions> : IMqttMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;

    public MqttMessageBus(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient)
    {
        _mqttMessagingClient = mqttMessagingClient;
    }

    public async Task Publish(IMessagePayload payload, string topic)
    {
        var message = new Message { Topic = topic, Payload = payload.MessagePayloadToJson() };
        await _mqttMessagingClient.PublishAsync(message.ToMqttMessage());
    }
}