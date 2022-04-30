using System.Text;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.Processing;

public static class MqttApplicationMessageExtensions
{
    public static IMessage ToMessage(this MqttApplicationMessage mqttApplicationMessage)
    {
        var payloadStr = Encoding.UTF8.GetString(mqttApplicationMessage.Payload);
        var message = new Message { Topic = mqttApplicationMessage.Topic, Payload = payloadStr };
        return message;
    }
}