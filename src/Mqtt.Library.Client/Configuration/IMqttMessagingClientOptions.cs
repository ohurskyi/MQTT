using MessagingLibrary.Core.Configuration;

namespace Mqtt.Library.Client.Configuration
{
    public interface IMqttMessagingClientOptions : IMessagingClientOptions
    {
        MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}