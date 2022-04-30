using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.MessageBus;

public interface IMqttMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task Publish(IMessage message);
}