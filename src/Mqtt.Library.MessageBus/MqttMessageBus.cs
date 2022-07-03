using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.MessageBus;

public class MqttMessageBus<TMessagingClientOptions> : IMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;

    public MqttMessageBus(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient)
    {
        _mqttMessagingClient = mqttMessagingClient;
    }

    public async Task Publish(IMessageContract contract, string topic)
    {
        var message = new Message { Topic = topic, Payload = contract.MessagePayloadToJson() };
        await _mqttMessagingClient.PublishAsync(message.ToMqttMessage());
    }

    public async Task Publish(IMessage message)
    {
        await _mqttMessagingClient.PublishAsync(message.ToMqttMessage());
    }
}