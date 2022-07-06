using MessagingLibrary.Client.Mqtt.Configuration;

namespace Mqtt.Library.Client.Infrastructure;

public class InfrastructureMqttMessagingClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "localhost", Port = 1883 };
}