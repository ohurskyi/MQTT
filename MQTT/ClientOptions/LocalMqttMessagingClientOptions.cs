using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Test.ClientOptions;

public class LocalMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "localhost", Port = 1883 };
}