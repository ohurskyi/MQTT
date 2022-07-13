using System.Text;
using MessagingLibrary.Core.Messages;
using MQTTnet;

namespace MessagingLibrary.Processing.Mqtt.Extensions;

public static class MqttApplicationMessageExtensions
{
    public static IMessage ToMessage(this MqttApplicationMessage mqttApplicationMessage)
    {
        var payloadStr = Encoding.UTF8.GetString(mqttApplicationMessage.Payload);
        var message = new Message
        {
            Topic = mqttApplicationMessage.Topic, 
            Payload = payloadStr, 
            ReplyTopic = mqttApplicationMessage.ResponseTopic,
            CorrelationId = mqttApplicationMessage.CorrelationData == null ? Guid.Empty : new Guid(mqttApplicationMessage.CorrelationData)
        };
        return message;
    }
}