using Mqtt.Library.Core.Messages;
using MQTTnet;
using Newtonsoft.Json;

namespace Mqtt.Library.MessageBus;

public static class MqttApplicationMessageExtensions
{
    public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
    {
        var body = msg.Body.ToString(Formatting.None);
        var mqttApplicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(msg.Topic)
            .WithPayload(body)
            .Build();

        return mqttApplicationMessage;
    }
}