using System.Text;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.Core;

public static class MqttApplicationMessageExtensions
{
    public static IMessage ToMessage(this MqttApplicationMessage mqttApplicationMessage)
    {
        var payloadStr = Encoding.UTF8.GetString(mqttApplicationMessage.Payload);
        var message = new Message { Topic = mqttApplicationMessage.Topic, Payload = payloadStr };
        return message;
    }
        
    public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
    {
        var mqttApplicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(msg.Topic)
            .WithPayload(msg.Payload)
            .Build();

        return mqttApplicationMessage;
    }
}