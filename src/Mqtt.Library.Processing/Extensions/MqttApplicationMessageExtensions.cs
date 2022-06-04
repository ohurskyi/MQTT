using System.Text;
using Mqtt.Library.Core.Messages;
using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mqtt.Library.Processing.Extensions;

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
            CorrelationId = new Guid(mqttApplicationMessage.CorrelationData)
        };
        return message;
    }
}