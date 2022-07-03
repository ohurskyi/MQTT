using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Client.Infrastructure;

public class InfrastructureMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "localhost", Port = 1883 };
}