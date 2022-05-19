﻿using Mqtt.Library.Core.Messages;
using MQTTnet;
using Newtonsoft.Json;

namespace Mqtt.Library.MessageBus;

public static class MqttApplicationMessageExtensions
{
    public static MqttApplicationMessage ToMqttMessage(this IMessage msg)
    {
        var mqttApplicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(msg.Topic)
            .WithPayload(msg.Payload)
            .Build();

        return mqttApplicationMessage;
    }
}