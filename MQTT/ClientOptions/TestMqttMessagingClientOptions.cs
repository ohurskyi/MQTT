using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Test.ClientOptions;

public class TestMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } =  new() { Host = "localhost", Port = 1883 };
}