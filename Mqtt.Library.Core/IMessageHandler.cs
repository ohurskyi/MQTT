using MQTTnet;

namespace Mqtt.Library.Core;

public interface IMessageHandler
{
    Task Handle(MqttApplicationMessage mqttApplicationMessage);
}