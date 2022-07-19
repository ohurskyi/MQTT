using MessagingLibrary.Core.Configuration;

namespace MessagingLibrary.Processing.Mqtt.Configuration.Configuration
{
    public interface IMqttMessagingClientOptions : IMessagingClientOptions
    {
        MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}