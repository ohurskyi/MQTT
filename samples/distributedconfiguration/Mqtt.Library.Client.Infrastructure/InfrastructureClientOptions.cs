using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

namespace Mqtt.Library.Client.Infrastructure;

public class InfrastructureClientOptions : IMqttMessagingClientOptions
{
    public MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; } = new() { Host = "localhost", Port = 1883 };
}