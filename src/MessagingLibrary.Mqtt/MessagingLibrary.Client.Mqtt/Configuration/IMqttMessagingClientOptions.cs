using MessagingLibrary.Core.Configuration;

namespace MessagingLibrary.Client.Mqtt.Configuration
{
    public interface IMqttMessagingClientOptions : IMessagingClientOptions
    {
        MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}