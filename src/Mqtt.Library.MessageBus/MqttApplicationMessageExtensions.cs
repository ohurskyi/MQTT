using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.MessageBus;

public static class MqttApplicationMessageExtensions
{
    public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
    {
        var body = msg.ToJson();
        var mqttApplicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(msg.Topic)
            .WithPayload(body)
            .Build();

        return mqttApplicationMessage;
    }
}