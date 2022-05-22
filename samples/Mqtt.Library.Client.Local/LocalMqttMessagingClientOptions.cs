using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Client.Local;

public class LocalMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "localhost", Port = 1883 };
}