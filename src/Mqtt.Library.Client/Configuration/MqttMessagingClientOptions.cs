﻿namespace Mqtt.Library.Client.Configuration
{
    public abstract class MqttMessagingClientOptions : IMqttMessagingClientOptions
    {
        public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}