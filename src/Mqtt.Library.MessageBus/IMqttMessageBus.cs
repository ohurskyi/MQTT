using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.MessageBus;

public interface IMqttMessageBus<TMessagingClientOptions>: IMqttMessageBus
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
}

public interface IMqttMessageBus
{
    Task Publish(IMessagePayload payload, string topic);
}