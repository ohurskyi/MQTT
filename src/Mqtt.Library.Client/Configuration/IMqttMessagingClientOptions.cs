namespace Mqtt.Library.Client.Configuration
{
    public interface IMqttMessagingClientOptions
    {
        MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}