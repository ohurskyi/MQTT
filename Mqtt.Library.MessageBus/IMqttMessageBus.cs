using Mqtt.Library.Client.Configuration;
using MQTTnet;

namespace Mqtt.Library.MessageBus;

public interface IMqttMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task Publish(MqttApplicationMessage mqttApplicationMessage);
}