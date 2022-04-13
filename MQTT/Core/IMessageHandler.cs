using MQTTnet;

namespace Mqtt.Library.Test.Core;

public interface IMessageHandler
{
    Task Handle(MqttApplicationMessage mqttApplicationMessage);
}