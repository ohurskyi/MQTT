﻿using MessagingLibrary.Core.Messages;
using MQTTnet;

namespace MessagingLibrary.MessageBus.Mqtt.Extensions;

public static class MqttApplicationMessageExtensions
{
    public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
    {
        var mqttApplicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(msg.Topic)
            .WithPayload(msg.Payload)
            .WithResponseTopic(msg.ReplyTopic)
            .WithCorrelationData(msg.CorrelationId.ToByteArray())
            .Build();

        return mqttApplicationMessage;
    }
}