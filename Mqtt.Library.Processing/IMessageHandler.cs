using MQTTnet;

namespace Mqtt.Library.Processing;

public interface IMessageHandler
{
    Task Handle(MqttApplicationMessage mqttApplicationMessage);
}